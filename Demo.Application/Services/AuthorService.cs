using Demo.Domain.Entities;
using Demo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public AuthorService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork; 
        }

        public void AddAuthor(Author author)
        {
            _applicationUnitOfWork.AuthorRepository.Add(author);
            _applicationUnitOfWork.Save();
            
        }

        
    }
}
