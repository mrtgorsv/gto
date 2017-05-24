using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GTO.Model.Context;
using GTO.Model.Enums;

namespace GTO.Services.Implementations
{
    public class PlayerService : IDisposable
    {
        GtoDatabaseContext _context = new GtoDatabaseContext();

        public Player Create()
        {
            return _context.Players.Create();
        }

        public List<Player> GetAll()
        {
            return _context.Players.ToList();
        }

        public void Add(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void ShowReport(int playerId)
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
                .Include(ag => ag.TestGroups.Select(tg=> tg.Test))
                .FirstOrDefault(ag => ag.Max >= selectedPlayer.Age && ag.Min <= selectedPlayer.Age);

            List<PassedPlayerResult> results = new List<PassedPlayerResult>();



            List<string> requiredTests = new List<string>();

            foreach (IGrouping<int, GtoEventPlayerRecord> resultGroup in playerRecords)
            {
                var bestRecord = resultGroup.FirstOrDefault(rg => rg.ResultRank == resultGroup.Min(rgr=> rgr.ResultRank));
                if (bestRecord != null)
                {
                    results.Add(new PassedPlayerResult
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

            var notCompleted = playerAgeGroup.TestGroups.Where(tg => !completedTestIds.Contains(tg.TestId)).Select(tg => tg.Test.Name).ToList();

            Export(selectedPlayer.FullName , results , requiredTests);
        }

        public void Export(string fullName, List<PassedPlayerResult> results, List<string> requiredTests)
        {
            string dest = string.Empty;

            using (var workbook = SpreadsheetDocument.Create(dest, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
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

                Sheet sheet = new Sheet { Id = relationshipId, SheetId = sheetId, Name = "Отчет о" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                List<string> columns = new List<string>();

                CreateCell("Участник", headerRow);
                CreateCell("Испытание", headerRow);
                CreateCell("Медаль", headerRow);

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
                    sheetData.AppendChild(newRow);
                }


                var secondHeader = new Row();
                CreateCell(string.Empty, secondHeader);
                CreateCell("Невыполненные испытания", secondHeader);

                foreach (string requiredTest in requiredTests)
                {
                    Row newRow = new Row();
                    CreateCell(string.Empty, newRow);
                    CreateCell(requiredTest, newRow);
                    sheetData.AppendChild(newRow);
                }
            }
        }

        private string CreateCell(string value , Row header)
        {
            Cell cell = new Cell();
            cell.DataType = CellValues.String;
            cell.CellValue = new CellValue(value);
            header.AppendChild(cell);
            return value;
        }
    }

    public class PassedPlayerResult
    {
        public string TestName { get; set; }
        public string RankName { get; set; }
        public int TestId { get; set; }
        public bool Required { get; set; }
    }
}