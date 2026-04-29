using PDIDatabase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIDatabase.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> SaveAsync();
    }
}
