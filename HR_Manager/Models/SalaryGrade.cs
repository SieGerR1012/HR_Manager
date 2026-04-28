using System.ComponentModel.DataAnnotations;

namespace HR_Manager.Models
{
    public class SalaryGrade
    {
        [Key]
        public int GradeId { get; set; }

        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        public ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}
