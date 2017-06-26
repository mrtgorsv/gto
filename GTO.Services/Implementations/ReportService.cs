using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GTO.Model.Context;
using GTO.Model.Enums;

namespace GTO.Services.Implementations
{
    public class ReportService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        private readonly GtoEventService _gtoEventService = new GtoEventService();
        private readonly PlayerService _playerService = new PlayerService();

        public void Dispose()
        {
            _context?.Dispose();
        }

        #region Player Report

        public void ShowPlayerReport(int playerId, string fileName)
        {
            var selectedPlayer = _context.Players.FirstOrDefault(p => p.Id == playerId);
            if (selectedPlayer == null)
            {
                throw new ArgumentException("Участник не найден");
            }

            List<PlayerTestInfo> results = new List<PlayerTestInfo>();

            var playerRecords = _playerService.GetGroupedPlayerRecords(playerId);

            AgeGroup playerAgeGroup = _playerService.GetplayerAgeGroup(selectedPlayer.Age);

            foreach (IGrouping<int, GtoEventPlayerRecord> resultGroup in playerRecords)
            {
                var bestRecord =
                    resultGroup.FirstOrDefault(rg => rg.ResultRank == resultGroup.Min(rgr => rgr.ResultRank));
                if (bestRecord != null)
                {
                    results.Add(new PlayerTestInfo
                    {
                        TestName = bestRecord.GtoEventTest.TestName,
                        RankName = bestRecord.ResultRankName,
                        TestId = bestRecord.GtoEventTest.TestId,
                        Required = bestRecord.GtoEventTest.Test.Required
                    });
                }
            }

            if (playerAgeGroup == null)
            {
                return;
            }

            var completedTestIds = results.Select(r => r.TestId).ToList();

            var notCompleted = playerAgeGroup.TestGroups
                .Where(tg => !completedTestIds.Contains(tg.TestId) && (tg.Sex == selectedPlayer.Sex || tg.Sex == 2))
                .Select(tg => new PlayerTestInfo
                {
                    TestName = tg.Test.Name,
                    Required = tg.Test.Required,
                    TestId = tg.TestId
                })
                .ToList();

            string medalName = ComputeGtoMedal(
                playerAgeGroup.AgeGroupRequirments.FirstOrDefault(agr => agr.Sex == selectedPlayer.Sex || agr.Sex == 2),
                completedTestIds.Count);

            ExportPlayerReport(selectedPlayer.FullName, results, notCompleted, fileName, medalName);
        }

