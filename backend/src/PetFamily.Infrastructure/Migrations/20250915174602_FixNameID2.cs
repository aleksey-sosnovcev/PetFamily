using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNameID2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "breed",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "species",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "pets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "breed",
                newName: "Id");
        }
    }
}
