using BEProject.Data;
using BEProject.Model.DTO;
using BEProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BEDbContext dbContext;

        public EmployeeController(BEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //get all employee
        [HttpGet]
        public async Task<IActionResult> getAllEmp()
        {
            var emps = await dbContext.Employees
                .Include(p => p.Position)
                .ThenInclude(t => t.Title)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.EmployeeName,
                    Nik = e.EmployeeNik,
                    Address = e.EmployeeAddress,
                    PositionId = e.Position.PositionId,
                    PositionCode = e.Position.PositionCode,
                    PositionName = e.Position.PositionName,
                    TitleCode = e.Position.Title.TitleCode,
                    TitleName = e.Position.Title.TitleName
                }).ToListAsync();

            return Ok(emps);
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var emp = await dbContext.Employees
                .Include(p => p.Position)
                .ThenInclude(t => t.Title)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.EmployeeName,
                    Nik = e.EmployeeNik,
                    Address = e.EmployeeAddress,
                    PositionId = e.Position.PositionId,
                    PositionCode = e.Position.PositionCode,
                    PositionName = e.Position.PositionName,
                    TitleCode = e.Position.Title.TitleCode,
                    TitleName = e.Position.Title.TitleName
                }).FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null)
            {
                return NotFound("Id tidak ditemukan");
            }
            return Ok(emp);

        }

        // post employee
        [HttpPost]
        public async Task<IActionResult> EmployeePost([FromBody] EmpReqDto empReqDto)
        {
            Employee newEmp = new Employee
            {
                EmployeeName = empReqDto.Name,
                EmployeeNik = empReqDto.Nik,
                EmployeeAddress = empReqDto.Address,
                PositionId = empReqDto.PositionId,
            };

            await dbContext.AddAsync(newEmp);
            await dbContext.SaveChangesAsync();

            return Ok(newEmp);
        }

        //edit employee
        [HttpPut("{id}")]
        public async Task<IActionResult> EmployeePut([FromRoute] int id, EmpReqDto empReqDto)
        {
            var empPut = await dbContext.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (empPut == null)
            {
                return NotFound();
            }

            empPut.EmployeeName = empReqDto.Name;
            empPut.EmployeeNik = empReqDto.Nik;
            empPut.EmployeeAddress = empReqDto.Address;
            empPut.PositionId = empReqDto.PositionId;

            await dbContext.SaveChangesAsync();
            return Ok(empPut);
        }

        //delete employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> EmployeeDelet([FromRoute] int id)
        {
            var emp = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(emp);
            dbContext.SaveChangesAsync();
            return Ok(emp);
        }
    }
}
