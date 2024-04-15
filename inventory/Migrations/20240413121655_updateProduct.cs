using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_ProductGroupId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "ProductGroupId",
                table: "productGroups",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupCode",
                table: "productGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductGroupId",
                table: "products",
                column: "ProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_ProductGroupId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductGroupCode",
                table: "productGroups");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "productGroups",
                newName: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductGroupId",
                table: "products",
                column: "ProductGroupId",
                unique: true);
        }
    }
}
