using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDICommon.DTOs
{
    public class UserDto
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string Slc { get; set; }
        public string Email { get; set; }
        public string AdminAccess { get; set; }
    }
}
