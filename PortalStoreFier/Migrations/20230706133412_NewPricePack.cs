using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalStoreFier.Migrations
{
    public partial class NewPricePack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "NewPricePacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PricePackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewPricePacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricePacks_NewPricePackId",
                table: "PricePacks",
                column: "NewPricePackId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricePacks_NewPricePacks_NewPricePackId",
                table: "PricePacks",
                column: "NewPricePackId",
                principalTable: "NewPricePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricePacks_NewPricePacks_NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropTable(
                name: "NewPricePacks");

            migrationBuilder.DropIndex(
                name: "IX_PricePacks_NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropColumn(
                name: "NewPricePackId",
                table: "PricePacks");

            migrationBuilder.DropColumn(
                name: "NewPricePackId",
                table: "Customers");
        }
    }
}
