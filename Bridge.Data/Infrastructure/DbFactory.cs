using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        BridgeDbContext dbContext;

        public BridgeDbContext Init()
        {
            return dbContext ?? (dbContext = new BridgeDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
