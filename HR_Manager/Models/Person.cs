namespace HR_Manager.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string ContactNumber { get; set; } = string.Empty;

        public Employee? Employee { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
