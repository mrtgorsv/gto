using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class GtoEventService : IDisposable
    {
        private readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        private GtoEvent Create()
        {
            var now = DateTime.Now;
            var gtoEvent = _context.GtoEvents.Create();
            gtoEvent.EventDate = now;
            return gtoEvent;
        }

        public void Add(GtoEvent gtoEvent)
        {
            _context.GtoEvents.Add(gtoEvent);
            _context.SaveChanges();
        }

        public GtoEvent GetCurrentEventRegistration()
        {
            var currDate = DateTime.Now.Date;
            var nextDate = currDate.AddDays(1);
            var gtoEvent = _context.GtoEvents
                .Include(ge => ge.GtoEventPlayers)
                .Include(ge => ge.GtoEventTests)
                .FirstOrDefault(ge => ge.EventDate >= currDate && ge.EventDate < nextDate);

            if (gtoEvent == null)
            {
                gtoEvent = Create();
                FillEventTests(gtoEvent);
            }
            return gtoEvent;
        }
        public GtoEvent GetCurrentEventDetail()
        {
            var currDate = DateTime.Now.Date;
            var nextDate = currDate.AddDays(1);
            var gtoEvent = _context.GtoEvents
                .Include(ge => ge.GtoEventTests)
                .Include(ge => ge.GtoEventPlayers)
                .Include(ge => ge.GtoEventTests.Select(gete => gete.Test))
                .Include(ge => ge.GtoEventPlayers.Select(gep => gep.Player))
                .FirstOrDefault(ge => ge.EventDate >= currDate && ge.EventDate < nextDate);

            if (gtoEvent == null)
            {
                gtoEvent = Create();
                FillEventTests(gtoEvent);
            }
            return gtoEvent;
        }

        private void FillEventTests(GtoEvent gtoEvent)
        {
            foreach (Test test in _context.Tests)
            {
                var gtoEventTest = _context.GtoEventTests.Create();
                gtoEventTest.TestId = test.Id;
                gtoEventTest.Test = test;
                gtoEvent.GtoEventTests.Add(gtoEventTest);
            }
        }

        public void AddOrUpdate(GtoEvent gtoEvent)
        {
            if (gtoEvent.Id == 0)
            {
                Add(gtoEvent);
            }
            else
            {
                _context.Entry(gtoEvent).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public List<GtoEventPlayerRecord> GetCurrentEventRecords()
        {
            GtoEvent currentEvent = GetCurrentEventRegistration();

            var records = _context.GtoEventPlayerRecords.Where(gepr => gepr.GtoEventTest.GtoEventId == currentEvent.Id).ToList();

            if (!records.Any())
            {
                if (currentEvent.GtoEventPlayers.Any())
                {
                    records = CreateGotEventPlayerRecords(currentEvent);
                }
            }

            return records;
        }

        private List<GtoEventPlayerRecord> CreateGotEventPlayerRecords(GtoEvent gtoEvent)
        {
            List<GtoEventPlayerRecord> result = new List<GtoEventPlayerRecord>();
            foreach (GtoEventPlayer player in gtoEvent.GtoEventPlayers)
            {
                var gtoEventPlayerRecord = _context.GtoEventPlayerRecords.Create();
                gtoEventPlayerRecord.GtoEventPlayer = player;
                gtoEventPlayerRecord.GtoEventPlayerId = player.Id;
                result.Add(gtoEventPlayerRecord);
            }
            return result;
        }

        public void SaveEventRecords(List<GtoEventPlayerRecord> gtoEventRecords)
        {
            foreach (GtoEventPlayerRecord gtoEventPlayerRecord in gtoEventRecords)
            {
                if (gtoEventPlayerRecord.GtoEventPlayerId == 0 || gtoEventPlayerRecord.GtoEventTestId == 0)
                {
                    if (gtoEventPlayerRecord.Id > 0)
                    {
                        _context.Entry(gtoEventPlayerRecord).State = EntityState.Deleted;
                    }
                }
                else
                {
                    AddOrUpdateRecord(gtoEventPlayerRecord);
                }
            }
            _context.SaveChanges();
        }

        private void AddOrUpdateRecord(GtoEventPlayerRecord record)
        {
            if (record.Id > 0)
            {
                _context.Entry(record).State =EntityState.Modified; 
            }
            else
            {
                _context.GtoEventPlayerRecords.Add(record);
            }
        }

        public List<TestGroup> GetTestGroups()
        {
            return _context.TestGroups.ToList();
        }
        public List<AgeGroup> GetAgeGroups()
        {
            return _context.AgeGroups
                .Include(ag=> ag.TestGroups)
                .ToList();
        }
    }
}