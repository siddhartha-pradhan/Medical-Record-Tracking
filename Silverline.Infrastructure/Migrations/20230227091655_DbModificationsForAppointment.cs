using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class DbModificationsForAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetail_Appointments_AppointmentId",
                table: "AppointmentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetail_ReferralId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetail_ReferralId",
                table: "MedicationTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail");

            migrationBuilder.RenameTable(
                name: "AppointmentDetail",
                newName: "AppointmentDetails");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentDetail_AppointmentId",
                table: "AppointmentDetails",
                newName: "IX_AppointmentDetails_AppointmentId");

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "LaboratoryDiagnosis",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentDetails",
                table: "AppointmentDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetails_Appointments_AppointmentId",
                table: "AppointmentDetails",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis",
                column: "ReferralId",
                principalTable: "AppointmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetails_ReferralId",
                table: "MedicationTreatments",
                column: "ReferralId",
                principalTable: "AppointmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetails_Appointments_AppointmentId",
                table: "AppointmentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetails_ReferralId",
                table: "MedicationTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentDetails",
                table: "AppointmentDetails");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "LaboratoryDiagnosis");

            migrationBuilder.RenameTable(
                name: "AppointmentDetails",
                newName: "AppointmentDetail");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentDetails_AppointmentId",
                table: "AppointmentDetail",
                newName: "IX_AppointmentDetail_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetail_Appointments_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetail_ReferralId",
                table: "LaboratoryDiagnosis",
                column: "ReferralId",
                principalTable: "AppointmentDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTreatments_AppointmentDetail_ReferralId",
                table: "MedicationTreatments",
                column: "ReferralId",
                principalTable: "AppointmentDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
