using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Manager.Migrations
{
    /// <inheritdoc />
    public partial class AddSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Positions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaryGradeGradeId",
                table: "Positions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                columns: table => new
                {
                    EmployeeSalaryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.EmployeeSalaryId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryGrades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSalary = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryGrades", x => x.GradeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SalaryGradeGradeId",
                table: "Positions",
                column: "SalaryGradeGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_SalaryGrades_SalaryGradeGradeId",
                table: "Positions",
                column: "SalaryGradeGradeId",
                principalTable: "SalaryGrades",
                principalColumn: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_SalaryGrades_SalaryGradeGradeId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "SalaryGrades");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SalaryGradeGradeId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "SalaryGradeGradeId",
                table: "Positions");
        }
    }
}
