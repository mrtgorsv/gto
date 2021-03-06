﻿using System;
using System.Windows.Forms;
using GTO.Models;
using GTO.Presenters;
using GTO.Properties;

namespace GTO.Views.Reports
{
    public partial class PlayerReportForm : Form
    {
        public PlayerReportPresenter CurrentPresenter { get; set; }

        public PlayerReportForm()
        {
            InitializeComponent();
            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new PlayerReportPresenter();
            PlayerComboBox.DisplayMember = "Text";
            PlayerComboBox.ValueMember = "Id";
            PlayerComboBox.SelectedIndexChanged += OnPlayerChanged;

            PlayerComboBox.DataSource = CurrentPresenter.PlayerList;
            ShowReportButton.Click += OnShowReportClick;
        }

        private void OnPlayerChanged(object sender, EventArgs e)
        {
            FileNameTextBox.Text = PlayerComboBox.Text;
        }

        private void OnShowReportClick(object sender, EventArgs e)
        {
            try
            {
                if (PlayerComboBox.SelectedItem != null)
                {
                    var selectedValue = (ComboBoxItem)PlayerComboBox.SelectedValue;
                    CurrentPresenter.ShowReport(selectedValue.Value, FileNameTextBox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.ErrorTitleMessage, MessageBoxButtons.OK);
            }
        }
    }
}
