using System;
using System.Collections.Generic;
using System.Text;
using TestOrionTek.DatabaseSettings.DbServices.Abstract;
using TestOrionTek.DatabaseSettings.Entities;

namespace TestOrionTek.DatabaseSettings.DbServices
{
    public class UnitOfWork : IUnitOfWork
    {
        
        public UnitOfWork()
        {
            
        }
        public IRepository<Employees> Employees => new Repository<Employees>();
    }
}
