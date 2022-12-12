using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class PaymentUserIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTime",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");
        }
    }
}
