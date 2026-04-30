using HR_Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_Manager.Data;

public class HrDbContext : DbContext
{
    public HrDbContext(DbContextOptions<HrDbContext> options)
        : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Position> Positions => Set<Position>();

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<City> Cities => Set<City>();
    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<SalaryGrade> SalaryGrades => Set<SalaryGrade>();
    public DbSet<EmployeeSalary> EmployeeSalaries => Set<EmployeeSalary>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Position>()
            .HasOne(p => p.SalaryGrade)
            .WithMany()
            .HasForeignKey(p => p.GradeId);
    }
}