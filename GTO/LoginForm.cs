using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTO
{
    public partial class LoginForm : Form
    {
        private MainForm formMain = new MainForm();

        public LoginForm()
        {
            InitializeComponent();
            AddOwnedForm(formMain);
        }

        const string aLogin = "GTO";
        const string aPassword = "password";

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm();
            aForm.ShowDialog(this);
            aForm.Dispose();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text != "" && txtPassword.Text != "")
            {
                if (txtLogin.Text == aLogin && txtPassword.Text == aPassword)
                {
                    this.Hide();
                    formMain.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Логин/пароль пользователя не подтвержден!!!", "Сообщение...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Введите логин/пароль пользователя!!!", "Сообщение...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
