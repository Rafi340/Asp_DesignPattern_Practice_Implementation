﻿using Demo.Application.Exceptions;
using Demo.Domain;
using Demo.Domain.Dtos;
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
            if (!_applicationUnitOfWork.AuthorRepository.IsNameDuplicate(author.Name))
            {
                _applicationUnitOfWork.AuthorRepository.Add(author);
                _applicationUnitOfWork.Save();
            }
            else
                throw new DuplicateAuthorNameException();

        }

        public void DeleteAuthor(Guid id)
        {
            _applicationUnitOfWork.AuthorRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public Author GetAuthor(Guid id)
        {
           return _applicationUnitOfWork.AuthorRepository.GetById(id);
        }

        public (IList<Author> data, int total, int totalDisplay) GetAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return _applicationUnitOfWork.AuthorRepository.GetPagedAuthors(pageIndex, pageSize, order, search);
        }

        public async Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorsSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search)
        {
            return await _applicationUnitOfWork.GetAuthorSP(pageIndex, pageSize, order, search);
        }

        public void Update(Author author)
        {
            if (!_applicationUnitOfWork.AuthorRepository.IsNameDuplicate(author.Name, author.Id))
            {
                _applicationUnitOfWork.AuthorRepository.Add(author);
                _applicationUnitOfWork.Save();
            }
            else
                throw new DuplicateAuthorNameException();
        }
    }
}
