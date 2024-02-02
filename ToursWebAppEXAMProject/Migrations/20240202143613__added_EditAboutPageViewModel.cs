using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _added_EditAboutPageViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditAboutPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AboutTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DetailsTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OperationModeTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhotoGalleryTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FeedbackTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MainShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AboutShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DetailsShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OperationModeShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhotoGalleryShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FeedbackShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MainFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationModeFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoGalleryFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AboutImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailsImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OperationModeImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhotoGalleryImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeedbackImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditAboutPage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditAboutPage");
        }
    }
}
