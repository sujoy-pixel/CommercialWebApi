using Dapper.Application.Interfaces;
using Dapper.Infrastructure.DapperContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext= dbContext;
            Products = new ProductRepository(_dbContext);
            Orders = new OrderRepository(_dbContext);
        }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get;}
       
    }
}
