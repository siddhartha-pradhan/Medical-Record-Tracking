using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class DbNullValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_LabTechnicians_TechnicianId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.AlterColumn<string>(
                name: "ActionStatus",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicianId",
                table: "LaboratoryDiagnosis",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ActionStatus",
                table: "LaboratoryDiagnosis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_LabTechnicians_TechnicianId",
                table: "LaboratoryDiagnosis",
                column: "TechnicianId",
                principalTable: "LabTechnicians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_LabTechnicians_TechnicianId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.AlterColumn<string>(
                name: "ActionStatus",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicianId",
                table: "LaboratoryDiagnosis",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionStatus",
                table: "LaboratoryDiagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_LabTechnicians_TechnicianId",
                table: "LaboratoryDiagnosis",
                column: "TechnicianId",
                principalTable: "LabTechnicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
