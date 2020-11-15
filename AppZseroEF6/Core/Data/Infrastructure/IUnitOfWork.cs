using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppZseroEF6.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task<int> CommitAsync();

        int Commit();

        bool AutoDetectChanges { get; set; }

        bool CheckConnection();

        IDbContextTransaction BeginTransaction();
    }
     
}
