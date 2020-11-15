using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppZseroEF6.Data.Infrastructure
{
  

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppZerobDbContext _context;
        private readonly List<object> _repositories = new List<object>();

        private bool _disposed;

        public UnitOfWork(AppZerobDbContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            var repo = (IRepository<T>)_repositories.SingleOrDefault(r => r is IRepository<T>);
            if (repo == null) _repositories.Add(repo = new RepositoryBase<T>(_context));

            return repo;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool AutoDetectChanges
        {
            get => _context.ChangeTracker.AutoDetectChangesEnabled;
            set => _context.ChangeTracker.AutoDetectChangesEnabled = value;
        }

        public bool CheckConnection()
        {
            try
            {
                _context.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException as SqlException;

                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                    throw new  Exception("ConflictException");
                throw;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }


        private void Dispose(bool isDisposing)
        {
            if (!_disposed)
                if (isDisposing)
                    _context?.Dispose();

            _disposed = true;
        }
    }
}
