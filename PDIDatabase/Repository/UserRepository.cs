using Microsoft.EntityFrameworkCore;
using PDICommon.DTOs;
using PDIDatabase.Data;
using PDIEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIDatabase.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PDItDbContext _context;

        public UserRepository(PDItDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = _context.Users
                .FromSqlRaw("EXEC sp_GetUsers")
                .AsEnumerable()   // ✅ switch to client-side
                .ToList();        // ✅ now normal LINQ

            return users.Select(u => new UserDto
            {
                EmpId = u.EmpId,
                Name = u.Name,
                District = u.District,
                Country = u.Country,
                Slc = u.Slc,
                Email = u.Email,
                AdminAccess = u.AdminAccess
            });
        }
        public async Task<IEnumerable<UserDto>> Search(UserDto f)
        {
            return await _context.Users
                .FromSqlRaw("EXEC sp_SearchUsers @EmpId={0}, @Name={1}, @District={2}, @Country={3}, @Slc={4}, @Email={5}, @AdminAccess={6}",
                    f.EmpId, f.Name, f.District, f.Country, f.Slc, f.Email, f.AdminAccess)
                .Select(u => new UserDto
                {
                    EmpId = u.EmpId,
                    Name = u.Name,
                    District = u.District,
                    Country = u.Country,
                    Slc = u.Slc,
                    Email = u.Email,
                    AdminAccess = u.AdminAccess
                }).ToListAsync();
        }

        public async Task Create(UserDto dto)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateUser @EmpId={0}, @Name={1}, @District={2}, @Country={3}, @Slc={4}, @Email={5}, @AdminAccess={6}",
                dto.EmpId, dto.Name, dto.District, dto.Country,
                dto.Slc, dto.Email, dto.AdminAccess);
        }

        public async Task Update(UserDto dto)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateUser @EmpId={0}, @Name={1}, @District={2}, @Country={3}, @Slc={4}, @Email={5}, @AdminAccess={6}",
                dto.EmpId, dto.Name, dto.District, dto.Country,
                dto.Slc, dto.Email, dto.AdminAccess);
        }

        public async Task Delete(string empId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteUser @EmpId={0}", empId);
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? empId)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(empId))
            {
                query = query.Where(u => u.EmpId.Contains(empId));
            }
            return await query.ToListAsync();
        }

    }
}
