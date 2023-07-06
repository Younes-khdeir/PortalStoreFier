using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalStoreFier.Migrations
{
    public partial class NewPricePackdata33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_NewPricePacks_NewPricePackId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_PricePacks_NewPricePacks_NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropIndex(
                name: "IX_PricePacks_NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropIndex(
                name: "IX_Customers_NewPricePackId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropColumn(
                name: "NewPricePackId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_NewPricePacks_CustomerId",
                table: "NewPricePacks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NewPricePacks_PricePackId",
                table: "NewPricePacks",
                column: "PricePackId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewPricePacks_Customers_CustomerId",
                table: "NewPricePacks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewPricePacks_PricePacks_PricePackId",
                table: "NewPricePacks",
                column: "PricePackId",
                principalTable: "PricePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewPricePacks_Customers_CustomerId",
                table: "NewPricePacks");

            migrationBuilder.DropForeignKey(
                name: "FK_NewPricePacks_PricePacks_PricePackId",
                table: "NewPricePacks");

            migrationBuilder.DropIndex(
                name: "IX_NewPricePacks_CustomerId",
                table: "NewPricePacks");

            migrationBuilder.DropIndex(
                name: "IX_NewPricePacks_PricePackId",
                table: "NewPricePacks");

            migrationBuilder.AddColumn<int>(
                name: "NewPricePackId",
                table: "PricePacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewPricePackId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PricePacks_NewPricePackId",
                table: "PricePacks",
                column: "NewPricePackId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PricePacks_NewPricePacks_NewPricePackId",
                table: "PricePacks",
                column: "NewPricePackId",
                principalTable: "NewPricePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
