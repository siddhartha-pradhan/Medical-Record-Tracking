using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class UpdatedDatabases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TimePeriod",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimePeriod",
                table: "MedicationTreatments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
