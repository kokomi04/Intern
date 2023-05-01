using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.Migrations
{
    /// <inheritdoc />
    public partial class FixDistrictID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "AccountShipContacts",
                newName: "ProvinceID");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "AccountShipContacts",
                newName: "DistrictID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                table: "AccountShipContacts",
                newName: "ProvinceId");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                table: "AccountShipContacts",
                newName: "DistrictId");
        }
    }
}
