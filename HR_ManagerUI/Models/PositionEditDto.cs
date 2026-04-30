namespace HR_ManagerUI.Models
{
    public class PositionEditDto
    {
        public int PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        // добавляем зарплату напрямую (для создания SalaryGrade)
        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }
    }
}
