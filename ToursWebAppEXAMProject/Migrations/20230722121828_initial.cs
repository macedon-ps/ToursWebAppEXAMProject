using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('Заголовок блога')"),
                    Message = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValueSql: "('Сообщение')"),
                    FullMessageLine = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Вся строка сообщений')"),
                    ShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('Краткое описание темы блога')"),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Полное описание темы блога')"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('Название страны')"),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValueSql: "('Краткое описание страны')"),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Полное описание страны')"),
                    Capital = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('Столица страны')"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Нет титульной картинки страны')"),
                    CountryMapPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('Нет ссылки на карту страны в GoogleMaps')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateTour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    NumberOfNights = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeOfEating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('Заголовок новости')"),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValueSql: "('Краткое описание новости')"),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Полное описание новости')"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Saller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('сотрудник')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechTaskViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('страница')"),
                    IsExecuteTechTask1 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask2 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask3 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask4 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask5 = table.Column<bool>(type: "bit", nullable: true),
                    IsExecuteTechTask6 = table.Column<bool>(type: "bit", nullable: true),
                    ExecuteTechTasksProgress = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('Название города')"),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValueSql: "('Краткое описание города')"),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Полное описание города')"),
                    isCapital = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(0)"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Нет титульной картинки города')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK__City__CountryId__2D27B809",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LevelHotel = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((2))"),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Hotel__CityId__31EC6D26",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('Название туристического продукта')"),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValueSql: "('Краткое описание туристического продукта')"),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('Полное описание туристического продукта')"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Products__CityId__34C8D9D1",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Products__CountryId__35BCFE0A",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateTourId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Tour__DateTourId__403A8C7D",
                        column: x => x.DateTourId,
                        principalTable: "DateTour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tour__FoodId__4222D4EF",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tour__HotelId__412EB0B6",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tour__ProductId__3F466844",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SallerId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Offer__Customer__4D94879B",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Offer__SallerId__4E88ABD4",
                        column: x => x.SallerId,
                        principalTable: "Saller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Offer__TourId__4F7CD00D",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CityId",
                table: "Hotel",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CustomerId",
                table: "Offer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_SallerId",
                table: "Offer",
                column: "SallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_TourId",
                table: "Offer",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CityId",
                table: "Product",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CountryId",
                table: "Product",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_DateTourId",
                table: "Tour",
                column: "DateTourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_FoodId",
                table: "Tour",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_HotelId",
                table: "Tour",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ProductId",
                table: "Tour",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "New");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "TechTaskViewModel");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Saller");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "DateTour");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
