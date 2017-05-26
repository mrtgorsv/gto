using System;
using System.ComponentModel;
using System.Windows.Forms;
using GTO.Presenters.Users;
using GTO.Properties;
using GTO.Views.Intefaces;

namespace GTO.Views.Users
{
    public partial class EditUserForm : Form , IEditForm
    {
        protected UserPresenter CurrentPresenter { get; set; }

        private readonly int _userId;

        public int EditableObjectId { get { return _userId; } }

        public EditUserForm(int userId = 0)
        {
            InitializeComponent();
            _userId = userId;

            SaveButton.Click += OnSaveButtonClicked;
            Closed += OnFormClosed;
            Load += OnFormLoaded;

            LoginTextBox.Validating += OnLoginValidating;
            PasswordTextBox.Validating += OnPasswordValidating;
        }

        private void OnFormLoaded(object sender, EventArgs e)
        {
            CurrentPresenter = new UserPresenter(_userId);
            InitializeFields();
        }
        private void InitializeFields()
        {
            LoginTextBox.Text = CurrentPresenter.EditableObject.Login;
            PasswordTextBox.Text = CurrentPresenter.EditableObject.Password;

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

                CurrentPresenter.EditableObject.Login = LoginTextBox.Text;
                CurrentPresenter.EditableObject.Password = PasswordTextBox.Text;

                CurrentPresenter.Save();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, Resources.SaveErrorMessage + exception.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }

        #region Validation

        private void OnLoginValidating(object sender, CancelEventArgs e)
        {
            string errorMessage = null;
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                errorMessage = "Логин обязателен для заполнения";
            }
            else if(!CurrentPresenter.ValidateLogin(LoginTextBox.Text))
            {
                errorMessage = "Логин уже использвется";
            }
            if (errorMessage != null)
            {
                FormErrorProvider.SetError(LoginTextBox, errorMessage);
                e.Cancel = true;
            }
        }

        private void OnPasswordValidating(object sender, CancelEventArgs e)
        {
            string errorMessage = null;
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            {
                errorMessage = "Пароль обязателен для заполнения";
            }
            if (errorMessage != null)
            {
                FormErrorProvider.SetError(PasswordTextBox, errorMessage);
                e.Cancel = true;
            }
        }


        #endregion
    }
}
