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
        private readonly ReportService _reportService;

        public PlayerReportPresenter()
        {
            _playerService = new PlayerService();
            _reportService = new ReportService();
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

        public void ShowReport(int playerId , string fileName)
        {
            _reportService.ShowReport(playerId, fileName);
        }

        public void Dispose()
        {
            _playerService.Dispose();
            _reportService.Dispose();
        }
    }
}