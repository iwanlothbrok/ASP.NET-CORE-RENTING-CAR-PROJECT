namespace RentalCars.Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class CarsAndBookingColumnsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_DealerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "IsConfirmed");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DealerId",
                table: "Bookings",
                column: "DealerId",
                unique: true,
                filter: "[DealerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_DealerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DealerId",
                table: "Bookings",
                column: "DealerId",
                unique: true);
        }
    }
}
