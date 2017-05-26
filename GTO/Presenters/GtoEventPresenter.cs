using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class GtoEventPresenter : EditPresenterBase
    {
        public GtoEvent EditableObject { get; set; }

        private readonly GtoEventService _gtoEventService;
        private readonly JudgeService _judgeService;
        private readonly PlayerService _playerService;

        private BindingList<GtoEventPlayer> _eventPlayers;
        private BindingList<GtoEventTest> _eventTests;

        private List<ComboBoxItem> _players;

        public GtoEventPresenter()
        {
            _gtoEventService = new GtoEventService();
            _judgeService = new JudgeService();
            _playerService = new PlayerService();

            EditableObject = _gtoEventService.GetCurrentEventRegistration();
            InitPlayers();
        }

        public BindingList<GtoEventPlayer> EventPlayerDataSource => _eventPlayers ?? (_eventPlayers = GetEvenPlayers());

        public BindingList<GtoEventTest> EventTestDataSource => _eventTests ?? (_eventTests = GetEventTests());

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

        public List<ComboBoxItem> AviablePlayerList
        {
            get
            {
                var aviablePlayers = new List<ComboBoxItem>(_players);
                aviablePlayers.Insert(0 , new ComboBoxItem
                {
                    Value = 0,
                    Text = "Не выбрано"
                });
                return aviablePlayers;
            }
        }

        private void UpdateUsers()
        {
            EditableObject.GtoEventPlayers.Clear();
            foreach (var groupedPlayers in EventPlayerDataSource.GroupBy(ep=> ep.PlayerId))
            {
                if (groupedPlayers.Key == 0) continue;

                GtoEventPlayer eventPlayer;
                if (groupedPlayers.Count() > 1)
                {
                    eventPlayer = groupedPlayers.FirstOrDefault(p => p.Id != 0) ?? groupedPlayers.First();
                }
                else
                {
                    eventPlayer = groupedPlayers.First();
                }
                EditableObject.GtoEventPlayers.Add(eventPlayer);
            }
        }

        private void InitPlayers()
        {
            _players = _playerService.GetAll()
                .Select(j => new ComboBoxItem
                {
                    Text = j.FullName,
                    Value = j.Id
                })
                .ToList();
        }

        private BindingList<GtoEventTest> GetEventTests()
        {
            return new BindingList<GtoEventTest>(EditableObject.GtoEventTests.ToList());
        }

        private BindingList<GtoEventPlayer> GetEvenPlayers()
        {
            return new BindingList<GtoEventPlayer>(EditableObject.GtoEventPlayers.ToList());
        }

        public override void Dispose()
        {
            _gtoEventService.Dispose();
        }

        public override void Save()
        {
            UpdateUsers();
            _gtoEventService.AddOrUpdate(EditableObject);
        }
    }
}