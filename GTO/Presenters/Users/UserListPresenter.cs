using GTO.Model.Context;
using GTO.Services.Implementations;

namespace GTO.Presenters.Users
{
    public class UserListPresenter : ListPresenterBase
    {
        private readonly UserService _userService;

        public override int SelectedEntityId
        {
            get
            {
                return SelectedEntity != null
                    ? ((User) SelectedEntity).Id
                    : 0;
            }
        }

        public UserListPresenter()
        {
            _userService = new UserService();

            EntityCloumns.Add("Login", "Логин");
        }

        protected override object GetDataSource()
        {
            return _userService.GetAll();
        }

        public override void RefreshEntity(int entityId)
        {
            _userService.Refresh(entityId);
        }
    }
}