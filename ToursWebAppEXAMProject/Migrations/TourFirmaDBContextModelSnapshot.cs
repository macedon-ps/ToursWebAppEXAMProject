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
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Полное описание темы блога')");

                    b.Property<string>("FullMessageLine")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Вся строка сообщений')");

                    b.Property<string>("Message")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("('Сообщение')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Заголовок блога')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Краткое описание темы блога')");

                    b.Property<string>("TitleImagePath")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Blog", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('страна')");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("LevelHotel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((2))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotel", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("HotelId");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Заголовок новости')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Краткое описание новости')");

                    b.Property<string>("TitleImagePath")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("New", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Название туристического продукта')");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Краткое описание туристического продукта')");

                    b.Property<string>("TitleImagePath")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Saller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DateTourId")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateTourId");

                    b.HasIndex("FoodId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("Tour", (string)null);
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

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Location", b =>
                {
                    b.HasOne("ToursWebAppEXAMProject.Models.City", "City")
                        .WithMany("Locations")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK__Location__CityId__35BCFE0A");

                    b.HasOne("ToursWebAppEXAMProject.Models.Country", "Country")
                        .WithMany("Locations")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__Location__Countr__34C8D9D1");

                    b.HasOne("ToursWebAppEXAMProject.Models.Hotel", "Hotel")
                        .WithMany("Locations")
                        .HasForeignKey("HotelId")
                        .IsRequired()
                        .HasConstraintName("FK__Location__HotelI__36B12243");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Hotel");
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

                    b.HasOne("ToursWebAppEXAMProject.Models.Location", "Location")
                        .WithMany("Tours")
                        .HasForeignKey("LocationId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__LocationId__412EB0B6");

                    b.HasOne("ToursWebAppEXAMProject.Models.Product", "Product")
                        .WithMany("Tours")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__ProductId__3F466844");

                    b.Navigation("DateTour");

                    b.Navigation("Food");

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.City", b =>
                {
                    b.Navigation("Hotels");

                    b.Navigation("Locations");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Locations");
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
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("ToursWebAppEXAMProject.Models.Location", b =>
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
