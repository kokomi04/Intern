using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.Migrations
{
    /// <inheritdoc />
    public partial class FkEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmployee",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdEmployee",
                table: "Bills",
                column: "IdEmployee");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills",
                column: "IdEmployee",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_IdEmployee",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_IdEmployee",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "IdEmployee",
                table: "Bills");
        }
    }
}
