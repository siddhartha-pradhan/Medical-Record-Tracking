using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class AppointmentDbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "MedicationTreatments",
                newName: "DoctorRemarks");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "LaboratoryDiagnosis",
                newName: "DoctorRemarks");

            migrationBuilder.AddColumn<string>(
                name: "ActionStatus",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PharmacistRemarks",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "LaboratoryDiagnosis",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "ActionStatus",
                table: "LaboratoryDiagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LaboratoryDiagnosis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicianRemarks",
                table: "LaboratoryDiagnosis",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionStatus",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "PharmacistRemarks",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "ActionStatus",
                table: "LaboratoryDiagnosis");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LaboratoryDiagnosis");

            migrationBuilder.DropColumn(
                name: "TechnicianRemarks",
                table: "LaboratoryDiagnosis");

            migrationBuilder.RenameColumn(
                name: "DoctorRemarks",
                table: "MedicationTreatments",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "DoctorRemarks",
                table: "LaboratoryDiagnosis",
                newName: "Remarks");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "LaboratoryDiagnosis",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
