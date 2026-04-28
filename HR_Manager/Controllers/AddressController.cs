using HR_Manager.Data;
using HR_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly HrDbContext _context;

        public AddressController(HrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var addresses = await _context.Addresses
                .Include(a => a.Person)
                .Include(a => a.City)
                .ToListAsync();

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Person)
                .Include(a => a.City)
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Address updated)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            address.PersonId = updated.PersonId;
            address.CityId = updated.CityId;
            address.Street = updated.Street;
            address.House = updated.House;
            address.Apartment = updated.Apartment;

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Address updated)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            if (updated.PersonId != 0)
                address.PersonId = updated.PersonId;

            if (updated.CityId != 0)
                address.CityId = updated.CityId;

            if (!string.IsNullOrWhiteSpace(updated.Street))
                address.Street = updated.Street;

            if (!string.IsNullOrWhiteSpace(updated.House))
                address.House = updated.House;

            if (updated.Apartment != null)
                address.Apartment = updated.Apartment;

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
