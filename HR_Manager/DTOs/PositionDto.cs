namespace HR_Manager.DTOs
{
    public class PositionDto
    {
        public int PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
}
