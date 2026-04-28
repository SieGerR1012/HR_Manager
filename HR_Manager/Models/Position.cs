namespace HR_Manager.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int GradeId { get; set; }
        public SalaryGrade? SalaryGrade { get; set; }
    }
}
