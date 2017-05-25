using System;
using System.Collections.Generic;
using GTO.Models;
using GTO.Services.Implementations;

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
            return _userService.CheckLogin(login, password);
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