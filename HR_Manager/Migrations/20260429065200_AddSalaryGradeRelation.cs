using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Manager.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryGradeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Positions_GradeId",
                table: "Positions",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_SalaryGrades_GradeId",
                table: "Positions",
                column: "GradeId",
                principalTable: "SalaryGrades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_SalaryGrades_GradeId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_GradeId",
                table: "Positions");
        }
    }
}
