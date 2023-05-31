using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class NullValueCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReferralId",
                table: "LaboratoryDiagnosis",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis",
                column: "ReferralId",
                principalTable: "AppointmentDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReferralId",
                table: "LaboratoryDiagnosis",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDiagnosis_AppointmentDetails_ReferralId",
                table: "LaboratoryDiagnosis",
                column: "ReferralId",
                principalTable: "AppointmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
