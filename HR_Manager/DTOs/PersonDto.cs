namespace HR_Manager.DTOs
{
    public class PersonDto
    {
        public int PersonId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string ContactNumber { get; set; } = string.Empty;
    }
}
