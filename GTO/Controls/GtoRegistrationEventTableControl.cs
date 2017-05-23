using System;
using System.Windows.Forms;
using GTO.Presenters;

namespace GTO.Controls
{
    public partial class GtoRegistrationEventTableControl : UserControl
    {
        public GtoEventPresenter CurrentPresenter { get; set; }
        public GtoRegistrationEventTableControl()
        {
            InitializeComponent();
            Load += OnFormLoad;

            PlayerEventDataGridView.DataError += OnPlayerGridError;
            EventTestDataGridView.DataError += OnEventGridError;
        }

        private void OnEventGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var a = 1;
        }

        private void OnPlayerGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var a = 1;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new GtoEventPresenter();

            PlayerEventDataGridView.AutoGenerateColumns = false;
            PlayerEventDataGridView.DataSource = CurrentPresenter.EventPlayerDataSource;

            EventTestDataGridView.AutoGenerateColumns = false;
            EventTestDataGridView.DataSource = CurrentPresenter.EventTestDataSource;

            TestJudgeColumn.DataSource = CurrentPresenter.JudgeList;
            TestJudgeColumn.DisplayMember = "Text";
            TestJudgeColumn.ValueMember = "Value";
            TestJudgeColumn.DataPropertyName = "JudgeId";

            TestTypeColumn.DataPropertyName = "TestName";

            PlayerNameColumn.DataSource = CurrentPresenter.PlayerList;
            PlayerNameColumn.DisplayMember = "Text";
            PlayerNameColumn.ValueMember = "Value";
            PlayerNameColumn.DataPropertyName = "PlayerId";
        }
    }
}
