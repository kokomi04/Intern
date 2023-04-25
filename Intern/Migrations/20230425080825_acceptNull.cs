using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.Migrations
{
    /// <inheritdoc />
    public partial class acceptNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AccountShipContacts_AccountShipContactId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AccountShipContactId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "ShipPrice",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmployee",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNotification",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountShipContactId",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountShipContactId",
                table: "Bills",
                column: "AccountShipContactId",
                unique: true,
                filter: "[AccountShipContactId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AccountShipContacts_AccountShipContactId",
                table: "Bills",
                column: "AccountShipContactId",
                principalTable: "AccountShipContacts",
                principalColumn: "AccountShipContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills",
                column: "IdEmployee",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AccountShipContacts_AccountShipContactId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AccountShipContactId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "ShipPrice",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdEmployee",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNotification",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountShipContactId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountShipContactId",
                table: "Bills",
                column: "AccountShipContactId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AccountShipContacts_AccountShipContactId",
                table: "Bills",
                column: "AccountShipContactId",
                principalTable: "AccountShipContacts",
                principalColumn: "AccountShipContactId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills",
                column: "IdEmployee",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
