using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using MediatR;
using Demo.Domain.Features.Books.Queries;

namespace Demo.Application.Features.Books.Queries
{
    public class GetBooksQuery : DataTables, IRequest<(IList<BookWithAuthorDto>, int, int)>, IGetBooksQuery
    {
        public BookSearchDto SearchItem { get; set; }
    }
}
