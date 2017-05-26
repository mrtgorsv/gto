using System;
using System.Windows.Forms;
using GTO.Controls;
using GTO.Controls.List;
using GTO.Utils;
using GTO.Views;
using GTO.Views.Judges;
using GTO.Views.Players;

namespace GTO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (CurrentPrincipal.Instance.IsAdmin)
            {
                AdminMenu.Visible = true;
            }

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Load += OnLoad;
            CloseMenuItem.Click += OnCloseMenuItemClick;
            AboutMenuItem.Click += OnAboutMenuItemClick;
            ReportMenuItem.Click += OnReportMenuItemClick;
            GtoEventMenuItem.Click += OnGtoEventMenuItemClick;
            GtoEventTestMenuItem.Click += OnGtoEventTestMenuItemClick;
            AddJudgeMenuItem.Click += OnAddJudgeMenuItemClick;
            AddPlayerMenuItem.Click += OnAddPlayerMenuItemClick;

            if (CurrentPrincipal.Instance.IsAdmin)
            {
                UserMenuItem.Click += OnUserMenuItemClick;
                PlayerMenuItem.Click += OnPlayerMenuItemClick;
                JudgeMenuItem.Click += OnJudgeMenuItemClick;
            }

        }

        #region Event handlers

        private void OnJudgeMenuItemClick(object sender, EventArgs e)
        {
            ShowForm(new JudgeListControl());
        }

        private void OnPlayerMenuItemClick(object sender, EventArgs e)
        {
            ShowForm(new PlayerListControl());
        }

        private void OnUserMenuItemClick(object sender, EventArgs e)
        {
            ShowForm(new UserListControl());
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        private void OnCloseMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnAboutMenuItemClick(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm();
            aForm.ShowDialog(this);
            aForm.Dispose();
        }

        private void OnAddPlayerMenuItemClick(object sender, EventArgs e)
        {
            var addPlayerForm = new EditPlayerForm();
            addPlayerForm.ShowDialog(this);
            addPlayerForm.Dispose();
        }

        private void OnAddJudgeMenuItemClick(object sender, EventArgs e)
        {
            var addJudgeForm = new EditJudgeForm();
            addJudgeForm.ShowDialog(this);
            addJudgeForm.Dispose();
        }

        private void OnGtoEventTestMenuItemClick(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var regForm = new GtoRegistrationEventTableControl();
            ContentPanel.Controls.Add(regForm);
            ShowForm(regForm);
        }

        private void OnGtoEventMenuItemClick(object sender, EventArgs e)
        {
            var resultForm = new GtoEventTestResultTableControl();
            ShowForm(resultForm);
        }

        private void OnReportMenuItemClick(object sender, EventArgs e)
        {
            ContentPanel.Controls.Clear();
            var reportForm = new ReportForm();
            reportForm.ShowDialog(this);
        }

        private void OnNeedClose(object sender, EventArgs args)
        {
            var userControl = ContentPanel.Controls[0] as UserControlBase;
            if (userControl != null)
            {
                userControl.NeedClose -= OnNeedClose;
            }
            ContentPanel.Controls.Clear();
        }
        #endregion

        private void ShowForm(UserControlBase userControl)
        {
            ContentPanel.Controls.Clear();
            userControl.NeedClose += OnNeedClose;
            userControl.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(userControl);
        }
    }
}