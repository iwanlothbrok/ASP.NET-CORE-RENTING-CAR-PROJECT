using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class RelationBetweenPaymentAndDebitCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments",
                column: "DebitCardId",
                principalTable: "FakeDebitCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments",
                column: "DebitCardId",
                principalTable: "FakeDebitCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
