using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infrastructure.Migrations
{
    public partial class FakeDebitCardsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_DebitCards_DebitCardId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitCards",
                table: "DebitCards");

            migrationBuilder.RenameTable(
                name: "DebitCards",
                newName: "FakeDebitCards");

            migrationBuilder.AlterColumn<long>(
                name: "CreditCardNumber",
                table: "FakeDebitCards",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 16);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FakeDebitCards",
                table: "FakeDebitCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments",
                column: "DebitCardId",
                principalTable: "FakeDebitCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_FakeDebitCards_DebitCardId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FakeDebitCards",
                table: "FakeDebitCards");

            migrationBuilder.RenameTable(
                name: "FakeDebitCards",
                newName: "DebitCards");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardNumber",
                table: "DebitCards",
                type: "int",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitCards",
                table: "DebitCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_DebitCards_DebitCardId",
                table: "Payments",
                column: "DebitCardId",
                principalTable: "DebitCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
