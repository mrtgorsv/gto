using System;
using System.Windows.Forms;
using GTO.Presenters.Report;
using GTO.Properties;

namespace GTO.Views.Info
{
    public partial class MedalReportForm : Form
    {
        public MedalReportPresenter CurrentPresenter { get; set; }

        public MedalReportForm()
        {
            InitializeComponent();
            Load += OnFormLoad;
            ShowReportButton.Click += OnShowReportClick;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new MedalReportPresenter();
            StartDatePicker.Value = DateTime.Now;
            EndDatePicker.Value = DateTime.Now;
        }

        private void OnShowReportClick(object sender, EventArgs e)
        {
            try
            {
                CurrentPresenter.ShowReport(StartDatePicker.Value, EndDatePicker.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }

        private void MedalReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
