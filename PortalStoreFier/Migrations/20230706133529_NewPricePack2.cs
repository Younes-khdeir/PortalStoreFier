using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalStoreFier.Migrations
{
    public partial class NewPricePack2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_NewPricePackId",
                table: "Customers",
                column: "NewPricePackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_NewPricePacks_NewPricePackId",
                table: "Customers",
                column: "NewPricePackId",
                principalTable: "NewPricePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_NewPricePacks_NewPricePackId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_NewPricePackId",
                table: "Customers");
        }
    }
}
