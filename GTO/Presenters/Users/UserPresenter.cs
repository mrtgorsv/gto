using GTO.Model.Context;
using GTO.Services.Implementations;

namespace GTO.Presenters.Users
{
    public class UserPresenter : EditPresenterBase
    {
        public User EditableObject { get; set; }

        protected UserService UserService;

        public UserPresenter(int userId)
        {
            UserService = new UserService();
            EditableObject = UserService.GetOrCreate(userId);
        }

        public override void Dispose()
        {
            UserService.Dispose();
        }
        public override void Save()
        {
            UserService.AddOrUpdate(EditableObject);
        }

        public bool ValidateLogin(string login)
        {
           return UserService.ValidateLogin(login, EditableObject.Id);
        }
    }
}