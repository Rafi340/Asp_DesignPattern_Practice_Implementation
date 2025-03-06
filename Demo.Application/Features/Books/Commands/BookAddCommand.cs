using Demo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Books.Commands
{
    public class BookAddCommand : IRequest
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
    }
}
