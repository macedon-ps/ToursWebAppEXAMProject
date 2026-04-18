using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _Delete_EditAboutPageViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditAboutPage");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "PhotoGalleryImages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "PhotoGalleryImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "EditAboutPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AboutShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AboutTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DetailsFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailsShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DetailsTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FeedbackFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeedbackShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FeedbackTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActual = table.Column<bool>(type: "bit", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MainFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MainShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MainTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OperationModeFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationModeImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OperationModeShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OperationModeTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhotoGalleryFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoGalleryImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhotoGalleryShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhotoGalleryTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditAboutPage", x => x.Id);
                });
        }
    }
}
