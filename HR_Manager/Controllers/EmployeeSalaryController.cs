using HR_Manager.Data;
using HR_Manager.DTOs;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeeSalaryController : ControllerBase
{
    private readonly HrDbContext _context;

    public EmployeeSalaryController(HrDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var salaries = await _context.EmployeeSalaries
            .Include(s => s.Employee)
                .ThenInclude(e => e.Person)
            .ToListAsync();

        var result = salaries.Select(s => new EmployeeSalaryDto
        {
            EmployeeSalaryId = s.EmployeeSalaryId,
            EmployeeName = $"{s.Employee!.Person!.LastName} {s.Employee.Person.FirstName}",
            Amount = s.Amount
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var salary = await _context.EmployeeSalaries
            .Include(s => s.Employee)
                .ThenInclude(e => e.Person)
            .Include(s => s.Employee)
                .ThenInclude(e => e.Position)
                    .ThenInclude(p => p.Department)
            .FirstOrDefaultAsync(s => s.EmployeeSalaryId == id);

        if (salary == null)
            return NotFound();

        return Ok(salary);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeSalary salary)
    {
        _context.EmployeeSalaries.Add(salary);
        await _context.SaveChangesAsync();

        return Ok(salary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeSalary updated)
    {
        var salary = await _context.EmployeeSalaries.FindAsync(id);

        if (salary == null)
            return NotFound();

        salary.EmployeeId = updated.EmployeeId;
        salary.Amount = updated.Amount;

        await _context.SaveChangesAsync();

        return Ok(salary);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, EmployeeSalary updated)
    {
        var salary = await _context.EmployeeSalaries.FindAsync(id);

        if (salary == null)
            return NotFound();

        if (updated.EmployeeId != 0)
            salary.EmployeeId = updated.EmployeeId;

        if (updated.Amount != 0)
            salary.Amount = updated.Amount;

        await _context.SaveChangesAsync();

        return Ok(salary);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var salary = await _context.EmployeeSalaries.FindAsync(id);

        if (salary == null)
            return NotFound();

        _context.EmployeeSalaries.Remove(salary);
        await _context.SaveChangesAsync();

        return Ok();
    }
}