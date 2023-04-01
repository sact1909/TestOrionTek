using System;
using System.Collections.Generic;
using System.Text;
using TestOrionTek.DatabaseSettings.Entities;

namespace TestOrionTek.DatabaseSettings.DbServices.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Employees> Employees { get; }
    }
}
