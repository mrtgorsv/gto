using System;
using System.Linq;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class UserService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        public bool CheckLogin(string login, string password)
        {
            return _context.Users.Any(u=> u.Login == login && u.Password == password);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}