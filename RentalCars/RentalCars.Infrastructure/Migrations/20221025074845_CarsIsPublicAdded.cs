﻿namespace RentalCars.Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class CarsIsPublicAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Cars");
        }
    }
}
