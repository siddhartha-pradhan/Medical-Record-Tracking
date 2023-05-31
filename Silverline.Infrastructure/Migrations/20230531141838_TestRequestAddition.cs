using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class TestRequestAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionStatus",
                table: "TestCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalizedDate",
                table: "TestCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "TestCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TechnicianRemarks",
                table: "TestCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "TestCarts",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionStatus",
                table: "TestCarts");

            migrationBuilder.DropColumn(
                name: "FinalizedDate",
                table: "TestCarts");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "TestCarts");

            migrationBuilder.DropColumn(
                name: "TechnicianRemarks",
                table: "TestCarts");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "TestCarts");
        }
    }
}
