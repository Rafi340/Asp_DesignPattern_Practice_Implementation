using Demo.Application;
using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using Demo.Domain.Repository;
using Demo.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork , IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context, IBookRepository bookRepository, IAuthorRepository authorRepository) : base(context)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
        }
        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }

        public async Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search)
        {
            var procedureName = "GetAuthors";

            var result = await SqlUtility.QueryWithStoredProcedureAsync<Author>(procedureName,
                new Dictionary<string, object>
                {
                    { "PageIndex", pageIndex },
                    { "PageSize", pageSize },
                    { "OrderBy", order },
                    { "RatingFrom", search.RatingFrom },
                    { "RatingTo", search.RatingTo },
                    { "Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    { "Biography", string.IsNullOrEmpty(search.Biography) ? null : search.Biography }
                },
                new Dictionary<string, Type>
                {
                    { "Total", typeof(int) },
                    { "TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}
