using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    public partial class _addedErrorAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Blog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "('Краткое описание темы блога')",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "('Краткое описание темы блога')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('Краткое описание темы блога')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "('Краткое описание темы блога')");
        }
    }
}
