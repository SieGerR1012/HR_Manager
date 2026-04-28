using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly HrDbContext _context;

        public PersonController(HrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _context.Persons
                .Include(p => p.Addresses)
                    .ThenInclude(a => a.City)
                .Include(p => p.Employee)
                    .ThenInclude(e => e.Position)
                        .ThenInclude(pos => pos.Department)
                .ToListAsync();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Addresses)
                    .ThenInclude(a => a.City)
                .Include(p => p.Employee)
                    .ThenInclude(e => e.Position)
                        .ThenInclude(pos => pos.Department)
                .FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Person updated)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            person.FirstName = updated.FirstName;
            person.LastName = updated.LastName;
            person.MiddleName = updated.MiddleName;
            person.BirthDate = updated.BirthDate;
            person.ContactNumber = updated.ContactNumber;

            await _context.SaveChangesAsync();

            return Ok(person);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Person updated)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(updated.FirstName))
                person.FirstName = updated.FirstName;

            if (!string.IsNullOrWhiteSpace(updated.LastName))
                person.LastName = updated.LastName;

            if (updated.MiddleName != null)
                person.MiddleName = updated.MiddleName;

            if (updated.BirthDate != default)
                person.BirthDate = updated.BirthDate;

            if (!string.IsNullOrWhiteSpace(updated.ContactNumber))
                person.ContactNumber = updated.ContactNumber;

            await _context.SaveChangesAsync();

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
