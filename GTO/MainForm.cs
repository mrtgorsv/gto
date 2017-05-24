using System;
using System.Windows.Forms;
using GTO.Controls;
using GTO.Views;

namespace GTO
{
    public partial class MainForm : Form
    {
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
            ShowForm(regForm);
        }

        private void btnCompetition_Click(object sender, EventArgs e)
        {
            var resultForm = new GtoEventTestResultTableControl {Dock = DockStyle.Fill};
            ShowForm(resultForm);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var reportForm = new ReportForm();
            reportForm.ShowDialog(this);
        }

        private void OnNeedClose(object sender, EventArgs args)
        {
            var userControl = ContentPanel.Controls[0] as UserControlBase;
            if (userControl != null)
            {
                userControl.NeedClose -= OnNeedClose;
            }
            ContentPanel.Controls.Clear();
        }


        private void ShowForm(UserControlBase userControl)
        {
            ContentPanel.Controls.Clear();
            userControl.NeedClose += OnNeedClose;
            ContentPanel.Controls.Add(userControl);
        }
    }
}
