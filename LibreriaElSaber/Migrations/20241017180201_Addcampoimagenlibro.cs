using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaElSaber.Migrations
{
    /// <inheritdoc />
    public partial class Addcampoimagenlibro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Libros");
        }
    }
}
