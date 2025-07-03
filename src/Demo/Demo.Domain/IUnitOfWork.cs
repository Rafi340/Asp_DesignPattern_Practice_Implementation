using Demo.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        ISqlUtility SqlUtility { get; }
    }
}
