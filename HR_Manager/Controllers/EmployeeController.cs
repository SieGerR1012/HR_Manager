using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly HrDbContext _context;

        public EmployeeController(HrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _context.Employees
                .Include(e => e.Person)
                .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                .Include(e => e.Position)
                    .ThenInclude(p => p.SalaryGrade)
                .Include(e => e.Salaries)
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Person)
                .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                .Include(e => e.Position)
                    .ThenInclude(p => p.SalaryGrade)
                .Include(e => e.Salaries)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee updated)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            employee.PersonId = updated.PersonId;
            employee.PositionId = updated.PositionId;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Employee updated)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            if (updated.PersonId != 0)
                employee.PersonId = updated.PersonId;

            if (updated.PositionId != 0)
                employee.PositionId = updated.PositionId;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
