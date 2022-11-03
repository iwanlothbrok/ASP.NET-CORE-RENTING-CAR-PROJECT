namespace RentalCars.Infrastructure.Migrations
{

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CarColumnIsBookedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Cars");
        }
    }
}
