using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class BookingIsExpiredAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Bookings");
        }
    }
}
