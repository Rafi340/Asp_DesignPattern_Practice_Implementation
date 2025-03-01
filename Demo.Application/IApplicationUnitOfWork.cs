using Demo.Domain;
using Demo.Domain.Repository;

namespace Demo.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
    }
}
