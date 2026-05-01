using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class FixTechTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId1",
                table: "TechTaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TechTaskItem_TechTaskPageId1",
                table: "TechTaskItem");

            migrationBuilder.DropColumn(
                name: "TechTaskPageId1",
                table: "TechTaskItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechTaskPageId1",
                table: "TechTaskItem",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 8,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 9,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 10,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 11,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 12,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 13,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 14,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 16,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 17,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 18,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 19,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 20,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 21,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 22,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 23,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 24,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 25,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 26,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 27,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 28,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 29,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "TechTaskItem",
                keyColumn: "Id",
                keyValue: 30,
                column: "TechTaskPageId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_TechTaskItem_TechTaskPageId1",
                table: "TechTaskItem",
                column: "TechTaskPageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId1",
                table: "TechTaskItem",
                column: "TechTaskPageId1",
                principalTable: "TechTaskPages",
                principalColumn: "Id");
        }
    }
}
