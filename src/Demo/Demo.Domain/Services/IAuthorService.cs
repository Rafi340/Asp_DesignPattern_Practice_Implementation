﻿using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Services
{
    public interface IAuthorService
    {
        void AddAuthor(Author author);
    }
}
