using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalStoreFier.Migrations
{
    public partial class addClassification22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ClassificationId",
                table: "Customers",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Classifications_ClassificationId",
                table: "Customers",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Classifications_ClassificationId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ClassificationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "Customers");
        }
    }
}
