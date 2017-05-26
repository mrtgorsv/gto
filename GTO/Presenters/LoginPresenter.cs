using System;
using System.Collections.Generic;
using GTO.Models;
using GTO.Services.Implementations;
using GTO.Utils;

namespace GTO.Presenters
{
    public class LoginPresenter : IDisposable
    {
        private readonly UserService _userService;

        public LoginPresenter()
        {
            _userService = new UserService();
        }

        public bool Login( string login , string password)
        {

            if (_userService.CheckLogin(login, password))
            {
                var user = _userService.GetByLogin(login);

                CurrentPrincipal.Instance.Id = user.Id;
                CurrentPrincipal.Instance.Login = user.Login;
                CurrentPrincipal.Instance.IsAdmin = user.Role != null && user.Role.IsAdmin;
                return true;
            }
            return false;

        }

        public List<ComboBoxItem> SexList => new List<ComboBoxItem>
        {
            new ComboBoxItem {Text = "Мужской" , Value = 1},
            new ComboBoxItem {Text = "Женский" , Value = 0}
        };

        public void Dispose()
        {
            _userService.Dispose();
        }
    }
}