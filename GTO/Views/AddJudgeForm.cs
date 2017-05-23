using System;
using System.Windows.Forms;
using GTO.Presenters;

namespace GTO.Views
{
    public partial class AddJudgeForm : Form
    {
        protected JudgePresenter CurrentPresenter { get; set; }

        public AddJudgeForm()
        {
            InitializeComponent();

            SaveButton.Click += OnSaveButtonClicked;
            Closed += OnFormClosed;
            Load += OnFormLoaded;
        }

        private void OnFormLoaded(object sender, EventArgs e)
        {
            CurrentPresenter = new JudgePresenter();

            SexComboBox.DisplayMember = "Text";
            SexComboBox.ValueMember = "Value";
            SexComboBox.DataSource = CurrentPresenter.SexList;
            SexComboBox.SelectedIndex = 0;
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            CurrentPresenter.Dispose();
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                CurrentPresenter.EditableObject.BirthDate = BirthDateDatePicker.Value;
                CurrentPresenter.EditableObject.Name = FirstNameTextBox.Text;
                CurrentPresenter.EditableObject.LastName = LastNameTextBox.Text;
                CurrentPresenter.EditableObject.MiddleName = MiddleNameTextBox.Text;
                CurrentPresenter.EditableObject.HomeAddress = AddressTextBox.Text;
                CurrentPresenter.EditableObject.PassCode = PassCodeTextBox.Text;
                CurrentPresenter.EditableObject.PassNumber = Convert.ToInt32(PassNumberTextBox.Text);
                CurrentPresenter.EditableObject.PassSerial = Convert.ToInt16(PassSerialTextBox.Text);
                CurrentPresenter.EditableObject.PassDate = PassDateDatePicker.Value;
                CurrentPresenter.EditableObject.PassLocation = PassLocationTextBox.Text;
                CurrentPresenter.EditableObject.RegistrationAddress = RegAddressTextBox.Text;
                CurrentPresenter.EditableObject.Category = CategoryTextBox.Text;
                CurrentPresenter.EditableObject.Sex = (short)SexComboBox.SelectedValue;
                CurrentPresenter.Save();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, "Ошибка при сохранении:" + exception.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

    }
}
