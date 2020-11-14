using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
         
        private AppZerobDbContext dbContext;
        public UnitOfWork(AppZerobDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
       

        public AppZerobDbContext DbContext
        {
            get { return dbContext  ; }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
