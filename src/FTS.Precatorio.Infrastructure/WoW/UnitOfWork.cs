using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;

namespace FTS.Precatorio.Infrastructure.Wow
{
    public class UnitOfWork : IUnitOfWork
    {
        private CoreContext _context;

        public UnitOfWork() { }

        public bool Commit()
        {
            if (_context == null) return false;

            var rowsAffected = _context.SaveChanges();
            return rowsAffected > 0;
        }

        public void ConfigContext(ICoreContext context)
        {
            _context = (CoreContext)context;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

    }
}