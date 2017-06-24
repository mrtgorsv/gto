using System;
using GTO.Services.Implementations;

namespace GTO.Presenters.Report
{
    public class EventReportPresenter : IDisposable
    {
        private readonly PlayerService _playerService;
        private readonly ReportService _reportService;

        public EventReportPresenter()
        {
            _playerService = new PlayerService();
            _reportService = new ReportService();
        }

        public void ShowReport(DateTime eventDate)
        {
            _reportService.ShowEventReport(eventDate);
        }

        public void Dispose()
        {
            _playerService.Dispose();
            _reportService.Dispose();
        }
    }
}