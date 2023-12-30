﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToursWebAppEXAMProject.DBContext;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    [DbContext(typeof(TourFirmaDBContext))]
    partial class TourFirmaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание темы блога')");

                    b.Property<string>("FullMessageLine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Вся строка сообщений')");

                    b.Property<string>("Message")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasDefaultValueSql("('Сообщение')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Заголовок блога')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Краткое описание темы блога')");

                    b.Property<string>("TitleImagePath")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Нет титульной картинки')");

                    b.HasKey("Id");

                    b.ToTable("Blog", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание города')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Название города')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasDefaultValueSql("('Краткое описание города')");

                    b.Property<string>("TitleImagePath")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Нет титульной картинки города')");

                    b.Property<bool>("isCapital")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(0)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Capital")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Столица страны')");

                    b.Property<string>("CountryMapPath")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Нет ссылки на карту страны в GoogleMaps')");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание страны')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Название страны')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasDefaultValueSql("('Краткое описание страны')");

                    b.Property<string>("TitleImagePath")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Нет титульной картинки страны')");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.DateTour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfNights")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DateTour", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ModeOfEating")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Food", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("LevelHotel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((2))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotel", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание новости')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Заголовок новости')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasDefaultValueSql("('Краткое описание новости')");

                    b.Property<string>("TitleImagePath")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Нет титульной картинки')");

                    b.HasKey("Id");

                    b.ToTable("New", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("SallerId")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SallerId");

                    b.HasIndex("TourId");

                    b.ToTable("Offer", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание туристического продукта')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Название туристического продукта')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasDefaultValueSql("('Краткое описание туристического продукта')");

                    b.Property<string>("TitleImagePath")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Нет титульной картинки')");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Saller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('сотрудник')");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Saller", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DateTourId")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateTourId");

                    b.HasIndex("FoodId");

                    b.HasIndex("HotelId");

                    b.HasIndex("ProductId");

                    b.ToTable("Tour", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.ViewModels.TechTaskViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("ExecuteTechTasksProgress")
                        .HasColumnType("float");

                    b.Property<bool?>("IsExecuteTechTask1")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecuteTechTask2")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecuteTechTask3")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecuteTechTask4")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecuteTechTask5")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecuteTechTask6")
                        .HasColumnType("bit");

                    b.Property<string>("PageName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValueSql("('страница')");

                    b.HasKey("Id");

                    b.ToTable("TechTaskViewModel", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToursWebAppEXAMProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.City", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__City__CountryId__2D27B809");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Hotel", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK__Hotel__CityId__31EC6D26");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Offer", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.Customer", "Customer")
                        .WithMany("Offer")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Offer__Customer__4D94879B");

                    b.HasOne("ToursWebAppEXAMProject.Models.Saller", "Saller")
                        .WithMany("Offer")
                        .HasForeignKey("SallerId")
                        .IsRequired()
                        .HasConstraintName("FK__Offer__SallerId__4E88ABD4");

                    b.HasOne("ToursWebAppEXAMProject.Models.Tour", "Tour")
                        .WithMany("Offer")
                        .HasForeignKey("TourId")
                        .IsRequired()
                        .HasConstraintName("FK__Offer__TourId__4F7CD00D");

                    b.Navigation("Customer");

                    b.Navigation("Saller");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Product", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.City", "City")
                        .WithMany("Products")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK__Products__CityId__34C8D9D1");

                    b.HasOne("ToursWebAppEXAMProject.Models.Country", "Country")
                        .WithMany("Products")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__Products__CountryId__35BCFE0A");

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Tour", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.DateTour", "DateTour")
                        .WithMany("Tours")
                        .HasForeignKey("DateTourId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__DateTourId__403A8C7D");

                    b.HasOne("ToursWebAppEXAMProject.Models.Food", "Food")
                        .WithMany("Tours")
                        .HasForeignKey("FoodId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__FoodId__4222D4EF");

                    b.HasOne("ToursWebAppEXAMProject.Models.Hotel", "Hotel")
                        .WithMany("Tours")
                        .HasForeignKey("HotelId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__HotelId__412EB0B6");

                    b.HasOne("ToursWebAppEXAMProject.Models.Product", "Product")
                        .WithMany("Tours")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__ProductId__3F466844");

                    b.Navigation("DateTour");

                    b.Navigation("Food");

                    b.Navigation("Hotel");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.City", b =>
                {
                    b.Navigation("Hotels");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Customer", b =>
                {
                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.DateTour", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Food", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Hotel", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Product", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Saller", b =>
                {
                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Tour", b =>
                {
                    b.Navigation("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
