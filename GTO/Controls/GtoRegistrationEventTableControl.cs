using System;
using System.Windows.Forms;
using GTO.Presenters;
using GTO.Properties;

namespace GTO.Controls
{
    public partial class GtoRegistrationEventTableControl : UserControlBase
    {
        public GtoEventPresenter CurrentPresenter { get; set; }
        public GtoRegistrationEventTableControl()
        {
            InitializeComponent();

            PlayerEventDataGridView.DataError += OnPlayerGridError;
            EventTestDataGridView.DataError += OnEventGridError;

            SaveButton.Click += OnSaveButtonClicked;
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                CurrentPresenter.Save();
                OnNeedClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message , Resources.SaveException);
            }
        }

        private void OnEventGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
        }

        private void OnPlayerGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
        }

        protected override void InitializePresenter()
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
            TestJudgeColumn.AutoComplete = true;

            TestTypeColumn.DataPropertyName = "TestName";

            PlayerNameColumn.DataSource = CurrentPresenter.AviablePlayerList;
            PlayerNameColumn.DisplayMember = "Text";
            PlayerNameColumn.ValueMember = "Value";
            PlayerNameColumn.DataPropertyName = "PlayerId";
            PlayerNameColumn.AutoComplete = true;

            EventTestDataGridView.EditingControlShowing += OnCellEdit;
            PlayerEventDataGridView.EditingControlShowing += OnCellEdit;
        }

        void OnCellEdit(object s, DataGridViewEditingControlShowingEventArgs e)
        {
            var comboBox = e.Control as DataGridViewComboBoxEditingControl;
            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }
    }

}