        private void ExportPlayerReport(string fullName, List<PlayerTestInfo> results,
            List<PlayerTestInfo> requiredTests,
            string fileName, string medalName)
        {
            using (var workbook = CreateWorkbook(fileName))
            {
                var sheetData = new SheetData();
                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();

                ApplySheet(sheetPart, workbook, "Отчет участника");

                var headerCells = new[] {"Участник", "Испытание", "Медаль", "Обязательное"};
                AppendRow(sheetData, 0, headerCells);

                bool nameAdded = false;

                if (!results.Any())
                {
                    Row newRow = new Row();
                    AppendCell(fullName, newRow);
                    sheetData.AppendChild(newRow);
                }
                else
                {
                    foreach (var result in results)
                    {
                        var rowCells = new[]
                        {
                            nameAdded ? string.Empty : fullName,
                            result.TestName,
                            result.RankName,
                            result.Required ? "Да" : "Нет"
                        };
                        AppendRow(sheetData,0, rowCells);

                        if (!nameAdded)
                        {
                            nameAdded = true;
                        }
                    }
                }

                AppendRow(sheetData , 0);

                AppendRow(sheetData, 1, "Невыполненные испытания");

                foreach (PlayerTestInfo requiredTest in requiredTests.OrderByDescending(rt => rt.Required))
                {
                    var rowCells = new[]
                    {
                        requiredTest.TestName,
                        string.Empty,
                        requiredTest.Required ? "Да" : "Нет"
                    };
                    AppendRow(sheetData, 1 , rowCells);
                }
                AppendRow(sheetData, 1, "Знак ГТО", medalName);

                sheetPart.Worksheet.Append(AutoSize(sheetData));

                sheetPart.Worksheet.Append(sheetData);
            }

            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion

        #region Medal Report

        public void ShowMedalReport(DateTime start, DateTime end)
        {
            List<GtoEvent> gtoEvents = _gtoEventService.GetByRange(start, end);
            if (gtoEvents.Any())
            {
                List<int> gtoEventIds = gtoEvents.Select(ge => ge.Id).ToList();

                _context.GtoEventTests
                    .Include(ge => ge.Test)
                    .Include(ge => ge.GtoEventPlayerRecords)
                    .Include(ge => ge.GtoEventPlayerRecords.Select(gepr => gepr.GtoEventPlayer.Player))
                    .Where(gtet => gtoEventIds.Contains(gtet.GtoEventId))
                    .Load();

                var eventRecords = gtoEvents.SelectMany(ge => ge.GtoEventTests).SelectMany(gevt => gevt.GtoEventPlayerRecords).ToList();


                ExportMedalReport(start, end, eventRecords);
            }
            else
            {
                throw new ArgumentException("Не найдено соревнований по указаному диапазону");
            }
        }

        private void ExportMedalReport(DateTime start, DateTime end, List<GtoEventPlayerRecord> eventRecords)
        {
            List<ResultRank> medals = eventRecords
                .Where(er => !er.ResultRank.Equals(ResultRank.NoRank))
                .Select(er => er.ResultRank)
                .ToList();

            int gtoMedalCount = 0;

            int eventPlayerCount = eventRecords.Count;
            int eventPlayerWithMedalCount = eventRecords.Count(er => !er.ResultRank.Equals(ResultRank.NoRank));

            using (var workbook = CreateWorkbook($"Медали соревнований с {start:d} по {end:d}"))
            {
                var sheetData = new SheetData();
                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                ApplySheet(sheetPart, workbook, "Итоги");

                #region Header

                AppendEmptyRow(sheetData);
                AppendRow(sheetData, 2, "Период с", start.ToString("d"), "по", end.ToString("d"));
                AppendEmptyRow(sheetData);

                #endregion

                #region Medals statistic

                AppendRow(sheetData, 1 , "Пройдено испытаний", string.Empty  , string.Empty                                              , string.Empty , "Получено значков ГТО");
                AppendRow(sheetData, 2                       , "Всего"       , medals.Count.ToString()                                   , string.Empty , string.Empty          , "Всего"   , gtoMedalCount.ToString());
                AppendRow(sheetData, 2                       , "Золото"      , medals.Count(m => m.Equals(ResultRank.Gold)).ToString()   , string.Empty , string.Empty          , "Золото"  , gtoMedalCount.ToString());
                AppendRow(sheetData, 2                       , "Серебро"     , medals.Count(m => m.Equals(ResultRank.Silver)).ToString() , string.Empty , string.Empty          , "Серебро" , gtoMedalCount.ToString());
                AppendRow(sheetData, 2                       , "Бронза"      , medals.Count(m => m.Equals(ResultRank.Bronze)).ToString() , string.Empty , string.Empty          , "Бронза"  , gtoMedalCount.ToString());
                AppendEmptyRow(sheetData);

                #endregion

                #region Player stats

                AppendRow(sheetData, 2 , "Спортсменов учавствовало");
                AppendRow(sheetData, 3                             , "Всего"     , eventPlayerCount.ToString());
                AppendRow(sheetData, 3                             , "С медалями", eventPlayerWithMedalCount.ToString());

                #endregion

                sheetPart.Worksheet.Append(AutoSize(sheetData));

                sheetPart.Worksheet.Append(sheetData);
            }

            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void AppendEmptyRow(SheetData sheetData)
        {
            AppendRow(sheetData , 0);
        }

        #endregion

        #region Event Report

        public void ShowEventReport(DateTime eventDate)
        {
            GtoEvent gtoEvent = _gtoEventService.GetByDate(eventDate.Date);

            if (gtoEvent != null)
            {
                _context.GtoEventTests
                    .Include(ge => ge.Test)
                    .Include(ge => ge.GtoEventPlayerRecords)
                    .Include(ge => ge.GtoEventPlayerRecords.Select(gepr => gepr.GtoEventPlayer.Player))
                    .Where(gtet => gtet.GtoEventId.Equals(gtoEvent.Id))
                    .Load();

                List<PlayerTestInfo> result = gtoEvent.GtoEventTests
                    .SelectMany(gevt => gevt.GtoEventPlayerRecords)
                    .Select(CreatePlayerInfo)
                    .ToList();

                ExportEventReport(eventDate, result);
            }
        }

        private void ExportEventReport(DateTime eventDate, List<PlayerTestInfo> results)
        {
            using (var workbook = CreateWorkbook($"Итоги соревнования от {eventDate:d}"))
            {
                var sheetData = new SheetData();
                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                ApplySheet(sheetPart, workbook, "Итоги");

                AppendRow(sheetData, 0, "Участник", "Медаль", "Испытание");

                foreach (var result in results)
                {
                    AppendRow(sheetData, 0 , result.PlayerName, result.RankName, result.TestName);
                }

                sheetPart.Worksheet.Append(AutoSize(sheetData));

                sheetPart.Worksheet.Append(sheetData);
            }

            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion

        private string ComputeGtoMedal(AgeGroupRequirment ageGroupRequirment, int completeCount)
        {
            if (ageGroupRequirment != null)
            {
                if (ageGroupRequirment.GoldCount <= completeCount)
                {
                    return "золото";
                }
                if (ageGroupRequirment.SilverCount <= completeCount)
                {
                    return "серебро";
                }
                if (ageGroupRequirment.BronzeCount <= completeCount)
                {
                    return "бронза";
                }
            }

            return "без медали";
        }

        #region Utils

        private static Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            Dictionary<int, int> maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            UInt32[] numberStyles = {5, 6, 7, 8}; //styles that will add extra chars
            UInt32[] boldStyles = {1, 2, 3, 4, 6, 7, 8}; //styles that will bold
            foreach (var r in rows)
            {
                var cells = r.Elements<Cell>().ToArray();

                //using cell index as my column
                for (int i = 0; i < cells.Length; i++)
                {
                    var cell = cells[i];
                    var cellValue = cell.CellValue == null ? string.Empty : cell.CellValue.InnerText;
                    var cellTextLength = cellValue.Length;

                    if (cell.StyleIndex != null && numberStyles.Contains<uint>(cell.StyleIndex))
                    {
                        int thousandCount = (int) Math.Truncate((double) cellTextLength / 4);

                        //add 3 for '.00' 
                        cellTextLength += (3 + thousandCount);
                    }

                    if (cell.StyleIndex != null && boldStyles.Contains<uint>(cell.StyleIndex))
                    {
                        //add an extra char for bold - not 100% acurate but good enough for what i need.
                        cellTextLength += 1;
                    }

                    if (maxColWidth.ContainsKey(i))
                    {
                        var current = maxColWidth[i];
                        if (cellTextLength > current)
                        {
                            maxColWidth[i] = cellTextLength;
                        }
                    }
                    else
                    {
                        maxColWidth.Add(i, cellTextLength);
                    }
                }
            }

            return maxColWidth;
        }


        private Columns AutoSize(SheetData sheetData)
        {
            var maxColWidth = GetMaxCharacterWidth(sheetData);

            Columns columns = new Columns();
            const double maxWidth = 7;
            foreach (var item in maxColWidth)
            {
                var width = Math.Truncate((item.Value * maxWidth + 15) / maxWidth * 256) / 256;

                Column col = new Column
                {
                    BestFit = true,
                    Min = (uint) (item.Key + 1),
                    Max = (uint) (item.Key + 1),
                    CustomWidth = true,
                    Width = width
                };
                columns.Append(col);
            }

            return columns;
        }

        private SpreadsheetDocument CreateWorkbook(string name)
        {
            SpreadsheetDocument workbook =
                SpreadsheetDocument.Create(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{name}.xlsx"),
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);

            var workbookPart = workbook.AddWorkbookPart();

            workbookPart.Workbook = new Workbook
            {
                Sheets = new Sheets()
            };

            return workbook;
        }

        private void ApplySheet(WorksheetPart sheetPart, SpreadsheetDocument workbook, string sheetName)
        {
            sheetPart.Worksheet = new Worksheet();

            Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Any())
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            Sheet sheet = new Sheet {Id = relationshipId, SheetId = sheetId, Name = sheetName};

            sheets.Append(sheet);
        }

        private PlayerTestInfo CreatePlayerInfo(GtoEventPlayerRecord eventPlayerRecord)
        {
            return new PlayerTestInfo
            {
                PlayerName = eventPlayerRecord.EventTestPlayerName,
                RankName = eventPlayerRecord.ResultRankName,
                TestName = eventPlayerRecord.EventTestName,
                Required = eventPlayerRecord.GtoEventTest.Test.Required,
                TestId = eventPlayerRecord.GtoEventTest.TestId
            };
        }

        private void AppendRow(SheetData sheetData, int skip, params string[] cells)
        {
            var row = new Row();

            while (skip > 0)
            {
                AppendCell(string.Empty, row);
                skip--;
            }

            foreach (string cell in cells)
            {
                AppendCell(cell, row);
            }
            sheetData.AppendChild(row);
        }
        private void AppendCell(string value, Row header)
        {
            Cell cell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value)
            };
            header.AppendChild(cell);
        }

        #endregion
    }

    public class PlayerTestInfo
    {
        public string TestName { get; set; }
        public string RankName { get; set; }
        public string PlayerName { get; set; }
        public int TestId { get; set; }
        public bool Required { get; set; }
    }
}