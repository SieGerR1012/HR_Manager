using System.Net;

namespace HR_Manager.Models
{
    public class City
    {
        public int CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
