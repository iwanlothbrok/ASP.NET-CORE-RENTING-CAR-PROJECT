using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class CarsTableImageUrlChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cars",
                newName: "CarPhoto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarPhoto",
                table: "Cars",
                newName: "ImageUrl");
        }
    }
}
