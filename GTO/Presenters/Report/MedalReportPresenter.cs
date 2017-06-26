using System;
using GTO.Services.Implementations;

namespace GTO.Presenters.Report
{
    public class MedalReportPresenter : IDisposable
    {
        private readonly PlayerService _playerService;
        private readonly ReportService _reportService;

        public MedalReportPresenter()
        {
            _playerService = new PlayerService();
            _reportService = new ReportService();
        }

        public void ShowReport(DateTime start, DateTime end)
        {
            _reportService.ShowMedalReport(start.Date, end.Date);
        }

        public void Dispose()
        {
            _playerService.Dispose();
            _reportService.Dispose();
        }
    }
}