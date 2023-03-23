using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy.EfCore.Migrations
{
    public partial class snake_case_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "dbo",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "category_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "category_id");

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "category_name",
                schema: "dbo",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "dbo",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
