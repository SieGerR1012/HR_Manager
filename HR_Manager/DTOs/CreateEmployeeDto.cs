namespace HR_Manager.DTOs
{
    public class CreateEmployeeDto
    {
        // Person
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; } = string.Empty;

        // Address
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public int CityId { get; set; }

        // Position
        public int PositionId { get; set; }

        // Salary
        public decimal Amount { get; set; }
    }
}
