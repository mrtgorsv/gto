using System;
using System.Windows.Forms;
using GTO.Models;
using GTO.Presenters;

namespace GTO.Views
{
    public partial class ReportForm : Form
    {
        public PlayerReportPresenter CurrentPresenter { get; set; }

        public ReportForm()
        {
            InitializeComponent();
            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new PlayerReportPresenter();
            PlayerComboBox.DisplayMember = "Text";
            PlayerComboBox.ValueMember = "Id";
            PlayerComboBox.DataSource = CurrentPresenter.PlayerList;
            ShowReportButton.Click += OnShowReportClick; 
        }

        private void OnShowReportClick(object sender, EventArgs e)
        {
            if (PlayerComboBox.SelectedItem != null)
            {
                var selectedValue = (ComboBoxItem) PlayerComboBox.SelectedValue;
                CurrentPresenter.ShowReport(selectedValue.Value);
            }
        }
    }
}
