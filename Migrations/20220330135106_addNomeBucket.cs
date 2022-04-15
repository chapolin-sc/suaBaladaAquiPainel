using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace suaBaladaAqui.Migrations
{
    public partial class addNomeBucket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data",
                table: "fotos",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "NomeBucketnaAws",
                table: "book",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeBucketnaAws",
                table: "book");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "fotos",
                newName: "data");
        }
    }
}
