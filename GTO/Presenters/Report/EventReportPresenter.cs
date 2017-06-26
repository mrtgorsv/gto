using System;
using GTO.Services.Implementations;

namespace GTO.Presenters.Report
{
    public class EventReportPresenter : IDisposable
    {
        private readonly GtoEventService _gtoEventService;
        private readonly ReportService _reportService;

        public EventReportPresenter()
        {
            _reportService = new ReportService();
            _gtoEventService = new GtoEventService();
        }

        public void ShowReport(DateTime eventDate)
        {

            eventDate = eventDate.Date;

            if (_gtoEventService.EventExists(eventDate))
            {
                _reportService.ShowEventReport(eventDate);
            }
            else
            {
                throw new ArgumentException($"Соревнования на дату {eventDate:dd:MM:yyyy} не найдено");
            }
        }

        public void Dispose()
        {
            _reportService?.Dispose();
            _gtoEventService?.Dispose();
        }
    }
}