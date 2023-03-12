using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class ColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TestHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TestHeaders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "BookedDate",
                table: "Appointments");
        }
    }
}
