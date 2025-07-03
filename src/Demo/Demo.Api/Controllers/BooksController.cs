using Demo.Application.Features.Books.Queries;
using Demo.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowedSites")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IMediator mediator, ILogger<BooksController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet(Name ="GetBooks")]
        [Authorize (Policy ="ValidLogin")]
        public async Task<object> Post([FromBody] GetBooksQuery bookQuery)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(bookQuery);

                var result = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Title),
                                HttpUtility.HtmlEncode(record.AuthorName),
                                record.Price.ToString(),
                                record.PublishDate.ToShortDateString(),
                                record.Id.ToString()
                            }).ToArray()
                };

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem in getting books");
                return DataTables.EmptyResult;
            }
        }
    }
}
