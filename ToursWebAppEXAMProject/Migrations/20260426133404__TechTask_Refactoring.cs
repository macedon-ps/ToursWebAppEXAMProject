using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _TechTask_Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechTaskPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechTaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechTaskPageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechTaskItems_TechTaskPages_TechTaskPageId",
                        column: x => x.TechTaskPageId,
                        principalTable: "TechTaskPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechTaskItems_TechTaskPageId",
                table: "TechTaskItems",
                column: "TechTaskPageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechTaskItems");

            migrationBuilder.DropTable(
                name: "TechTaskPages");
        }
    }
}
