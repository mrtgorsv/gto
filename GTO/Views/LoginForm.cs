using System;
using System.Windows.Forms;
using GTO.Presenters;

namespace GTO.Views
{
    public partial class LoginForm : Form
    {
        protected LoginPresenter CurrentPresenter { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new LoginPresenter();
        }


        private void OnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm();
            aForm.ShowDialog(this);
            aForm.Dispose();
        }

        private void OnLoginClick(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин/пароль пользователя!!!", "Сообщение...", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!CurrentPresenter.Login(LoginTextBox.Text, PasswordTextBox.Text))
            {
                MessageBox.Show("Логин/пароль пользователя не подтвержден!!!", "Сообщение...", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
            Close();
        }
    }
}