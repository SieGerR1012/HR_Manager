using HR_Manager.Data;
using HR_Manager.DTOs;
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
                .Include(e => e.Salaries)
                .ToListAsync();

            var result = employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FullName = $"{e.Person!.LastName} {e.Person.FirstName} {e.Person.MiddleName}",
                PositionTitle = e.Position!.Title,
                DepartmentName = e.Position.Department!.Name,
                Salary = e.Salaries.FirstOrDefault()?.Amount
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Person)
                .ThenInclude(p => p.Addresses)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            var address = employee.Person.Addresses.FirstOrDefault();

            var salary = await _context.EmployeeSalaries
                .FirstOrDefaultAsync(s => s.EmployeeId == id);

            return Ok(new UpdateEmployeeDto
            {
                EmployeeId = employee.EmployeeId,

                LastName = employee.Person.LastName,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                BirthDate = employee.Person.BirthDate,
                ContactNumber = employee.Person.ContactNumber,

                Street = address?.Street ?? "",
                House = address?.House ?? "",
                Apartment = address?.Apartment,
                CityId = address?.CityId ?? 0,

                PositionId = employee.PositionId,

                Amount = salary?.Amount ?? 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            // 1. Person
            var person = new Person
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                BirthDate = dto.BirthDate,
                ContactNumber = dto.ContactNumber
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            // 2. Address
            var address = new Address
            {
                PersonId = person.PersonId,   
                Street = dto.Street,
                House = dto.House,
                Apartment = dto.Apartment,
                CityId = dto.CityId           
            };

            _context.Addresses.Add(address);

            // 3. Employee
            var employee = new Employee
            {
                PersonId = person.PersonId,  
                PositionId = dto.PositionId   
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // 4. Salary
            var salary = new EmployeeSalary
            {
                EmployeeId = employee.EmployeeId,
                Amount = dto.Amount
            };

            _context.EmployeeSalaries.Add(salary);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees
                .Include(e => e.Person)
                .ThenInclude(p => p.Addresses)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            var address = employee.Person.Addresses.FirstOrDefault();

            // Person
            employee.Person.LastName = dto.LastName;
            employee.Person.FirstName = dto.FirstName;
            employee.Person.MiddleName = dto.MiddleName;
            employee.Person.BirthDate = dto.BirthDate;
            employee.Person.ContactNumber = dto.ContactNumber;

            // Address
            if (address != null)
            {
                address.Street = dto.Street;
                address.House = dto.House;
                address.Apartment = dto.Apartment;
                address.CityId = dto.CityId;
            }

            // Employee
            employee.PositionId = dto.PositionId;

            // Salary
            var salary = await _context.EmployeeSalaries
                .FirstOrDefaultAsync(s => s.EmployeeId == id);

            if (salary != null)
                salary.Amount = dto.Amount;

            await _context.SaveChangesAsync();

            return Ok();
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
