using System;
using System.Collections.Generic;
using System.Linq;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class PlayerReportPresenter : IDisposable
    {
        private readonly PlayerService _playerService;

        public PlayerReportPresenter()
        {
            _playerService = new PlayerService();
        }

        public List<ComboBoxItem> PlayerList
        {
            get
            {
                return _playerService.GetAll()
                .Select(j => new ComboBoxItem
                {
                    Text = j.FullName,
                    Value = j.Id
                })
                .ToList();
            }
        }

        public void ShowReport(int playerId)
        {
            _playerService.ShowReport(playerId);
        }

        public void Dispose()
        {
            _playerService.Dispose();
        }
    }
}