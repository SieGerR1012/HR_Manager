namespace HR_Manager.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}
