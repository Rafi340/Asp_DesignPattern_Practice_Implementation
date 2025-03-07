using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repository
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        void AddBook(Book book);
    }
}
