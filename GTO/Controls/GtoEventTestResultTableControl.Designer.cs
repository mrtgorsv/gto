namespace GTO.Controls
{
    partial class GtoEventTestResultTableControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GtoEventTestResultDataGrid = new System.Windows.Forms.DataGridView();
            this.GtoEventPlayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GtoEventTestColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GtoEventTestResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GtoEventTestJudgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GtoEventTestResultDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GtoEventTestResultDataGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SaveButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GtoEventTestResultDataGrid
            // 
            this.GtoEventTestResultDataGrid.AllowUserToAddRows = false;
            this.GtoEventTestResultDataGrid.AllowUserToDeleteRows = false;
            this.GtoEventTestResultDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GtoEventTestResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GtoEventTestResultDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GtoEventPlayer,
            this.GtoEventTestColumn,
            this.GtoEventTestResultColumn,
            this.GtoEventTestJudgeColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.GtoEventTestResultDataGrid, 2);
            this.GtoEventTestResultDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GtoEventTestResultDataGrid.Location = new System.Drawing.Point(3, 3);
            this.GtoEventTestResultDataGrid.Name = "GtoEventTestResultDataGrid";
            this.GtoEventTestResultDataGrid.RowHeadersVisible = false;
            this.GtoEventTestResultDataGrid.Size = new System.Drawing.Size(494, 469);
            this.GtoEventTestResultDataGrid.TabIndex = 0;
            // 
            // GtoEventPlayer
            // 
            this.GtoEventPlayer.HeaderText = "Участник";
            this.GtoEventPlayer.Name = "GtoEventPlayer";
            this.GtoEventPlayer.ReadOnly = true;
            // 
            // GtoEventTestColumn
            // 
            this.GtoEventTestColumn.HeaderText = "Соревнование";
            this.GtoEventTestColumn.Name = "GtoEventTestColumn";
            // 
            // GtoEventTestResultColumn
            // 
            this.GtoEventTestResultColumn.HeaderText = "Результат";
            this.GtoEventTestResultColumn.Name = "GtoEventTestResultColumn";
            // 
            // GtoEventTestJudgeColumn
            // 
            this.GtoEventTestJudgeColumn.HeaderText = "Судья";
            this.GtoEventTestJudgeColumn.Name = "GtoEventTestJudgeColumn";
            this.GtoEventTestJudgeColumn.ReadOnly = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveButton.Location = new System.Drawing.Point(253, 478);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(244, 19);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // GtoEventTestResultTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GtoEventTestResultTableControl";
            this.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GtoEventTestResultDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView GtoEventTestResultDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtoEventPlayer;
        private System.Windows.Forms.DataGridViewComboBoxColumn GtoEventTestColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtoEventTestResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtoEventTestJudgeColumn;
        private System.Windows.Forms.Button SaveButton;
    }
}
