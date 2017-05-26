using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class JudgeService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();

        public Judge Create()
        {
            return _context.Judges.Create();
        }

        public void AddOrUpdate(Judge judge)
        {
            if (judge.Id > 0)
            {
                _context.Entry(judge).State = EntityState.Modified;
            }
            else
            {
                _context.Judges.Add(judge);
            }
            _context.SaveChanges();
        }

        public List<Judge> GetAll()
        {
            return _context.Judges.ToList();
        }

        public Judge GetOrCreate(int judgeId)
        {
            return _context.Judges.Find(judgeId) ?? Create();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Refresh(int entityId)
        {
            var refreshEntity = _context.Judges.Find(entityId);
            if (refreshEntity != null)
            {
                _context.Entry(refreshEntity).Reload();
            }
        }
    }
}
