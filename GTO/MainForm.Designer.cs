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
            this.регистрацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddJudge = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTable = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCompetition = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReport = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.регистрацияToolStripMenuItem,
            this.btnTable,
            this.btnCompetition,
            this.btnReport,
            this.btnAbout,
            this.btnClose});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(749, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // регистрацияToolStripMenuItem
            // 
            this.регистрацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddPlayer,
            this.btnAddJudge});
            this.регистрацияToolStripMenuItem.Name = "регистрацияToolStripMenuItem";
            this.регистрацияToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.регистрацияToolStripMenuItem.Text = "Регистрация";
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(125, 22);
            this.btnAddPlayer.Text = "Участник";
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // btnAddJudge
            // 
            this.btnAddJudge.Name = "btnAddJudge";
            this.btnAddJudge.Size = new System.Drawing.Size(125, 22);
            this.btnAddJudge.Text = "Судья";
            this.btnAddJudge.Click += new System.EventHandler(this.btnAddJudge_Click);
            // 
            // btnTable
            // 
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(66, 20);
            this.btnTable.Text = "Таблица";
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnCompetition
            // 
            this.btnCompetition.Name = "btnCompetition";
            this.btnCompetition.Size = new System.Drawing.Size(116, 20);
            this.btnCompetition.Text = "К соревнованиям";
            this.btnCompetition.Click += new System.EventHandler(this.btnCompetition_Click);
            // 
            // btnReport
            // 
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(51, 20);
            this.btnReport.Text = "Отчет";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(94, 20);
            this.btnAbout.Text = "О программе";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 20);
            this.btnClose.Text = "Выход";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.Load += new System.EventHandler(this.OnLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem регистрацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAddPlayer;
        private System.Windows.Forms.ToolStripMenuItem btnAddJudge;
        private System.Windows.Forms.ToolStripMenuItem btnTable;
        private System.Windows.Forms.ToolStripMenuItem btnCompetition;
        private System.Windows.Forms.ToolStripMenuItem btnReport;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnClose;
        private System.Windows.Forms.Panel ContentPanel;
    }
}