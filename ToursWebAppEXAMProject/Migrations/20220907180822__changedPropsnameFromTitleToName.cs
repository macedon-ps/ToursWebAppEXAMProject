using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    public partial class _changedPropsnameFromTitleToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "New",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Blog",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "New",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Blog",
                newName: "Title");
        }
    }
}
