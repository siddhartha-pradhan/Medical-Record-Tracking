using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class TestDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_Doctors_ApprovedBy",
                table: "MedicationTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_Patients_PatientId",
                table: "MedicationTreatments");

            migrationBuilder.RenameColumn(
                name: "ApprovedBy",
                table: "MedicationTreatments",
                newName: "ReferralId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationTreatments_ApprovedBy",
                table: "MedicationTreatments",
                newName: "IX_MedicationTreatments_ReferralId");

            migrationBuilder.RenameColumn(
                name: "DiagnosisDescription",
                table: "Appointments",
                newName: "AppointmentRequest");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RecordDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Pharmacists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "MedicationTreatments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PharmacistId",
                table: "MedicationTreatments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "MedicationTreatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LabTechnicians",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppointmentDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDetail_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryDiagnosis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDiagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryDiagnosis_AppointmentDetail_ReferralId",
                        column: x => x.ReferralId,
                        principalTable: "AppointmentDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryDiagnosis_DiagnosticTests_TestId",
                        column: x => x.TestId,
                        principalTable: "DiagnosticTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryDiagnosis_LabTechnicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "LabTechnicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationTreatments_PharmacistId",
                table: "MedicationTreatments",
                column: "PharmacistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetail_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDiagnosis_ReferralId",
                table: "LaboratoryDiagnosis",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDiagnosis_TechnicianId",
                table: "LaboratoryDiagnosis",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDiagnosis_TestId",
                table: "LaboratoryDiagnosis",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetail_ReferralId",
                table: "MedicationTreatments",
                column: "ReferralId",
                principalTable: "AppointmentDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_Patients_PatientId",
                table: "MedicationTreatments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_Pharmacists_PharmacistId",
                table: "MedicationTreatments",
                column: "PharmacistId",
                principalTable: "Pharmacists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetail_ReferralId",
                table: "MedicationTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_Patients_PatientId",
                table: "MedicationTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_Pharmacists_PharmacistId",
                table: "MedicationTreatments");

            migrationBuilder.DropTable(
                name: "LaboratoryDiagnosis");

            migrationBuilder.DropTable(
                name: "AppointmentDetail");

            migrationBuilder.DropIndex(
                name: "IX_MedicationTreatments_PharmacistId",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RecordDetails");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "PharmacistId",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "MedicationTreatments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LabTechnicians");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "ReferralId",
                table: "MedicationTreatments",
                newName: "ApprovedBy");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationTreatments_ReferralId",
                table: "MedicationTreatments",
                newName: "IX_MedicationTreatments_ApprovedBy");

            migrationBuilder.RenameColumn(
                name: "AppointmentRequest",
                table: "Appointments",
                newName: "DiagnosisDescription");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "MedicationTreatments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_Doctors_ApprovedBy",
                table: "MedicationTreatments",
                column: "ApprovedBy",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_Patients_PatientId",
                table: "MedicationTreatments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
