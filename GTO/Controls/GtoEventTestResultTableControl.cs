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
            var cell = GtoEventTestResultDataGrid[e.ColumnIndex, e.RowIndex];
            if (cell.OwningColumn.Name.Equals(GtoEventTestColumn.Name))
            {
                DataGridViewComboBoxCell gtoTestCell = cell as DataGridViewComboBoxCell;
                if (gtoTestCell != null)
                {
                    GtoEventPlayerRecord record = gtoTestCell.OwningRow.DataBoundItem as GtoEventPlayerRecord;
                    var aviableTests = CurrentPresenter.GetAviableEventTests(record);
                    if (gtoTestCell.Items.Count != aviableTests.Count)
                    {
                        gtoTestCell.DisplayMember = "Text";
                        gtoTestCell.ValueMember = "Value";
                        gtoTestCell.AutoComplete = true;
                        gtoTestCell.DataSource = new BindingSource(aviableTests, null);
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
            GtoEventTestColumn.DataSource = CurrentPresenter.GtoEventTestDataSource;

            GtoEventTestJudgeColumn.DataPropertyName = "EventTestJudgeName";
            GtoEventPlayerColumn.DataPropertyName = "EventTestPlayerName";
            GtoResultRankColumn.DataPropertyName = "ResultRankName";
            GtoEventTestResultColumn.DataPropertyName = "TestValue";
            

            GtoEventTestResultDataGrid.CellEndEdit += OnCellEditComplete;
        }

        private void OnCellEditComplete(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var cell = GtoEventTestResultDataGrid[e.ColumnIndex, e.RowIndex];
                var record = cell.OwningRow.DataBoundItem as GtoEventPlayerRecord;

                if (record == null)
                {
                    return;
                }

                if (cell.OwningColumn.Name.Equals(GtoEventTestColumn.Name))
                {
                    CurrentPresenter.UpdateRecordTest(record);
                }
                else if (cell.OwningColumn.Name.Equals(GtoEventTestResultColumn.Name))
                {
                    CurrentPresenter.UpdateRecordResult(record);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message , Resources.InputErrorMessage , MessageBoxButtons.OK);
            }

            GtoEventTestResultDataGrid.Update();
            GtoEventTestResultDataGrid.Refresh();
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
