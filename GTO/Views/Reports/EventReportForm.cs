using System;
using System.Windows.Forms;
using GTO.Presenters.Report;
using GTO.Properties;

namespace GTO.Views.Reports
{
    public partial class EventReportForm : Form
    {
        public EventReportPresenter CurrentPresenter { get; set; }
        public EventReportForm()
        {
            InitializeComponent();

            Load += OnFormLoad;
            ShowReportButton.Click += OnShowReportClick;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new EventReportPresenter();
            EventDatePicker.Value = DateTime.Now;
        }

        private void OnShowReportClick(object sender, EventArgs e)
        {
            try
            {
                CurrentPresenter.ShowReport(EventDatePicker.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }
    }
}
