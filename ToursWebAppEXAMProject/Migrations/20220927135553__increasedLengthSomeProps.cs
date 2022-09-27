using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    public partial class _increasedLengthSomeProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tour",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Product",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValueSql: "('Краткое описание туристического продукта')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Краткое описание туристического продукта')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "('Название туристического продукта')",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "('Название туристического продукта')");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "New",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValueSql: "('Краткое описание новости')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Краткое описание новости')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "New",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "('Заголовок новости')",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "('Заголовок новости')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotel",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "('Заголовок блога')",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "('Заголовок блога')");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Blog",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValueSql: "('Сообщение')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "('Сообщение')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tour",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Краткое описание туристического продукта')",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldDefaultValueSql: "('Краткое описание туристического продукта')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('Название туристического продукта')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "('Название туристического продукта')");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "New",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Краткое описание новости')",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldDefaultValueSql: "('Краткое описание новости')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "New",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('Заголовок новости')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "('Заголовок новости')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('Заголовок блога')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "('Заголовок блога')");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Blog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "('Сообщение')",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldDefaultValueSql: "('Сообщение')");
        }
    }
}
