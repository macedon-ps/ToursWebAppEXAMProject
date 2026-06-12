using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _IncreaseTitleImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Product",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "New",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Country",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "('Нет титульной картинки страны')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки страны')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "City",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "('Нет титульной картинки города')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки города')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Blog",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Product",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "New",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Country",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "('Нет титульной картинки страны')",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки страны')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "City",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "('Нет титульной картинки города')",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки города')");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Blog",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "('Нет титульной картинки')",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "('Нет титульной картинки')");
        }
    }
}
