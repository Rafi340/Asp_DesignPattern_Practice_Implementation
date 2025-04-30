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
        bool IsNameDuplicate(string name, Guid? id = null);
        (IList<Author> data, int total, int totalDisplay) GetPagedAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        void Update(Author author);
    }
}
