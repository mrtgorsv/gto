using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GTO.Presenters.Players;
using GTO.Properties;
using GTO.Views.Intefaces;

namespace GTO.Views.Players
{
    public partial class EditPlayerForm : Form , IEditForm
    {

        protected PlayerPresenter CurrentPresenter { get; set; }

        private readonly int _playerId;

        public int EditableObjectId { get { return _playerId; } }

        public EditPlayerForm(int playerId = 0)
        {
            InitializeComponent();
            _playerId = playerId;
            SaveButton.Click += OnSaveButtonClicked;
            Closed += OnFormClosed;
            Load += OnFormLoaded;
            PassNumberUpDown.Maximum = 999999;
            PassSerialUpDown.Maximum = 9999;

            FirstNameTextBox.Validating += OnFirstNameValidating;
            LastNameTextBox.Validating += OnLastNameValidating;
            RegAddressTextBox.Validating += OnRegAddressValidating;
            BirthDateDatePicker.Validating += OnBirthDateDatePickerValidating;
            FirstNameTextBox.Validated += OnFirstNameValidated;
            LastNameTextBox.Validated += OnLastNameValidated;
            RegAddressTextBox.Validated += OnRegAddressValidated;
            BirthDateDatePicker.Validated += OnBirthDateDatePickerValidated;
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

        private void OnFormLoaded(object sender, EventArgs e)
        {
            CurrentPresenter = new PlayerPresenter(_playerId);
            InitializeFields();
        }

        private void InitializeFields()
        {
            FirstNameTextBox.Text = CurrentPresenter.EditableObject.Name;
            LastNameTextBox.Text = CurrentPresenter.EditableObject.LastName;
            MiddleNameTextBox.Text = CurrentPresenter.EditableObject.MiddleName;

            if (CurrentPresenter.EditableObject.BirthDate != DateTime.MinValue)
            {
                BirthDateDatePicker.Value = CurrentPresenter.EditableObject.BirthDate;
            }

            PassCodeTextBox.Text = CurrentPresenter.EditableObject.PassCode;

            if (CurrentPresenter.EditableObject.PassNumber.HasValue)
            {
                PassNumberUpDown.Value = CurrentPresenter.EditableObject.PassNumber.Value;
            }
            if (CurrentPresenter.EditableObject.PassSerial.HasValue)
            {
                PassSerialUpDown.Value = CurrentPresenter.EditableObject.PassSerial.Value;
            }
            if (CurrentPresenter.EditableObject.PassDate.HasValue)
            {
                PassDateDatePicker.Value = CurrentPresenter.EditableObject.PassDate.Value;
            }

            PassLocationTextBox.Text = CurrentPresenter.EditableObject.PassLocation;

            SportCategoryTextBox.Text = CurrentPresenter.EditableObject.SportCategory;

            SexComboBox.DisplayMember = "Text";
            SexComboBox.ValueMember = "Value";
            SexComboBox.DataSource = CurrentPresenter.SexList;
            SexComboBox.SelectedValue = (int)CurrentPresenter.EditableObject.Sex;

            RegAddressTextBox.Text = CurrentPresenter.EditableObject.RegistrationAddress;
            LocalAddressTextBox.Text = CurrentPresenter.EditableObject.HomeAddress;

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

                CurrentPresenter.EditableObject.BirthDate = BirthDateDatePicker.Value;
                CurrentPresenter.EditableObject.Name = FirstNameTextBox.Text;
                CurrentPresenter.EditableObject.LastName = LastNameTextBox.Text;
                CurrentPresenter.EditableObject.MiddleName = MiddleNameTextBox.Text;

                CurrentPresenter.EditableObject.PassNumber = Convert.ToInt32(PassNumberUpDown.Value);
                CurrentPresenter.EditableObject.PassSerial = Convert.ToInt16(PassSerialUpDown.Value);
                CurrentPresenter.EditableObject.PassCode = PassCodeTextBox.Text;

                CurrentPresenter.EditableObject.PassDate = PassDateDatePicker.Value;
                CurrentPresenter.EditableObject.PassLocation = PassLocationTextBox.Text;

                CurrentPresenter.EditableObject.RegistrationAddress = RegAddressTextBox.Text;
                CurrentPresenter.EditableObject.HomeAddress = LocalAddressTextBox.Text;

                CurrentPresenter.EditableObject.SportCategory = SportCategoryTextBox.Text;
                CurrentPresenter.EditableObject.Sex = Convert.ToInt16(SexComboBox.SelectedValue);
                CurrentPresenter.Save();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, Resources.SaveErrorMessage + exception.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }
    }
}
