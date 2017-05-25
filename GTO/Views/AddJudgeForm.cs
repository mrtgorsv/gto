using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GTO.Presenters;
using GTO.Properties;

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

            FirstNameTextBox.Validating += OnFirstNameValidating;
            LastNameTextBox.Validating += OnLastNameValidating;
            RegAddressTextBox.Validating += OnRegAddressValidating;
            BirthDateDatePicker.Validating += OnBirthDateDatePickerValidating;

            FirstNameTextBox.Validated += OnFirstNameValidated;
            LastNameTextBox.Validated += OnLastNameValidated;
            RegAddressTextBox.Validated += OnRegAddressValidated;
            BirthDateDatePicker.Validated += OnBirthDateDatePickerValidated;
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
                if (!ValidateChildren() || !Validate())
                {
                    return;
                }

                CurrentPresenter.EditableObject.Name = FirstNameTextBox.Text;
                CurrentPresenter.EditableObject.LastName = LastNameTextBox.Text;
                CurrentPresenter.EditableObject.MiddleName = MiddleNameTextBox.Text;

                CurrentPresenter.EditableObject.BirthDate = BirthDateDatePicker.Value;

                CurrentPresenter.EditableObject.PassCode = PassCodeTextBox.Text;
                CurrentPresenter.EditableObject.PassNumber = Convert.ToInt32(PassNumberUpDown.Value);
                CurrentPresenter.EditableObject.PassSerial = Convert.ToInt16(PassSerialUpDown.Value);
                CurrentPresenter.EditableObject.PassDate = PassDateDatePicker.Value;
                CurrentPresenter.EditableObject.PassLocation = PassLocationTextBox.Text;

                CurrentPresenter.EditableObject.Category = CategoryTextBox.Text;
                CurrentPresenter.EditableObject.Sex = Convert.ToInt16(SexComboBox.SelectedValue);

                CurrentPresenter.EditableObject.RegistrationAddress = RegAddressTextBox.Text;
                CurrentPresenter.EditableObject.HomeAddress = LocalAddressTextBox.Text;

                CurrentPresenter.Save();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, Resources.SaveErrorMessage + exception.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }


        #region Validation

        private void OnBirthDateDatePickerValidated(object sender, EventArgs e)
        {
            FormErrorProvider.SetError(BirthDateDatePicker, "");
        }

        private void OnRegAddressValidated(object sender, EventArgs e)
        {
            FormErrorProvider.SetError(RegAddressTextBox, "");
        }

        private void OnLastNameValidated(object sender, EventArgs e)
        {
            FormErrorProvider.SetError(LastNameTextBox, "");
        }

        private void OnFirstNameValidated(object sender, EventArgs e)
        {
            FormErrorProvider.SetError(FirstNameTextBox, "");
        }

        private void OnBirthDateDatePickerValidating(object sender, CancelEventArgs e)
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDateDatePicker.Value.Year;
            if (BirthDateDatePicker.Value > today.AddYears(-age))
                age--;
            if (age < 18 || age > 79)
            {
                FormErrorProvider.SetError(BirthDateDatePicker, "Возраст не подходит для участия");
                e.Cancel = true;
            }
        }

        private void OnRegAddressValidating(object sender, CancelEventArgs e)
        {
            string errorMessage = null;
            if (string.IsNullOrWhiteSpace(RegAddressTextBox.Text))
            {
                errorMessage = "Адрес регистрация обязателен для заполнения";
            }
            if (errorMessage != null)
            {
                FormErrorProvider.SetError(RegAddressTextBox, errorMessage);
                e.Cancel = true;
            }
        }

        private void OnLastNameValidating(object sender, CancelEventArgs e)
        {
            string errorMessage = null;
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                errorMessage = "Фамилия обязательна для заполнения";
            }
            else if (LastNameTextBox.Text.Any(char.IsDigit))
            {
                errorMessage = "Фамилия не может содержать символы";
            }
            if (errorMessage != null)
            {
                FormErrorProvider.SetError(LastNameTextBox, errorMessage);
                e.Cancel = true;
            }
        }

        private void OnFirstNameValidating(object sender, CancelEventArgs e)
        {
            string errorMessage = null;
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                errorMessage = "Имя обязательна для заполнения";
            }
            else if (FirstNameTextBox.Text.Any(char.IsDigit))
            {
                errorMessage = "Имя не может содержать символы";
            }
            if (errorMessage != null)
            {
                FormErrorProvider.SetError(FirstNameTextBox, errorMessage);
                e.Cancel = true;
            }
        }

        #endregion
    }
}