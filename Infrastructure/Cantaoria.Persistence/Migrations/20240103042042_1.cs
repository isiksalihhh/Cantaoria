using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cantaoria.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Menus_MenuID",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MenuID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderID",
                table: "Products",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Menus_MenuID",
                table: "Categories",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Menus_MenuID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "MenuID",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersID",
                        column: x => x.OrdersID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsID",
                table: "OrderProduct",
                column: "ProductsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Menus_MenuID",
                table: "Categories",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "ID");
        }
    }
}
