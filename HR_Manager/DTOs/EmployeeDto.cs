namespace HR_Manager.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string PositionTitle { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public decimal? Salary { get; set; }
    }
}
