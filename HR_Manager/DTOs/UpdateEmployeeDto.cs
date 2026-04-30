namespace HR_Manager.DTOs
{
    public class UpdateEmployeeDto
    {
        public int EmployeeId { get; set; }

        // Person
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; } = "";

        // Address
        public string Street { get; set; } = "";
        public string House { get; set; } = "";
        public string? Apartment { get; set; }
        public int CityId { get; set; }

        // Employee
        public int PositionId { get; set; }

        // Salary
        public decimal Amount { get; set; }
    }
}
