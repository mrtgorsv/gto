using System;
using System.Windows.Forms;
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
            GtoEventTestResultDataGrid.DataSource = CurrentPresenter.EventPlayerRecordDataSource;

            GtoEventTestColumn.DataSource = CurrentPresenter.EventTests;

            GtoEventTestColumn.DisplayMember = "EventTestName";
            GtoEventTestColumn.ValueMember = "Id";
            GtoEventTestColumn.ValueMember = "GtoEventTestId";

            GtoEventTestJudgeColumn.DataPropertyName = "EventTestJudgeName";
        }
    }
}
