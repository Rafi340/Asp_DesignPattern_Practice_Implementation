using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repository
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
    }
}
