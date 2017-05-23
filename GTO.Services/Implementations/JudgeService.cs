using System;
using System.Collections.Generic;
using System.Linq;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class JudgeService : IDisposable
    {
        GtoDatabaseContext _context = new GtoDatabaseContext();

        public Judge Create()
        {
            return _context.Judges.Create();
        }

        public void Add(Judge judge)
        {
            _context.Judges.Add(judge);
            _context.SaveChanges();
        }

        public List<Judge> GetAll()
        {
            return _context.Judges.ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
