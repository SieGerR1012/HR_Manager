namespace HR_ManagerUI.Models
{
    public class CreateEmployeeDto
    {
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; } = "";

        public string Street { get; set; } = "";
        public string House { get; set; } = "";
        public string? Apartment { get; set; }

        public int CityId { get; set; }

        public int PositionId { get; set; }

        public decimal Amount { get; set; }
    }
}
