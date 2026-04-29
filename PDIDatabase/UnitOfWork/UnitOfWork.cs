using DocumentFormat.OpenXml.Spreadsheet;
using PDIDatabase.Data;
using PDIDatabase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIDatabase.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PDItDbContext _context;

        public IUserRepository Users { get; }

        public UnitOfWork(PDItDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
