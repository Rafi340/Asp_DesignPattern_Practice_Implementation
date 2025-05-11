using Demo.Domain.Entities;
using Demo.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid> , IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public void AddBook(Book book)
        {
            Add(book);
        }

        public List<Book> GetLatestBooks()
        {
            DateTime? date = DateTime.Now.AddDays(-365);
            return _dbContext.Book.Where(t => t.PublishDate < date).ToList();
        }
    }
}
