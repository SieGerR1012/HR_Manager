namespace HR_Manager.Models
{
    public class EmployeeSalary
    {
        public int EmployeeSalaryId { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public decimal Amount { get; set; }
    }
}
