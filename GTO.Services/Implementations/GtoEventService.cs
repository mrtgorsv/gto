using System;
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

        private GtoEventPlayer CreateNewEventPlayer(int? id)
        {
            var gtoEventPlayer = _context.GtoEventPlayers.Create();

            if (id.HasValue)
            {
                gtoEventPlayer.PlayerId = id.Value;
            }
            return gtoEventPlayer;
        }

        private GtoEventTest CreateNewGtoEventTest(int? id)
        {
            var gtoEventTest = _context.GtoEventTests.Create();
            if (id.HasValue)
            {
                gtoEventTest.TestId = id.Value;
            }
            return gtoEventTest;
        }

        public void Add(GtoEvent gtoEvent)
        {
            _context.GtoEvents.Add(gtoEvent);
            _context.SaveChanges();
        }

        public GtoEvent GetCurrent()
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
    }
}