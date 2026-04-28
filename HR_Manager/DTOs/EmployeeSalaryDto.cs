namespace HR_Manager.DTOs
{
    public class EmployeeSalaryDto
    {
        public int EmployeeSalaryId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public decimal Amount { get; set; }
    }
}
