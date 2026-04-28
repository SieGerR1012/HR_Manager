using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SalaryGradeController : ControllerBase
{
    private readonly HrDbContext _context;

    public SalaryGradeController(HrDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var grades = await _context.SalaryGrades
            .Include(g => g.Positions)
                .ThenInclude(p => p.Department)
            .ToListAsync();

        return Ok(grades);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var grade = await _context.SalaryGrades
            .Include(g => g.Positions)
                .ThenInclude(p => p.Department)
            .FirstOrDefaultAsync(g => g.GradeId == id);

        if (grade == null)
            return NotFound();

        return Ok(grade);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SalaryGrade grade)
    {
        _context.SalaryGrades.Add(grade);
        await _context.SaveChangesAsync();

        return Ok(grade);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SalaryGrade updated)
    {
        var grade = await _context.SalaryGrades.FindAsync(id);

        if (grade == null)
            return NotFound();

        grade.MinSalary = updated.MinSalary;
        grade.MaxSalary = updated.MaxSalary;

        await _context.SaveChangesAsync();

        return Ok(grade);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, SalaryGrade updated)
    {
        var grade = await _context.SalaryGrades.FindAsync(id);

        if (grade == null)
            return NotFound();

        if (updated.MinSalary != 0)
            grade.MinSalary = updated.MinSalary;

        if (updated.MaxSalary != 0)
            grade.MaxSalary = updated.MaxSalary;

        await _context.SaveChangesAsync();

        return Ok(grade);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var grade = await _context.SalaryGrades.FindAsync(id);

        if (grade == null)
            return NotFound();

        _context.SalaryGrades.Remove(grade);
        await _context.SaveChangesAsync();

        return Ok();
    }
}