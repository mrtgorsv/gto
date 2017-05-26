using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTO.Presenters;
using GTO.Views.Intefaces;

namespace GTO.Controls.List
{
    public partial class ListControlBase : UserControlBase
    {
        protected ListPresenterBase CurrentPresenter { get; set; }

        public ListControlBase()
        {
            InitializeComponent();

            EntityDataGrid.ReadOnly = true;

            AddButton.Click += OnAddButtonClick;
            EditButton.Click += OnEditButtonClick;
            Load += OnControlLoad;
            EntityDataGrid.SelectionChanged += OnEntityDataGridSelectionChanged;
        }

        private void OnEntityDataGridSelectionChanged(object sender, EventArgs e)
        {
            CurrentPresenter.SelectedEntity = EntityDataGrid.SelectedRows.Count  > 0 
                ? EntityDataGrid.SelectedRows[0].DataBoundItem 
                : null;

            AddButton.Enabled = CurrentPresenter.SelectedEntity != null;
            EditButton.Enabled = CurrentPresenter.SelectedEntity != null;
        }

        protected virtual void OnControlLoad(object sender, EventArgs e)
        {
            EntityDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EntityDataGrid.AutoGenerateColumns = false;

            foreach (KeyValuePair<string, string> column in CurrentPresenter.EntityCloumns)
            {
                DataGridViewColumn gridColumn = new DataGridViewTextBoxColumn
                {
                    HeaderText = column.Value,
                    DataPropertyName = column.Key,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                EntityDataGrid.Columns.Add(gridColumn);
            }

            EntityDataGrid.DataSource = CurrentPresenter.DataSource;
        }

        protected virtual void OnEditButtonClick(object sender, EventArgs e)
        {
        }

        protected virtual void OnAddButtonClick(object sender, EventArgs e)
        {
        }

        protected void RefreshDataSource()
        {
            CurrentPresenter.RefreshDataSource();
            EntityDataGrid.DataSource = CurrentPresenter.DataSource;
            EntityDataGrid.Update();
            EntityDataGrid.Refresh();
        }

        protected void ShowEditForm(Form form)
        {
            form.Closed += OnEditFormClosed;
            form.ShowDialog(this);
        }

        protected void OnEditFormClosed(object sender, EventArgs e)
        {
            if (sender is IEditForm)
            {
                CurrentPresenter.RefreshEntity((sender as IEditForm).EditableObjectId);
            }
            RefreshDataSource();
        }
    }
}
