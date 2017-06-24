using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GTO.Model.Context;
using GTO.Model.Enums;

namespace GTO.Services.Implementations
{
    public class ReportService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void ShowPlayerReport(int playerId, string fileName)
        {
            var selectedPlayer = _context.Players.FirstOrDefault(p => p.Id == playerId);
            if (selectedPlayer == null)
            {
                throw new ArgumentException("Участник не найден");
            }

            var playerRecords =
                _context.GtoEventPlayerRecords.Include(epr => epr.GtoEventTest)
                    .Include(epr => epr.GtoEventTest.Test)
                    .Where(epr => epr.GtoEventPlayer.PlayerId == selectedPlayer.Id)
                    .Where(rg => rg.ResultRank != ResultRank.NoRank)
                    .GroupBy(gepr => gepr.GtoEventTest.TestId);

            AgeGroup playerAgeGroup = _context.AgeGroups
                .Include(ag => ag.TestGroups)
                .Include(ag => ag.TestGroups.Select(tg => tg.Test))
                .FirstOrDefault(ag => ag.Max >= selectedPlayer.Age && ag.Min <= selectedPlayer.Age);

            List<PlayerTestInfo> results = new List<PlayerTestInfo>();

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

            Export(selectedPlayer.FullName, results, notCompleted, fileName, medalName);
        }

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

        private void Export(string fullName, List<PlayerTestInfo> results, List<PlayerTestInfo> requiredTests,
            string fileName, string medalName)
        {
            using (var workbook =
                SpreadsheetDocument.Create(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{fileName}.xlsx"),
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = workbook.AddWorkbookPart();

                workbookPart.Workbook = new Workbook
                {
                    Sheets = new Sheets()
                };

                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();

                var sheetData = new SheetData();
                sheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Any())
                {
                    sheetId =
                        sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                Sheet sheet = new Sheet {Id = relationshipId, SheetId = sheetId, Name = "Отчет участника"};

                sheets.Append(sheet);

                Row headerRow = new Row();
                CreateCell("Участник", headerRow);
                CreateCell("Испытание", headerRow);
                CreateCell("Медаль", headerRow);
                CreateCell("Обязательное", headerRow);

                sheetData.AppendChild(headerRow);

                bool nameAdded = false;

                if (!results.Any())
                {
                    Row newRow = new Row();
                    CreateCell(fullName, newRow);
                    sheetData.AppendChild(newRow);
                }
                else
                {
                    foreach (var result in results)
                    {
                        Row newRow = new Row();
                        if (!nameAdded)
                        {
                            CreateCell(fullName, newRow);
                            nameAdded = true;
                        }
                        else
                        {
                            CreateCell(string.Empty, newRow);
                        }
                        CreateCell(result.TestName, newRow);
                        CreateCell(result.RankName, newRow);
                        CreateCell(result.Required ? "Да" : "Нет", newRow);
                        sheetData.AppendChild(newRow);
                    }
                }

                sheetData.AppendChild(new Row());

                var secondHeader = new Row();
                CreateCell(string.Empty, secondHeader);
                CreateCell("Невыполненные испытания", secondHeader);
                sheetData.AppendChild(secondHeader);

                foreach (PlayerTestInfo requiredTest in requiredTests.OrderByDescending(rt => rt.Required))
                {
                    Row newRow = new Row();
                    CreateCell(string.Empty, newRow);
                    CreateCell(requiredTest.TestName, newRow);
                    CreateCell(string.Empty, newRow);
                    CreateCell(requiredTest.Required ? "Да" : "Нет", newRow);
                    sheetData.AppendChild(newRow);
                }
                AddGtoResult(sheetData, medalName);


                sheetPart.Worksheet.Append(AutoSize(sheetData));

                sheetPart.Worksheet.Append(sheetData);
            }

            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void ShowMedalReport(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public void ShowEventReport(DateTime eventDate)
        {
            throw new NotImplementedException();
        }

        private void AddGtoResult(SheetData sheet, string medalName)
        {
            Row gtoResultRow = new Row();
            CreateCell(string.Empty, gtoResultRow);
            CreateCell("Знак ГТО", gtoResultRow);
            CreateCell(medalName, gtoResultRow);
            sheet.Append(gtoResultRow);
        }

        #region Utils

        private static Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            Dictionary<int, int> maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            UInt32[] numberStyles = { 5, 6, 7, 8 }; //styles that will add extra chars
            UInt32[] boldStyles = { 1, 2, 3, 4, 6, 7, 8 }; //styles that will bold
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
                        int thousandCount = (int)Math.Truncate((double)cellTextLength / 4);

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

        private void CreateCell(string value, Row header)
        {
            Cell cell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value)
            };
            header.AppendChild(cell);
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
                    Min = (uint)(item.Key + 1),
                    Max = (uint)(item.Key + 1),
                    CustomWidth = true,
                    Width = width
                };
                columns.Append(col);
            }

            return columns;
        }

        #endregion

    }

    public class PlayerTestInfo
    {
        public string TestName { get; set; }
        public string RankName { get; set; }
        public int TestId { get; set; }
        public bool Required { get; set; }
    }
}