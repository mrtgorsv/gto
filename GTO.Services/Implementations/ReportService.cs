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

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void ShowReport(int playerId, string fileName)
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

            var notCompleted = playerAgeGroup.TestGroups.Where(tg => !completedTestIds.Contains(tg.TestId) && (tg.Sex == selectedPlayer.Sex || tg.Sex == 2))
                .Select(tg => new PlayerTestInfo
                {
                    TestName = tg.Test.Name,
                    Required = tg.Test.Required,
                    TestId = tg.TestId
                })
                .ToList();

            string medalName = ComputeGtoMedal(playerAgeGroup.AgeGroupRequirments.FirstOrDefault(agr=> agr.Sex == selectedPlayer.Sex || agr.Sex == 2) , completedTestIds.Count);

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

        private void Export(string fullName, List<PlayerTestInfo> results, List<PlayerTestInfo> requiredTests, string fileName, string medalName)
        {
            using (var workbook =
                SpreadsheetDocument.Create(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}.xlsx", fileName)),
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = workbook.AddWorkbookPart();

                workbookPart.Workbook = new Workbook
                {
                    Sheets = new Sheets()
                };

                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                sheetPart.Worksheet = new Worksheet(sheetData);

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
            }

            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void AddGtoResult(SheetData sheet, string medalName)
        {
            Row gtoResultRow = new Row();
            CreateCell(string.Empty, gtoResultRow);
            CreateCell("Знак ГТО", gtoResultRow);
            CreateCell(medalName, gtoResultRow);
            sheet.Append(gtoResultRow);
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
    }

    public class PlayerTestInfo
    {
        public string TestName { get; set; }
        public string RankName { get; set; }
        public int TestId { get; set; }
        public bool Required { get; set; }
    }
}