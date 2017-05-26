using System;
using GTO.Presenters.Users;
using GTO.Views.Users;

namespace GTO.Controls.List
{
    public partial class UserListControl : ListControlBase
    {
        public UserListControl()
        {
            InitializeComponent();
        }

        protected override void OnControlLoad(object sender, EventArgs e)
        {
            CurrentPresenter = new UserListPresenter();
            base.OnControlLoad(sender, e);
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            ShowEditForm(new EditUserForm(CurrentPresenter.SelectedEntityId));
        }

        protected override void OnAddButtonClick(object sender, EventArgs e)
        {
            ShowEditForm(new EditUserForm());
        }
    }
}
