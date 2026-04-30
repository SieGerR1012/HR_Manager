namespace HR_Manager.DTOs
{
    public class CreatePositionDto
    {
        public string Title { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }
    }
}
