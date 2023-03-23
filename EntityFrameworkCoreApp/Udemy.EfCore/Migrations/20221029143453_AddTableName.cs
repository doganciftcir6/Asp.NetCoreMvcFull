using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy.EfCore.Migrations
{
    public partial class AddTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.EnsureSchema(
                name: "c");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category",
                newSchema: "c");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                schema: "c",
                table: "Category",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                schema: "c",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "c",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }
    }
}
