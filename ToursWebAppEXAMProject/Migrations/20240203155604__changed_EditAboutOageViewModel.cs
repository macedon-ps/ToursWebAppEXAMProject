using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _changed_EditAboutOageViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "EditAboutPage",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "EditAboutPage",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "EditAboutPage");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "EditAboutPage");
        }
    }
}
