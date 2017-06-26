using System;
using GTO.Model.Context;

namespace GTO.Services.Implementations
{
    public class AppService : IDisposable
    {
        readonly GtoDatabaseContext _context = new GtoDatabaseContext();



        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
