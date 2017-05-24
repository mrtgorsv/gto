﻿using System;
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

        private List<ComboBoxItem> _players;

        public GtoEventPresenter()
        {
            _gtoEventService = new GtoEventService();
            _judgeService = new JudgeService();
            _playerService = new PlayerService();

            EditableObject = _gtoEventService.GetCurrentEventRegistration();
            InitPlayers();
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

        public void Save()
        {
            UpdateUsers();
            _gtoEventService.AddOrUpdate(EditableObject);
        }

        private void UpdateUsers()
        {
            EditableObject.GtoEventPlayers.Clear();
            foreach (GtoEventPlayer player in EventPlayerDataSource)
            {
                EditableObject.GtoEventPlayers.Add(player);
            }
        }

        public void Dispose()
        {
            _gtoEventService.Dispose();
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
    }
}