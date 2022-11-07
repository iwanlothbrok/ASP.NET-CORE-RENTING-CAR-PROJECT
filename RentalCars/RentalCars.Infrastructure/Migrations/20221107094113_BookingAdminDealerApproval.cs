using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class BookingAdminDealerApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Bookings",
                newName: "IsConfirmedByDealer");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmedByAdmin",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmedByAdmin",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "IsConfirmedByDealer",
                table: "Bookings",
                newName: "IsConfirmed");
        }
    }
}
