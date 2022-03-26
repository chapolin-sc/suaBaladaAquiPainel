using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace suaBaladaAqui.Migrations
{
    public partial class bookFotos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "fotos",
                newName: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_fotos_BookId",
                table: "fotos",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_fotos_book_BookId",
                table: "fotos",
                column: "BookId",
                principalTable: "book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fotos_book_BookId",
                table: "fotos");

            migrationBuilder.DropIndex(
                name: "IX_fotos_BookId",
                table: "fotos");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "fotos",
                newName: "IdBook");
        }
    }
}
