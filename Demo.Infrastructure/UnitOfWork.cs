using Demo.Domain;
using Demo.Domain.Repository;
using Demo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
      
        public UnitOfWork(DbContext context) 
        {
            _dbContext = context;

        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}
