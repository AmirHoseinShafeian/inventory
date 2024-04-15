using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory.Migrations
{
    public partial class updatemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentGroup",
                table: "productGroups");

            migrationBuilder.AddColumn<int>(
                name: "ParentGroupId",
                table: "productGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productGroups_ParentGroupId",
                table: "productGroups",
                column: "ParentGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_productGroups_productGroups_ParentGroupId",
                table: "productGroups",
                column: "ParentGroupId",
                principalTable: "productGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productGroups_productGroups_ParentGroupId",
                table: "productGroups");

            migrationBuilder.DropIndex(
                name: "IX_productGroups_ParentGroupId",
                table: "productGroups");

            migrationBuilder.DropColumn(
                name: "ParentGroupId",
                table: "productGroups");

            migrationBuilder.AddColumn<int>(
                name: "ParentGroup",
                table: "productGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
