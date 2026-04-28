using HR_Manager.Data;
using HR_Manager.DTOs;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly HrDbContext _context;

    public DepartmentController(HrDbContext context)
    {
        _context = context;
    }

    // GET: api/department
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departments = await _context.Departments.ToListAsync();

        var result = departments.Select(d => new DepartmentDto
        {
            DepartmentId = d.DepartmentId,
            Name = d.Name
        });

        return Ok(result);
    }

    // GET: api/department/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Positions)
            .FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (department == null)
            return NotFound();

        return Ok(department);
    }

    // POST: api/department
    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return Ok(department);
    }

    // PUT: api/department/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Department updated)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        department.Name = updated.Name;

        await _context.SaveChangesAsync();

        return Ok(department);
    }

    // DELETE: api/department/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return Ok();
    }
}