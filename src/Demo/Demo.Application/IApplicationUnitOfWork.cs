using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using Demo.Domain.Repository;

namespace Demo.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }

        Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search);
    }
}
