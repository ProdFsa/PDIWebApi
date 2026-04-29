using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDICommon.DTOs;
using PDIServices;

namespace PDIWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
            => Ok(await _service.GetAll());

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] UserDto filter)
            => Ok(await _service.Search(filter));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            await _service.Create(dto);
            return Ok();
        }

        [HttpPut("{empId}")]
        public async Task<IActionResult> Update(string empId, UserDto dto)
        {
            await _service.Update(empId, dto);
            return NoContent();
        }

        [HttpDelete("{empId}")]
        public async Task<IActionResult> Delete(string empId)
        {
            await _service.Delete(empId);
            return NoContent();
        }
        

        [HttpGet("export")]
        public async Task<IActionResult> ExportUsers([FromQuery] string? empId)
        {
            var users = await _service.GetAllAsync(empId);

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");

            // Header
            worksheet.Cell(1, 1).Value = "EMP ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "District";
            worksheet.Cell(1, 4).Value = "Country";
            worksheet.Cell(1, 5).Value = "SLC";
            worksheet.Cell(1, 6).Value = "Email";
            worksheet.Cell(1, 7).Value = "Admin Access";

            int row = 2;

            foreach (var user in users)
            {
                worksheet.Cell(row, 1).Value = user.EmpId;
                worksheet.Cell(row, 2).Value = user.Name;
                worksheet.Cell(row, 3).Value = user.District;
                worksheet.Cell(row, 4).Value = user.Country;
                worksheet.Cell(row, 5).Value = user.Slc;
                worksheet.Cell(row, 6).Value = user.Email;
                worksheet.Cell(row, 7).Value = user.AdminAccess;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Users.xlsx"
            );
        }
    }
}
