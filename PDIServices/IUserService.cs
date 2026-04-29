using PDICommon.DTOs;
using PDIEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> Search(UserDto filter);
        Task<IEnumerable<User>> GetAllAsync(string? empId);
        Task Create(UserDto dto);
        Task Update(string empId, UserDto dto);
        Task Delete(string empId);
    }
}
