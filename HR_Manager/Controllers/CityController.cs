using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly HrDbContext _context;

        public CityController(HrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _context.Cities
                .Include(c => c.Addresses)
                    .ThenInclude(a => a.Person)
                .ToListAsync();

            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _context.Cities
                .Include(c => c.Addresses)
                    .ThenInclude(a => a.Person)
                .FirstOrDefaultAsync(c => c.CityId == id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return Ok(city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, City updated)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
                return NotFound();

            city.CityName = updated.CityName;

            await _context.SaveChangesAsync();

            return Ok(city);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
                return NotFound();

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
