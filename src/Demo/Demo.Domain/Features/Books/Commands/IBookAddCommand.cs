using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Features.Books.Commands
{
    public interface IBookAddCommand
    {
        void Execute();
    }
}
