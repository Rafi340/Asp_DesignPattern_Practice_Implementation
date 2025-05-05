using Demo.Domain;
using Demo.Domain.Repository;
using Demo.Domain.Utilities;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Utilities;
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
        protected ISqlUtility SqlUtility { get; private set; }

        public UnitOfWork(DbContext context) 
        {
            _dbContext = context;
            SqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());

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
