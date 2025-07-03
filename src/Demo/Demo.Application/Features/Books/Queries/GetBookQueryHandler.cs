using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using MediatR;

namespace Demo.Application.Features.Books.Queries
{
    public class GetBookQueryHandler : IRequestHandler<GetBooksQuery, (IList<BookWithAuthorDto>, int, int)>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public GetBookQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<(IList<BookWithAuthorDto>, int, int)> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var procedureName = "GetBooks";

            var result = await _applicationUnitOfWork.SqlUtility
                .QueryWithStoredProcedureAsync<BookWithAuthorDto>(procedureName,
                new Dictionary<string, object?>
                {
                    { "PageIndex", request.PageIndex },
                    { "PageSize", request.PageSize },
                    { "OrderBy", request.FormatSortExpression(["Title", "AuthorName", "Price", "PublishDate"]) },
                    { "PublishDateFrom", request.SearchItem.PublishDateFrom },
                    { "PublishDateTo", request.SearchItem.PublishDateTo },
                    { "PriceFrom", request.SearchItem.PriceFrom },
                    { "PriceTo", request.SearchItem.PriceTo },
                    { "Title", string.IsNullOrEmpty(request.SearchItem.Title) ? null : request.SearchItem.Title },
                    { "AuthorName", string.IsNullOrEmpty(request.SearchItem.AuthorName) ? null : request.SearchItem.AuthorName }
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
