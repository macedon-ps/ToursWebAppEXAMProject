using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _changedCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalDescrition",
                table: "City",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalDescrition",
                table: "City");
        }
    }
}
