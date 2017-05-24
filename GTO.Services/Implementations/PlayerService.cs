using System;
using System.Collections.Generic;
using System.Linq;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class PlayerService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        public Player Create()
        {
            return _context.Players.Create();
        }

        public List<Player> GetAll()
        {
            return _context.Players.ToList();
        }

        public void Add(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}