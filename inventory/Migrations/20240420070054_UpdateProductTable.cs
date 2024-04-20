using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XpDate",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "ProductGroupCode",
                table: "productGroups",
                newName: "Code");

            migrationBuilder.AddColumn<string>(
                name: "Exp",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exp",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "productGroups",
                newName: "ProductGroupCode");

            migrationBuilder.AddColumn<int>(
                name: "XpDate",
                table: "products",
                type: "int",
                nullable: true);
        }
    }
}
