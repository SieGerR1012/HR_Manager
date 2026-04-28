using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly HrDbContext _context;

    public PositionController(HrDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var positions = await _context.Positions
            .Include(p => p.Department)
            .Include(p => p.SalaryGrade)
            .ToListAsync();

        return Ok(positions);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        return Ok(position);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var position = await _context.Positions
            .Include(p => p.Department)
            .Include(p => p.SalaryGrade)
            .FirstOrDefaultAsync(p => p.PositionId == id);

        if (position == null)
            return NotFound();

        return Ok(position);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Position updated)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position == null)
            return NotFound();

        position.Title = updated.Title;
        position.DepartmentId = updated.DepartmentId;
        position.GradeId = updated.GradeId;

        await _context.SaveChangesAsync();

        return Ok(position);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, Position updated)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position == null)
            return NotFound();

        if (!string.IsNullOrWhiteSpace(updated.Title))
            position.Title = updated.Title;

        if (updated.DepartmentId != 0)
            position.DepartmentId = updated.DepartmentId;

        if (updated.GradeId != 0)
            position.GradeId = updated.GradeId;

        await _context.SaveChangesAsync();

        return Ok(position);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position == null)
            return NotFound();

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();

        return Ok();
    }
}