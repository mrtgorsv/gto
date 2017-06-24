namespace GTO
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddJudgeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GtoEventTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GtoEventMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MedalReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdminMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JudgeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegistrationToolStripMenuItem,
            this.GtoEventTestMenuItem,
            this.GtoEventMenuItem,
            this.отчетыToolStripMenuItem,
            this.AboutMenuItem,
            this.CloseMenuItem,
            this.AdminMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(749, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RegistrationToolStripMenuItem
            // 
            this.RegistrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPlayerMenuItem,
            this.AddJudgeMenuItem});
            this.RegistrationToolStripMenuItem.Name = "RegistrationToolStripMenuItem";
            this.RegistrationToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.RegistrationToolStripMenuItem.Text = "Регистрация";
            // 
            // AddPlayerMenuItem
            // 
            this.AddPlayerMenuItem.Name = "AddPlayerMenuItem";
            this.AddPlayerMenuItem.Size = new System.Drawing.Size(125, 22);
            this.AddPlayerMenuItem.Text = "Участник";
            // 
            // AddJudgeMenuItem
            // 
            this.AddJudgeMenuItem.Name = "AddJudgeMenuItem";
            this.AddJudgeMenuItem.Size = new System.Drawing.Size(125, 22);
            this.AddJudgeMenuItem.Text = "Судья";
            // 
            // GtoEventTestMenuItem
            // 
            this.GtoEventTestMenuItem.Name = "GtoEventTestMenuItem";
            this.GtoEventTestMenuItem.Size = new System.Drawing.Size(66, 20);
            this.GtoEventTestMenuItem.Text = "Таблица";
            // 
            // GtoEventMenuItem
            // 
            this.GtoEventMenuItem.Name = "GtoEventMenuItem";
            this.GtoEventMenuItem.Size = new System.Drawing.Size(116, 20);
            this.GtoEventMenuItem.Text = "К соревнованиям";
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayerReportMenuItem,
            this.MedalReportMenuItem,
            this.EventReportMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // PlayerReportMenuItem
            // 
            this.PlayerReportMenuItem.Name = "PlayerReportMenuItem";
            this.PlayerReportMenuItem.Size = new System.Drawing.Size(234, 22);
            this.PlayerReportMenuItem.Text = "\"Информация об участнике\"";
            // 
            // MedalReportMenuItem
            // 
            this.MedalReportMenuItem.Name = "MedalReportMenuItem";
            this.MedalReportMenuItem.Size = new System.Drawing.Size(234, 22);
            this.MedalReportMenuItem.Text = "\"Полученные медали\"";
            // 
            // EventReportMenuItem
            // 
            this.EventReportMenuItem.Name = "EventReportMenuItem";
            this.EventReportMenuItem.Size = new System.Drawing.Size(234, 22);
            this.EventReportMenuItem.Text = "\"Итоги соревнования\"";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(94, 20);
            this.AboutMenuItem.Text = "О программе";
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(53, 20);
            this.CloseMenuItem.Text = "Выход";
            // 
            // AdminMenu
            // 
            this.AdminMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserMenuItem,
            this.PlayerMenuItem,
            this.JudgeMenuItem});
            this.AdminMenu.Name = "AdminMenu";
            this.AdminMenu.Size = new System.Drawing.Size(134, 20);
            this.AdminMenu.Text = "Администрирование";
            this.AdminMenu.Visible = false;
            // 
            // UserMenuItem
            // 
            this.UserMenuItem.Name = "UserMenuItem";
            this.UserMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UserMenuItem.Text = "Пользователи";
            // 
            // PlayerMenuItem
            // 
            this.PlayerMenuItem.Name = "PlayerMenuItem";
            this.PlayerMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PlayerMenuItem.Text = "Участники";
            // 
            // JudgeMenuItem
            // 
            this.JudgeMenuItem.Name = "JudgeMenuItem";
            this.JudgeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.JudgeMenuItem.Text = "Судьи";
            // 
            // ContentPanel
            // 
            this.ContentPanel.BackgroundImage = global::GTO.Properties.Resources.ehCixrFnIvY;
            this.ContentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(0, 24);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(749, 466);
            this.ContentPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(749, 490);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTO";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RegistrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddJudgeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GtoEventTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GtoEventMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem AdminMenu;
        private System.Windows.Forms.ToolStripMenuItem UserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JudgeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayerReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MedalReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventReportMenuItem;
    }
}