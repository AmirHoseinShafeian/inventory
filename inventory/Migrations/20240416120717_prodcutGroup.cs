using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory.Migrations
{
    public partial class prodcutGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_productRemittances_ProductId",
                table: "productRemittances",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productRegistrations_ProductId",
                table: "productRegistrations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productRegistrations_products_ProductId",
                table: "productRegistrations",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productRemittances_products_ProductId",
                table: "productRemittances",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productRegistrations_products_ProductId",
                table: "productRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_productRemittances_products_ProductId",
                table: "productRemittances");

            migrationBuilder.DropIndex(
                name: "IX_productRemittances_ProductId",
                table: "productRemittances");

            migrationBuilder.DropIndex(
                name: "IX_productRegistrations_ProductId",
                table: "productRegistrations");
        }
    }
}
