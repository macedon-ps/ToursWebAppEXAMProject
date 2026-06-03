using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutPageVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Keyword = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsActual = table.Column<bool>(type: "boolean", nullable: false),
                    MainTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AboutTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DetailsTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OperationModeTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhotoGalleryTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FeedbackTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MainShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AboutShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DetailsShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OperationModeShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PhotoGalleryShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FeedbackShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MainFullDescription = table.Column<string>(type: "text", nullable: true),
                    AboutFullDescription = table.Column<string>(type: "text", nullable: true),
                    DetailsFullDescription = table.Column<string>(type: "text", nullable: true),
                    OperationModeFullDescription = table.Column<string>(type: "text", nullable: true),
                    PhotoGalleryFullDescription = table.Column<string>(type: "text", nullable: true),
                    FeedbackFullDescription = table.Column<string>(type: "text", nullable: true),
                    MainImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AboutImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DetailsImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OperationModeImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhotoGalleryImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FeedbackImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutPageVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Askers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCustomer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Askers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BirthYear = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: "('Заголовок блога')"),
                    Message = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false, defaultValue: "('Сообщение')"),
                    FullMessageLine = table.Column<string>(type: "text", nullable: true, defaultValue: "('Вся строка сообщений')"),
                    ShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: "('Краткое описание темы блога')"),
                    FullDescription = table.Column<string>(type: "text", nullable: false, defaultValue: "('Полное описание темы блога')"),
                    TitleImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "('Название страны')"),
                    ShortDescription = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false, defaultValue: "('Краткое описание страны')"),
                    FullDescription = table.Column<string>(type: "text", nullable: false, defaultValue: "('Полное описание страны')"),
                    Capital = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "('Столица страны')"),
                    TitleImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "('Нет титульной картинки страны')"),
                    CountryMapPath = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValue: "('Нет ссылки на карту страны в GoogleMaps')"),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateTour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfDays = table.Column<int>(type: "integer", nullable: false),
                    NumberOfNights = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModeOfEating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: "('Заголовок новости')"),
                    ShortDescription = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false, defaultValue: "('Краткое описание новости')"),
                    FullDescription = table.Column<string>(type: "text", nullable: false, defaultValue: "('Полное описание новости')"),
                    TitleImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Saller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "('сотрудник')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechTaskPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AboutPageVersionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoGalleryImages_AboutPageVersions_AboutPageVersionId",
                        column: x => x.AboutPageVersionId,
                        principalTable: "AboutPageVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Correspondences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    QuestionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Answer = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    AnswerDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    AskerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correspondences_Askers_AskerId",
                        column: x => x.AskerId,
                        principalTable: "Askers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "('Название города')"),
                    ShortDescription = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false, defaultValue: "('Краткое описание города')"),
                    LocalDescription = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: false, defaultValue: "('Полное описание города')"),
                    isCapital = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TitleImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "('Нет титульной картинки города')"),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "TechTaskItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechTaskPageId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTaskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechTaskItem_TechTaskPages_TechTaskPageId",
                        column: x => x.TechTaskPageId,
                        principalTable: "TechTaskPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LevelHotel = table.Column<int>(type: "integer", nullable: false, defaultValue: 2),
                    CityId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: "('Название туристического продукта')"),
                    ShortDescription = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false, defaultValue: "('Краткое описание туристического продукта')"),
                    FullDescription = table.Column<string>(type: "text", nullable: false, defaultValue: "('Полное описание туристического продукта')"),
                    TitleImagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "('Нет титульной картинки')"),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateTourId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    FoodId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    SallerId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.InsertData(
                table: "TechTaskPages",
                columns: new[] { "Id", "PageName" },
                values: new object[,]
                {
                    { 1, "Home" },
                    { 2, "Search" },
                    { 3, "Edit" },
                    { 4, "Support" },
                    { 5, "About" },
                    { 6, "Common" }
                });

            migrationBuilder.InsertData(
                table: "TechTaskItem",
                columns: new[] { "Id", "Description", "IsCompleted", "OrderNumber", "TechTaskPageId" },
                values: new object[,]
                {
                    { 1, "Данная страница д.содержать:\r\n			<br /><strong>блок навигационных кнопок:</strong>\r\n			\"Новости со всего мира\", \"Блоги о туризме\", \"ТЗ для Home\"\r\n			<br /><strong>большую пейзажную фоновую картинку</strong>\r\n			(м.б. море, тропический остров с пальмами)\r\n			<br /><strong>списки новостей и блогов о туризме с вертикальными скроллами</strong>,\r\n			которые загружаются при открытии страницы вертикальными полосами слева и справа;", false, 1, 1 },
                    { 2, "Д.б. создана <strong>страница со списком новостей</strong>,\r\n			которая открывается по нажатию на кнопку\r\n			<strong>\"Новости со всего мира\"</strong>.\r\n			<br />В списке новостей <strong>для каждой новости</strong>\r\n			указываются:\r\n			титульная картинка,\r\n			название новости,\r\n			краткое описание,\r\n			полное описание,\r\n			ссылка на полное описание.\r\n			<br />При нажатии на ссылку\r\n			<strong>\"Подробнее\"</strong>\r\n			открывается новость с:\r\n			титульной картинкой,\r\n			названием,\r\n			полным описанием,\r\n			id новости и датой сохранения.", false, 2, 1 },
                    { 3, "Д.б. создана <strong>страница со списком блогов</strong>,\r\n			которая открывается по нажатию на кнопку\r\n			<strong>\"Блоги о туризме\"</strong>.\r\n			<br />В списке блогов <strong>для каждого блога</strong>\r\n			указываются:\r\n			титульная картинка,\r\n			название блога,\r\n			краткое описание,\r\n			ссылка на полное описание.\r\n			<br />При нажатии на ссылку\r\n			<strong>\"Подробнее\"</strong>\r\n			открывается блог с:\r\n			титульной картинкой,\r\n			названием,\r\n			полным описанием,\r\n			сообщениями блоггеров,\r\n			id блога и датой сохранения.", false, 3, 1 },
                    { 4, "Д.б. реализована\r\n			<strong>возможность создания текстовых сообщений блоггеров,\r\n			редактирования и удаления со стороны редактора сайта</strong>.\r\n			<br />Д.б. реализован\r\n			<strong>механизм получения, пересылки и сохранения текстовых сообщений блоггеров</strong>.", false, 4, 1 },
                    { 5, "Д.б. реализована\r\n			<strong>функциональность оценивания прогресса выполнения технических задач\r\n			для каждой страницы</strong>.\r\n			<br />По нажатию на кнопки:\r\n			<strong>\"ТЗ для Home\"</strong>,\r\n			<strong>\"ТЗ для Search\"</strong>,\r\n			<strong>\"ТЗ для Admin\"</strong>,\r\n			<strong>\"ТЗ для Support\"</strong>,\r\n			<strong>\"ТЗ для About\"</strong>\r\n			открываются соответствующие страницы\r\n			со шкалой прогресса\r\n			и возможностью редактирования и сохранения выполненных ТЗ.", false, 5, 1 },
                    { 6, "Дополнительно:\r\n			<br />М.б. создан\r\n			<strong>дополнительный функционал:</strong>\r\n			<strong>\"последние прочитанные новости/блоги\"</strong>,\r\n			<strong>\"статистика прочтения новостей/\r\n			написания сообщений в блогах\"</strong>,\r\n			<strong>\"чаще всего читают новости/блоги\"</strong>\r\n			и т.д.", false, 6, 1 },
                    { 7, "Данная страница д.содержать:\r\n							<br /><strong>блок навигационных кнопок: </strong> \"Поиск туров\", \"Все турпродуктв\", \"Все страны\", \"Все города\", \"ТЗ для Search\"\r\n							< br />< strong > меню поиска:</ strong > страны,\r\n					города,\r\n					дат тура,\r\n					количества туристов и кнопка \"Найти тур\"\r\n							< br />< strong > информативное меню:</ strong > карта местности и описания страны,\r\n					города,\r\n					достопримечательностей\r\n							< br />< strong > галерея с фото:</ strong > фотографии страны,\r\n					городов,\r\n					достопримечательностей\r\n\r\n							< br /> По нажатию на кнопку < strong > \"Поиск туров\" или \"Найти тур\" </ strong > осуществляется < strong > функционал поиска турпродукта по заданным параметрам поиска </ strong > и выводится < strong > список всех найденных туристических продуктов </ strong > с вертикальным скроллом", false, 1, 2 },
                    { 8, "<strong>Меню поиска</strong> д.содержать: \r\n							<br/><strong>выпадающий список стран</strong> (элемент radiobutton - выбор только 1 страны);\r\n							<br/><strong>выпадающие календари</strong> с <strong>датами начала и окончания тура</strong>);\r\n							<br/><strong>выпадающий список - количество дней/ночей</strong> (элемент select - выбор 1 или больше вариантов);\r\n							<br/><strong>выпадающий список людей</strong> (взрослых) + возможность добавить детей (количество и возраст детей) (по умолчанию - 2 взрослых, без детей)\r\n							<br/><strong>кнопка \"Найти тур\"</strong>;\r\n							< br /> Д.б.организован < strong > поиск туристических продуктов по заданным параметрам </ strong > ", false, 2, 2 },
                    { 9, "<strong>Информативное меню</strong> под меню поиска, д. содержать:\r\n							<br/><strong>раскрытый список городов</strong> данной страны;\r\n							<br />(элемент select - выбор 1 или больше городов) (расположено - под страной) (при выборе страны - появляются города этой страны);\r\n							<br/><strong>д.б. карта местности</strong> (рядом с городами):\r\n							<br/><strong>описание:</strong>\r\n							<br />при выборе страны - в верхней трети - появляется <strong>описание о стране</strong>\r\n							<br />при выборе города - в средней трети - появляется <strong>описание о городе</strong>\r\n							<br />а в нижней трети - появляется <strong>описание о достопримечательности</strong>", false, 3, 2 },
                    { 10, "Д.б. реализована <strong>возможность просматривать карту страны</strong>, с нанесенными на нее городами и турпродуктами\r\n							", false, 4, 2 },
                    { 11, "Д.б. реализована <strong>галерея с фото страны, городов и достопримечательностей</strong>\r\n							<br />выводятся все фото, кот. есть в базе; если их много и они не помещаются, то появляется горизонт. скроллинг<br/>", false, 5, 2 },
                    { 12, "Дополнительно:\r\n							<br />м.б. реализована <strong>галерея с фото из гостинниц, кафе, других мест</strong>", false, 6, 2 },
                    { 13, "Данная страница д. содержать:\r\n							<br/><strong>блок навигационных кнопок: </strong> \"Редактировать новости\", \"Редактировать блоги\", \"Редактировать турпродукты\", \"ТЗ для Edit\";\r\n							< br />< strong > общее меню поиска сущностей(новостей / блогов / турпродуктов) по их полному названию / или по ключевому слову </ strong > из названия сущности для выборки из БД;\r\n							< br />< strong > страницу со списком выбранных сущностей </ strong >,\r\n					отвечающих критериям поиска.", false, 1, 3 },
                    { 14, "Д.б. создана <strong>страница создания, редактирования и удаления новости</strong>, ее названия, краткого и полного описания, пути к титульной картинке", false, 2, 3 },
                    { 15, "Д.б. создана <strong>страница создания, редактирования и удаления блога</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 3, 3 },
                    { 16, "Д.б. создана <strong>страница создания, редактирования и удаления туристического продукта</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 4, 3 },
                    { 17, "Д.б. организован <strong>доступ для входа на страницу - для редактирования данных БД</strong> - только для сотрудников турфирмы, у кот. <strong> роль \"superadmin\" илм \"editor\"</strong>", false, 5, 3 },
                    { 18, "Дополнительно:\r\n							<br />М.б. созданы <strong>3 роли с разным доступом: </strong>\r\n							<br />М.б. создана <strong>страница создания, редактирования и удаления страны</strong>, ее названия, краткого и полного описания, пути к титульной картинке\r\n							<br />М.б. создана <strong>страница создания, редактирования и удаления города</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 6, 3 },
                    { 19, "Данная страница д. содержать:\r\n							<br /><strong>блок навигационных кнопок:</strong> \"Перевод текста\", \"Карта\" или \"Прогноз погоды\", \"ТЗ для Support\";\r\n							< br /> по нажатию на кнопку < strong > \"Перевод текста\" </ strong > -раскрывается область,\r\n					в кот.слева - текст,\r\n					кот.\r\n							н.перевести,\r\n					а справа - перевод на иностр.язык\r\n							< br /> по нажатию на кнопку < strong > \"Карта\" </ strong > -раскрывается область,\r\n					в кот.появляется карта данной местности\r\n							< br /> по нажатию на кнопку < strong > \"Прогноз погоды\" </ strong > -раскрывается область,\r\n					в кот.появляется прогноз погоды для данной местности", false, 1, 4 },
                    { 20, "Д.б. создана <strong>страница с возможностью перевода текстов на иностранный язык</strong> с использованием <strong>Google Translate API</strong>", false, 2, 4 },
                    { 21, "Д.б. создана <strong>страница с выводимой картой местности</strong>, с использованием <strong>Google Map API</strong>", false, 3, 4 },
                    { 22, "М.б. создана <strong>страница с прогнозом погоды для данной местности</strong>, с использованием <strong>Open Weather API</strong>", false, 4, 4 },
                    { 23, "М.б. создана <strong>страница с информацией о странах, об их географическом положении, об их достопримечательностях</strong>", false, 5, 4 },
                    { 24, "Дополнительно:\r\n							<br />М.б. создана <strong>страница с информацией о странах, об их истории, политическом устройстве, праве, традициях и обычаях местного населения, об объектах сферы гостеприимства (гостинницы, кафе и т.д.)</strong>", false, 6, 4 },
                    { 25, "Данная страница д. содержать:\r\n							<br/><strong>блок навигационных кнопок:</strong> \"Наши реквизиты\", \"Режим работы\", \"Фотогаллерея\", \"Обратная связь\", \"Edit AboutPage\", \"ТЗ для About\"\r\n							< br />< strong > блок рекизитов тур.фирмы </ strong >\r\n							< br />< strong > блок с информацией о режиме работы турфирмы </ strong >\r\n							< br />< strong > блок с фотографиями путешествий по странам мира </ strong > ", false, 1, 5 },
                    { 26, "Д.б. <strong>страница для создания, редактирования и удаления разных версий страницы About</strong>.\r\n							<br />Д.б. организован <strong>доступ к данной функциональности по ролям \"superadmin\", \"editor\"</strong>", false, 2, 5 },
                    { 27, "Д.б. <strong>страница с формой обратной связи с клиентами туристической фирмы</strong>\r\n							<br />Д.б. реализован <strong>функционал создания, отправки, сохранения вопросов от клиентов и просмотра  ответов на них от туристической фирмы</strong> (+ для боковой панели)", false, 3, 5 },
                    { 28, "Д.б. реализована возможность <strong>звонить из приложения и отправлять сообщения по email</strong> (+ для боковой панели)", false, 4, 5 },
                    { 29, "Д.б. реализована возможность <strong>общения с клиентами через социальные сети Facebook, WhatsApp, Telegram, Viber</strong> (+ для боковой панели)", false, 5, 5 },
                    { 30, "Дополнительно:\r\n							<br />М.б. реализован <strong>функционал подсчета стоимости поездки</strong> (+ для боковой панели)", false, 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_AskerId",
                table: "Correspondences",
                column: "AskerId");

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
                name: "IX_PhotoGalleryImages_AboutPageVersionId",
                table: "PhotoGalleryImages",
                column: "AboutPageVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CityId",
                table: "Product",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CountryId",
                table: "Product",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechTaskItem_OrderNumber",
                table: "TechTaskItem",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TechTaskItem_TechTaskPageId",
                table: "TechTaskItem",
                column: "TechTaskPageId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Correspondences");

            migrationBuilder.DropTable(
                name: "New");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "PhotoGalleryImages");

            migrationBuilder.DropTable(
                name: "TechTaskItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Askers");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Saller");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "AboutPageVersions");

            migrationBuilder.DropTable(
                name: "TechTaskPages");

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
