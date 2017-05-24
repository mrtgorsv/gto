using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class GtoEventTestResultPresenter : IDisposable
    {
        private GtoEvent _currentEvent;

        private readonly GtoEventService _gtoEventService;

        private BindingList<GtoEventPlayerRecord> _gtoEventPlayerRecords;
        private List<AgeGroup> _aviableAgeGroups;

        public GtoEventTestResultPresenter()
        {
            _gtoEventService = new GtoEventService();

            _currentEvent = _gtoEventService.GetCurrentEventDetail();
            _aviableAgeGroups = _gtoEventService.GetAgeGroups();

            InitEventPlayerRecords();
        }

        private void InitEventPlayerRecords()
        {
            _gtoEventPlayerRecords = new BindingList<GtoEventPlayerRecord>(_gtoEventService.GetCurrentEventRecords());
        }

        public BindingList<GtoEventPlayerRecord> EventPlayerRecordDataSource
        {
            get { return _gtoEventPlayerRecords; }
        }

        public List<ComboBoxItem> GtoEventTestDataSource
        {
            get
            {
                return _currentEvent.GtoEventTests.Select(gete => new ComboBoxItem()
                    {
                        Value = gete.Id,
                        Text = gete.TestName
                    })
                    .ToList();
            }
        }


        public void Save()
        {
            _gtoEventService.SaveEventRecords(EventPlayerRecordDataSource.ToList());
        }

        public void Dispose()
        {
            _gtoEventService.Dispose();
        }

        public List<ComboBoxItem> GetAviableEventTests(GtoEventPlayerRecord record)
        {
            var age = record.GtoEventPlayer.Player.Age;
            var sex = record.GtoEventPlayer.Player.Sex;

            var ageGroup = _aviableAgeGroups.FirstOrDefault(ag => ag.Max >= age && ag.Min <= age);
            if (ageGroup == null)
            {
                return new List<ComboBoxItem>();
            }
            var aviableTestIds = ageGroup.TestGroups.Where(tg => tg.Sex == sex || tg.Sex == 2).Select(tg=> tg.TestId).ToList();

            return _currentEvent.GtoEventTests.Where(gtet => aviableTestIds.Contains(gtet.TestId)).Select(
                gtet => new ComboBoxItem()
                {
                    Value = gtet.Id,
                    Text = gtet.TestName
                }).ToList();
        }

        public void UpdateRecordTest(GtoEventPlayerRecord record)
        {
            record.GtoEventTest = _currentEvent.GtoEventTests.FirstOrDefault(gete => gete.Id == record.GtoEventTestId);
        }
    }

    public class GtoEventTestRecodDto
    {
        public int GtoPlayerId { get; set; }
        public int? GtoTestId { get; set; }
    }
}