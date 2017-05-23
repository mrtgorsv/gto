using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class GtoEventPresenter : IDisposable
    {
        public GtoEvent EditableObject { get; set; }

        private readonly GtoEventService _gtoEventService;
        private readonly JudgeService _judgeService;
        private readonly PlayerService _playerService;

        private BindingList<GtoEventPlayer> _eventPlayers;
        private BindingList<GtoEventTest> _eventTests;

        public GtoEventPresenter()
        {
            _gtoEventService = new GtoEventService();
            _judgeService = new JudgeService();
            _playerService = new PlayerService();

            EditableObject = _gtoEventService.GetCurrent();
        }

        public BindingList<GtoEventPlayer> EventPlayerDataSource
        {
            get { return _eventPlayers ?? (_eventPlayers = GetEvenPlayers()); }
        }

        public BindingList<GtoEventTest> EventTestDataSource
        {
            get { return _eventTests ?? (_eventTests = GetEventTests()); }
        }

        public List<ComboBoxItem> JudgeList
        {
            get
            {
                return _judgeService.GetAll().Select(j => new ComboBoxItem
                {
                    Text = j.FullName,
                    Value = j.Id
                }).ToList();
            }
        }

        public List<ComboBoxItem> PlayerList
        {
            get
            {
                return _playerService.GetAll().Select(j => new ComboBoxItem
                {
                    Text = j.FullName,
                    Value = j.Id
                }).ToList();
            }
        }

        public void Save()
        {
            _gtoEventService.AddOrUpdate(EditableObject);
        }

        public void Dispose()
        {
            _gtoEventService.Dispose();
        }

        private BindingList<GtoEventTest> GetEventTests()
        {
            return new BindingList<GtoEventTest>(EditableObject.GtoEventTests.ToList());
        }


        private BindingList<GtoEventPlayer> GetEvenPlayers()
        {
            return new BindingList<GtoEventPlayer>(EditableObject.GtoEventPlayers.ToList());
        }
    }
}