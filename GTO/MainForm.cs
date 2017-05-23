using System;
using System.Windows.Forms;
using GTO.Controls;
using GTO.Views;

namespace GTO
{
    public partial class MainForm : Form
    {

        private GtoEventTestResultTableControl _eventResultView;
        private GtoEventReportControl _eventReportView;
        private GtoRegistrationEventTableControl _registrationEventView;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm();
            aForm.ShowDialog(this);
            aForm.Dispose();
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            var addPlayerForm = new AddPlayerForm();
            addPlayerForm.ShowDialog(this);
            addPlayerForm.Dispose();
        }

        private void btnAddJudge_Click(object sender, EventArgs e)
        {
            var addJudgeForm = new AddJudgeForm();
            addJudgeForm.ShowDialog(this);
            addJudgeForm.Dispose();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var regForm = new GtoRegistrationEventTableControl {Dock = DockStyle.Fill};
            ContentPanel.Controls.Add(regForm);
        }

        private void btnCompetition_Click(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var resultForm = new GtoEventTestResultTableControl {Dock = DockStyle.Fill};
            ContentPanel.Controls.Add(resultForm);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var reportForm = new GtoEventReportControl { Dock = DockStyle.Fill };
            ContentPanel.Controls.Add(reportForm);
        }


    }
}
