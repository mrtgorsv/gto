using System;
using GTO.Presenters.Players;
using GTO.Views.Players;

namespace GTO.Controls.List
{
    public partial class PlayerListControl : ListControlBase
    {
        public PlayerListControl()
        {
            AddButton.Visible = false;
            InitializeComponent();
        }

        protected override void OnControlLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new PlayerListPresenter();
            base.OnControlLoad(sender, e);
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            ShowEditForm(new EditPlayerForm(CurrentPresenter.SelectedEntityId));
        }
    }
}
