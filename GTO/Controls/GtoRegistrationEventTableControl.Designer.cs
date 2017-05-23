namespace GTO.Controls
{
    partial class GtoRegistrationEventTableControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EventTestDataGridView = new System.Windows.Forms.DataGridView();
            this.TestTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestJudgeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PlayerEventDataGridView = new System.Windows.Forms.DataGridView();
            this.PlayerNameColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PlayerEventGroupBox = new System.Windows.Forms.GroupBox();
            this.EventTestGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EventTestDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerEventDataGridView)).BeginInit();
            this.PlayerEventGroupBox.SuspendLayout();
            this.EventTestGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EventTestDataGridView
            // 
            this.EventTestDataGridView.AllowUserToAddRows = false;
            this.EventTestDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EventTestDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventTestDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TestTypeColumn,
            this.TestJudgeColumn});
            this.EventTestDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventTestDataGridView.Location = new System.Drawing.Point(3, 16);
            this.EventTestDataGridView.Name = "EventTestDataGridView";
            this.EventTestDataGridView.Size = new System.Drawing.Size(238, 445);
            this.EventTestDataGridView.TabIndex = 2;
            // 
            // TestTypeColumn
            // 
            this.TestTypeColumn.HeaderText = "Испытание";
            this.TestTypeColumn.Name = "TestTypeColumn";
            this.TestTypeColumn.ReadOnly = true;
            // 
            // TestJudgeColumn
            // 
            this.TestJudgeColumn.HeaderText = "Судья";
            this.TestJudgeColumn.Name = "TestJudgeColumn";
            // 
            // PlayerEventDataGridView
            // 
            this.PlayerEventDataGridView.AllowUserToOrderColumns = true;
            this.PlayerEventDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PlayerEventDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlayerEventDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlayerNameColumn});
            this.PlayerEventDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayerEventDataGridView.Location = new System.Drawing.Point(3, 16);
            this.PlayerEventDataGridView.Name = "PlayerEventDataGridView";
            this.PlayerEventDataGridView.Size = new System.Drawing.Size(238, 445);
            this.PlayerEventDataGridView.TabIndex = 0;
            // 
            // PlayerNameColumn
            // 
            this.PlayerNameColumn.HeaderText = "ФИО";
            this.PlayerNameColumn.Name = "PlayerNameColumn";
            // 
            // PlayerEventGroupBox
            // 
            this.PlayerEventGroupBox.Controls.Add(this.PlayerEventDataGridView);
            this.PlayerEventGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayerEventGroupBox.Location = new System.Drawing.Point(3, 3);
            this.PlayerEventGroupBox.Name = "PlayerEventGroupBox";
            this.PlayerEventGroupBox.Size = new System.Drawing.Size(244, 464);
            this.PlayerEventGroupBox.TabIndex = 1;
            this.PlayerEventGroupBox.TabStop = false;
            this.PlayerEventGroupBox.Text = "Участники";
            // 
            // EventTestGroupBox
            // 
            this.EventTestGroupBox.Controls.Add(this.EventTestDataGridView);
            this.EventTestGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventTestGroupBox.Location = new System.Drawing.Point(253, 3);
            this.EventTestGroupBox.Name = "EventTestGroupBox";
            this.EventTestGroupBox.Size = new System.Drawing.Size(244, 464);
            this.EventTestGroupBox.TabIndex = 3;
            this.EventTestGroupBox.TabStop = false;
            this.EventTestGroupBox.Text = "Испытания";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.PlayerEventGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.EventTestGroupBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SaveButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveButton.Location = new System.Drawing.Point(253, 473);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(244, 24);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // GtoRegistrationEventTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GtoRegistrationEventTableControl";
            this.Size = new System.Drawing.Size(500, 500);
            ((System.ComponentModel.ISupportInitialize)(this.EventTestDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerEventDataGridView)).EndInit();
            this.PlayerEventGroupBox.ResumeLayout(false);
            this.EventTestGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PlayerEventDataGridView;
        private System.Windows.Forms.GroupBox PlayerEventGroupBox;
        private System.Windows.Forms.GroupBox EventTestGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn PlayerNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestTypeColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn TestJudgeColumn;
        private System.Windows.Forms.DataGridView EventTestDataGridView;
    }
}
