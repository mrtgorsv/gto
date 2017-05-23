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

        public GtoEventTestResultPresenter()
        {
            _gtoEventService = new GtoEventService();

            _currentEvent = _gtoEventService.GetCurrentEventDetail();

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

        public List<ComboBoxItem> EventTests
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
    }
}