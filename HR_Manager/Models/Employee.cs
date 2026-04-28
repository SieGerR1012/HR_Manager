namespace HR_Manager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public ICollection<EmployeeSalary> Salaries { get; set; } = new List<EmployeeSalary>();
    }

}
