using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GTO.Model.Context;
using GTO.Model.Enums;

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

        public void AddOrUpdate(Player player)
        {
            if (player.Id > 0)
            {
                _context.Entry(player).State = EntityState.Modified;
            }
            else
            {
                _context.Players.Add(player);
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Player GetOrCreate(int playerId)
        {
            return _context.Players.Find(playerId) ?? Create();
        }

        public void Refresh(int entityId)
        {
            var refreshEntity = _context.Players.Find(entityId);
            if (refreshEntity != null)
            {
                _context.Entry(refreshEntity).Reload();
            }
        }

        public IQueryable<IGrouping<int, GtoEventPlayerRecord>> GetGroupedPlayerRecords(int playerId)
        {
            return
                _context.GtoEventPlayerRecords
                    .Include(epr => epr.GtoEventTest)
                    .Include(epr => epr.GtoEventTest.Test)
                    .Where(epr => epr.GtoEventPlayer.PlayerId == playerId)
                    .Where(rg => rg.ResultRank != ResultRank.NoRank)
                    .GroupBy(gepr => gepr.GtoEventTest.TestId);
        }

        public AgeGroup GetplayerAgeGroup(int playerAge)
        {
            return _context.AgeGroups
                .Include(ag => ag.TestGroups)
                .Include(ag => ag.TestGroups.Select(tg => tg.Test))
                .FirstOrDefault(ag => ag.Max >= playerAge && ag.Min <= playerAge);
        }
    }
}