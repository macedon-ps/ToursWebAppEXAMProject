using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _AddedPublicIdForImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "New",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Country",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "City",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Blog",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DetailsImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OperationModeImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoGalleryImagePublicId",
                table: "AboutPageVersions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "New");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "AboutImagePublicId",
                table: "AboutPageVersions");

            migrationBuilder.DropColumn(
                name: "DetailsImagePublicId",
                table: "AboutPageVersions");

            migrationBuilder.DropColumn(
                name: "FeedbackImagePublicId",
                table: "AboutPageVersions");

            migrationBuilder.DropColumn(
                name: "MainImagePublicId",
                table: "AboutPageVersions");

            migrationBuilder.DropColumn(
                name: "OperationModeImagePublicId",
                table: "AboutPageVersions");

            migrationBuilder.DropColumn(
                name: "PhotoGalleryImagePublicId",
                table: "AboutPageVersions");
        }
    }
}
