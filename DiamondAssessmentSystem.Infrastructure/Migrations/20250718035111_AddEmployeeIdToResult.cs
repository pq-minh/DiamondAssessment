using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondAssessmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeIdToResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Results_EmployeeId",
                table: "Results",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Employees_EmployeeId",
                table: "Results",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "employee_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Employees_EmployeeId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EmployeeId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Results");
        }
    }
}
