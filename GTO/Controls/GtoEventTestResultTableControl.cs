using System;
using System.Windows.Forms;
using GTO.Model.Context;
using GTO.Presenters;
using GTO.Properties;

namespace GTO.Controls
{
    public partial class GtoEventTestResultTableControl : UserControlBase
    {
        public GtoEventTestResultPresenter CurrentPresenter { get; set; }

        public GtoEventTestResultTableControl()
        {
            InitializeComponent();
            SaveButton.Click += OnSaveButtonClick;

            GtoEventTestResultDataGrid.DataError += OnError;
            GtoEventTestResultDataGrid.EditingControlShowing += OnCellEdit;
            GtoEventTestResultDataGrid.CellBeginEdit += OnCellEditBegin;
        }

        private void OnCellEditBegin(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                
                DataGridViewComboBoxCell gtoTestCell = GtoEventTestResultDataGrid[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell;
                if (gtoTestCell != null)
                {
                    if (gtoTestCell.Items.Count == 0)
                    {
                        gtoTestCell.DisplayMember = "Text";
                        gtoTestCell.ValueMember = "Value";
                        gtoTestCell.AutoComplete = true;
                        GtoEventPlayerRecord record = gtoTestCell.OwningRow.DataBoundItem as GtoEventPlayerRecord;
                        gtoTestCell.DataSource = new BindingSource(CurrentPresenter.GetAviableEventTests(record), null);
                    }
                }
            }
        }


        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            try
            {
                CurrentPresenter.Save();
                OnNeedClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.SaveException);
            }
        }

        protected override void InitializePresenter()
        {
            CurrentPresenter = new GtoEventTestResultPresenter();
            GtoEventTestResultDataGrid.AutoGenerateColumns = false;
            GtoEventTestResultDataGrid.DataSource = CurrentPresenter.EventPlayerRecordDataSource;

            GtoEventTestColumn.DataPropertyName = "GtoEventTestId";
            GtoEventTestColumn.DisplayMember = "Text";
            GtoEventTestColumn.ValueMember = "Value";

            GtoEventTestJudgeColumn.DataPropertyName = "EventTestJudgeName";
            GtoEventPlayerColumn.DataPropertyName = "EventTestPlayerName";

            GtoEventTestResultDataGrid.CellEndEdit += OnCellEditComplete;
        }

        private void OnCellEditComplete(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var column = GtoEventTestResultDataGrid[e.ColumnIndex , e.RowIndex];
                var record = column.OwningRow.DataBoundItem as GtoEventPlayerRecord;
                if (record != null)
                {
                    CurrentPresenter.UpdateRecordTest(record);
                    GtoEventTestResultDataGrid.Update();
                    GtoEventTestResultDataGrid.Refresh();
                }
            }
        }

        private void OnError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
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
