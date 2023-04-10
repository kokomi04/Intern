using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountShipContactStatus",
                columns: table => new
                {
                    AccountShipContactStatusId = table.Column<int>(type: "int", nullable: false),
                    AccountShipContactStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountShipContactStatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountShipContactStatus", x => x.AccountShipContactStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    AccountStatusId = table.Column<int>(type: "int", nullable: false),
                    AccountStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountStatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.AccountStatusId);
                });

            migrationBuilder.CreateTable(
                name: "BillStatus",
                columns: table => new
                {
                    BillStatusId = table.Column<int>(type: "int", nullable: false),
                    BillStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BillStatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatus", x => x.BillStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    BrandCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrandDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "BuyMethods",
                columns: table => new
                {
                    BuyMethodId = table.Column<int>(type: "int", nullable: false),
                    BuyMethodCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BuyMethodName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyMethods", x => x.BuyMethodId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CategoryTypeDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.CategoryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ColorDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    ProducerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProducerDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducerId);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatus",
                columns: table => new
                {
                    ProductStatusId = table.Column<int>(type: "int", nullable: false),
                    ProductStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProductStatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatus", x => x.ProductStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SalesStatus",
                columns: table => new
                {
                    SalesStatusId = table.Column<int>(type: "int", nullable: false),
                    SalesStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesStatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStatus", x => x.SalesStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SalesTypes",
                columns: table => new
                {
                    SalesTypeId = table.Column<int>(type: "int", nullable: false),
                    SalesTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesTypeDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTypes", x => x.SalesTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ShipMethods",
                columns: table => new
                {
                    ShipMethodId = table.Column<int>(type: "int", nullable: false),
                    ShipMethodCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShipMethodName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShipPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipMethods", x => x.ShipMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    SizeCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SizeDetail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AccountUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountPassWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountStatusId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountStatus_AccountStatusId",
                        column: x => x.AccountStatusId,
                        principalTable: "AccountStatus",
                        principalColumn: "AccountStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false),
                    SalesStatusId = table.Column<int>(type: "int", nullable: false),
                    SalesTypeId = table.Column<int>(type: "int", nullable: false),
                    SalesCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesPersen = table.Column<int>(type: "int", nullable: true),
                    SaleInt = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_Sales_SalesStatus_SalesStatusId",
                        column: x => x.SalesStatusId,
                        principalTable: "SalesStatus",
                        principalColumn: "SalesStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_SalesTypes_SalesTypeId",
                        column: x => x.SalesTypeId,
                        principalTable: "SalesTypes",
                        principalColumn: "SalesTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ProductStatusId = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ShellPrice = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_CategoryTypes_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "CategoryTypes",
                        principalColumn: "CategoryTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductStatus_ProductStatusId",
                        column: x => x.ProductStatusId,
                        principalTable: "ProductStatus",
                        principalColumn: "ProductStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountShipContacts",
                columns: table => new
                {
                    AccountShipContactId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AccountShipContactStatusId = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountShipContacts", x => x.AccountShipContactId);
                    table.ForeignKey(
                        name: "FK_AccountShipContacts_AccountShipContactStatus_AccountShipContactStatusId",
                        column: x => x.AccountShipContactStatusId,
                        principalTable: "AccountShipContactStatus",
                        principalColumn: "AccountShipContactStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountShipContacts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountBags",
                columns: table => new
                {
                    AccountBagId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBags", x => x.AccountBagId);
                    table.ForeignKey(
                        name: "FK_AccountBags_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImgs",
                columns: table => new
                {
                    ProductImgId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CountImg = table.Column<int>(type: "int", nullable: false),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImgs", x => x.ProductImgId);
                    table.ForeignKey(
                        name: "FK_ProductImgs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteStars",
                columns: table => new
                {
                    VoteStarId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StarVoted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteStars", x => x.VoteStarId);
                    table.ForeignKey(
                        name: "FK_VoteStars_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteStars_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false),
                    AccountShipContactId = table.Column<int>(type: "int", nullable: false),
                    BuyMethodId = table.Column<int>(type: "int", nullable: false),
                    BillStatusId = table.Column<int>(type: "int", nullable: false),
                    ShipMethodId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipToBuyerDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuyerNotification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipPrice = table.Column<int>(type: "int", nullable: false),
                    BillCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_AccountShipContacts_AccountShipContactId",
                        column: x => x.AccountShipContactId,
                        principalTable: "AccountShipContacts",
                        principalColumn: "AccountShipContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_BillStatus_BillStatusId",
                        column: x => x.BillStatusId,
                        principalTable: "BillStatus",
                        principalColumn: "BillStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_BuyMethods_BuyMethodId",
                        column: x => x.BuyMethodId,
                        principalTable: "BuyMethods",
                        principalColumn: "BuyMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_ShipMethods_ShipMethodId",
                        column: x => x.ShipMethodId,
                        principalTable: "ShipMethods",
                        principalColumn: "ShipMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    BillDetailId = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.BillDetailId);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillSales",
                columns: table => new
                {
                    BillSalesId = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    SalesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSales", x => x.BillSalesId);
                    table.ForeignKey(
                        name: "FK_BillSales_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillSales_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "SalesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBags_AccountId",
                table: "AccountBags",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBags_ProductId",
                table: "AccountBags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountStatusId",
                table: "Accounts",
                column: "AccountStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountShipContacts_AccountId",
                table: "AccountShipContacts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountShipContacts_AccountShipContactStatusId",
                table: "AccountShipContacts",
                column: "AccountShipContactStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductId",
                table: "BillDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountShipContactId",
                table: "Bills",
                column: "AccountShipContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills",
                column: "BillStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BuyMethodId",
                table: "Bills",
                column: "BuyMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ShipMethodId",
                table: "Bills",
                column: "ShipMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BillSales_BillId",
                table: "BillSales",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillSales_SalesId",
                table: "BillSales",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImgs_ProductId",
                table: "ProductImgs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryTypeId",
                table: "Products",
                column: "CategoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProducerId",
                table: "Products",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStatusId",
                table: "Products",
                column: "ProductStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesStatusId",
                table: "Sales",
                column: "SalesStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesTypeId",
                table: "Sales",
                column: "SalesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteStars_AccountId",
                table: "VoteStars",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteStars_ProductId",
                table: "VoteStars",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBags");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "BillSales");

            migrationBuilder.DropTable(
                name: "ProductImgs");

            migrationBuilder.DropTable(
                name: "VoteStars");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AccountShipContacts");

            migrationBuilder.DropTable(
                name: "BillStatus");

            migrationBuilder.DropTable(
                name: "BuyMethods");

            migrationBuilder.DropTable(
                name: "ShipMethods");

            migrationBuilder.DropTable(
                name: "SalesStatus");

            migrationBuilder.DropTable(
                name: "SalesTypes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "ProductStatus");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "AccountShipContactStatus");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
