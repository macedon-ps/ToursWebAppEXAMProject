using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _titleimagepath_dont_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "New",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Нет титульной картинки')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValueSql: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "New",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValueSql: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValueSql: "('Нет титульной картинки')");
        }
    }
}
