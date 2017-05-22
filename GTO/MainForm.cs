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
    public partial class MainForm : Form
    {

        private AddJudgeForm _addJudgeForm = new AddJudgeForm();
        private AddPlayerForm _addPlayerForm = new AddPlayerForm();

        public MainForm()
        {
            InitializeComponent();
            AddOwnedForm(_addJudgeForm);
            AddOwnedForm(_addPlayerForm);
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            tabPage1.Parent = null;
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = tabControl1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm();
            aForm.ShowDialog(this);
            aForm.Dispose();
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            _addPlayerForm.ShowDialog(this);
        }

        private void btnAddJudge_Click(object sender, EventArgs e)
        {
            _addJudgeForm.ShowDialog(this);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            tabPage1.Parent = tabControl1;
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = null;
        }

        private void btnCompetition_Click(object sender, EventArgs e)
        {
            tabPage1.Parent = null;
            tabPage2.Parent = tabControl1;
            tabPage3.Parent = null;
            tabPage4.Parent = null;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            tabPage1.Parent = null;
            tabPage2.Parent = null;
            tabPage3.Parent = tabControl1;
            tabPage4.Parent = null;
        }


    }
}
