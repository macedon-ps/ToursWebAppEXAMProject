using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _Changed_TechTaskItem_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechTaskItems_TechTaskPages_TechTaskPageId",
                table: "TechTaskItems");

            migrationBuilder.DropTable(
                name: "TechTaskViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechTaskItems",
                table: "TechTaskItems");

            migrationBuilder.RenameTable(
                name: "TechTaskItems",
                newName: "TechTaskItem");

            migrationBuilder.RenameIndex(
                name: "IX_TechTaskItems_TechTaskPageId",
                table: "TechTaskItem",
                newName: "IX_TechTaskItem_TechTaskPageId");

            migrationBuilder.AddColumn<int>(
                name: "TechTaskPageId1",
                table: "TechTaskItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechTaskItem",
                table: "TechTaskItem",
                column: "Id");

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
                name: "IX_TechTaskItem_OrderNumber",
                table: "TechTaskItem",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TechTaskItem_TechTaskPageId1",
                table: "TechTaskItem",
                column: "TechTaskPageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId",
                table: "TechTaskItem",
                column: "TechTaskPageId",
                principalTable: "TechTaskPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId1",
                table: "TechTaskItem",
                column: "TechTaskPageId1",
                principalTable: "TechTaskPages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId",
                table: "TechTaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId1",
                table: "TechTaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechTaskItem",
                table: "TechTaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TechTaskItem_OrderNumber",
                table: "TechTaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TechTaskItem_TechTaskPageId1",
                table: "TechTaskItem");

            migrationBuilder.DropColumn(
                name: "TechTaskPageId1",
                table: "TechTaskItem");

            migrationBuilder.RenameTable(
                name: "TechTaskItem",
                newName: "TechTaskItems");

            migrationBuilder.RenameIndex(
                name: "IX_TechTaskItem_TechTaskPageId",
                table: "TechTaskItems",
                newName: "IX_TechTaskItems_TechTaskPageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechTaskItems",
                table: "TechTaskItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TechTaskViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecuteTechTasksProgress = table.Column<double>(type: "float", nullable: true),
                    IsExecuteTechTask1 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask2 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask3 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask4 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask5 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask6 = table.Column<bool>(type: "bit", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('страница')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskViewModel", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TechTaskItems_TechTaskPages_TechTaskPageId",
                table: "TechTaskItems",
                column: "TechTaskPageId",
                principalTable: "TechTaskPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
