using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
