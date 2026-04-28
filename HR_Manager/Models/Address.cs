namespace HR_Manager.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
