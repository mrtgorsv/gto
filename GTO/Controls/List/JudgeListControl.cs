using System;
using GTO.Presenters.Judges;
using GTO.Views.Judges;

namespace GTO.Controls.List
{
    public partial class JudgeListControl : ListControlBase
    {
        public JudgeListControl()
        {
            AddButton.Visible = false;
            InitializeComponent();
        }

        protected override void OnControlLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new JudgeListPresenter();
            base.OnControlLoad(sender , e);
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            ShowEditForm(new EditJudgeForm(CurrentPresenter.SelectedEntityId));
        }
    }
}
