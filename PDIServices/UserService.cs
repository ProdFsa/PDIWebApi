using PDICommon.DTOs;
using PDIDatabase.UnitOfWork;
using PDIEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
            => await _uow.Users.GetAll();

        public async Task<IEnumerable<UserDto>> Search(UserDto filter)
            => await _uow.Users.Search(filter);

        public async Task Create(UserDto dto)
        {
            dto.EmpId = Guid.NewGuid().ToString();
            await _uow.Users.Create(dto);
        }

        public async Task Update(string empId, UserDto dto)
        {
            dto.EmpId = empId;
            await _uow.Users.Update(dto);
        }

        public async Task Delete(string empId)
        {
            await _uow.Users.Delete(empId);
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? empId)
        {
            return await _uow.Users.GetAllAsync(empId);
        }
    }
}
