namespace HR_Manager.DTOs
{
    public class AddressDto
    {
        public int AddressId { get; set; }

        public string Street { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public string PersonName { get; set; } = string.Empty;
    }
}
