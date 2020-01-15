using Microsoft.EntityFrameworkCore.Migrations;

namespace CheeseMVC.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Cheeses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatregoryID",
                table: "Cheeses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cheeses_CategoryID",
                table: "Cheeses",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeses_Categories_CategoryID",
                table: "Cheeses",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeses_Categories_CategoryID",
                table: "Cheeses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Cheeses_CategoryID",
                table: "Cheeses");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Cheeses");

            migrationBuilder.DropColumn(
                name: "CatregoryID",
                table: "Cheeses");
        }
    }
}
