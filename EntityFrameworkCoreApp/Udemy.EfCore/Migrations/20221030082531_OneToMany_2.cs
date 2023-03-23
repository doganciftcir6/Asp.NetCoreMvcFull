using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy.EfCore.Migrations
{
    public partial class OneToMany_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleHistory_Products_ProductId",
                table: "SaleHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleHistory",
                table: "SaleHistory");

            migrationBuilder.RenameTable(
                name: "SaleHistory",
                newName: "saleHistories");

            migrationBuilder.RenameIndex(
                name: "IX_SaleHistory_ProductId",
                table: "saleHistories",
                newName: "IX_saleHistories_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_saleHistories",
                table: "saleHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_saleHistories_Products_ProductId",
                table: "saleHistories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_saleHistories_Products_ProductId",
                table: "saleHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_saleHistories",
                table: "saleHistories");

            migrationBuilder.RenameTable(
                name: "saleHistories",
                newName: "SaleHistory");

            migrationBuilder.RenameIndex(
                name: "IX_saleHistories_ProductId",
                table: "SaleHistory",
                newName: "IX_SaleHistory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleHistory",
                table: "SaleHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleHistory_Products_ProductId",
                table: "SaleHistory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
