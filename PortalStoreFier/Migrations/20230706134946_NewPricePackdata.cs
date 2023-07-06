using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalStoreFier.Migrations
{
    public partial class NewPricePackdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "NewPricePacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartingDate",
                table: "NewPricePacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "NewPricePacks");

            migrationBuilder.DropColumn(
                name: "ContractStartingDate",
                table: "NewPricePacks");
        }
    }
}
