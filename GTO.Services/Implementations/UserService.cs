using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public User GetOrCreate(int userid)
        {
            return _context.Users.Find(userid) ?? Create();
        }
        public User Create()
        {
            return _context.Users.Create();
        }

        public void AddOrUpdate(User user)
        {
            if (user.Id != 0)
            {
               _context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                _context.Users.Add(user);
            }
            _context.SaveChanges();
        }

        public User GetByLogin(string login)
        {
            return _context.Users.First(u => u.Login.Equals(login));
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool ValidateLogin(string login, int id)
        {
            return !_context.Users.Any(u => u.Login.ToLower() == login.ToLower() && u.Id != id);
        }

        public void Refresh(int entityId)
        {
            var refreshEntity = _context.Users.Find(entityId);
            if (refreshEntity != null)
            {
                _context.Entry(refreshEntity).Reload();
            }
        }
    }
}