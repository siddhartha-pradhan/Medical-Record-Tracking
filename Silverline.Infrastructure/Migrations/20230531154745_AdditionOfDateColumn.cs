using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverline.Infrastructure.Migrations
{
    public partial class AdditionOfDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookedDate",
                table: "TestCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedDate",
                table: "TestCarts");
        }
    }
}
