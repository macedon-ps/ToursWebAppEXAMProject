using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services;

namespace ToursWebAppEXAMProject.DBContext
{
	public partial class TourFirmaDBContext : DbContext
	{
		public TourFirmaDBContext() {}

		public TourFirmaDBContext(DbContextOptions<TourFirmaDBContext> options): base(options)	{}

		public virtual DbSet<City> Cities { get; set; } = null!;
		public virtual DbSet<Country> Countries { get; set; } = null!;
		public virtual DbSet<Customer> Customers { get; set; } = null!;
		public virtual DbSet<DateTour> DateTours { get; set; } = null!;
		public virtual DbSet<Food> Foods { get; set; } = null!;
		public virtual DbSet<Hotel> Hotels { get; set; } = null!;
		public virtual DbSet<Location> Locations { get; set; } = null!;
		public virtual DbSet<Ofertum> Oferta { get; set; } = null!;
		public virtual DbSet<Product> Products { get; set; } = null!;
		public virtual DbSet<Saller> Sallers { get; set; } = null!;
		public virtual DbSet<Tour> Tours { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ConfigData.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>(entity =>
			{
				entity.ToTable("City");

				entity.Property(e => e.Name).HasMaxLength(50);

				entity.HasOne(d => d.Country)
					.WithMany(p => p.Cities)
					.HasForeignKey(d => d.CountryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__City__CountryId__29572725");
			});

			modelBuilder.Entity<Country>(entity =>
			{
				entity.ToTable("Country");

				entity.Property(e => e.Name)
					.HasMaxLength(50)
					.HasDefaultValueSql("('страна')");
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.ToTable("Customer");

				entity.Property(e => e.Gender).HasMaxLength(10);

				entity.Property(e => e.Name).HasMaxLength(50);

				entity.Property(e => e.Surname).HasMaxLength(50);
			});

			modelBuilder.Entity<DateTour>(entity =>
			{
				entity.ToTable("DateTour");

				entity.Property(e => e.DateEnd).HasColumnType("datetime");

				entity.Property(e => e.DateStart).HasColumnType("datetime");
			});

			modelBuilder.Entity<Food>(entity =>
			{
				entity.ToTable("Food");

				entity.Property(e => e.ModeOfEating).HasMaxLength(50);
			});

			modelBuilder.Entity<Hotel>(entity =>
			{
				entity.ToTable("Hotel");

				entity.Property(e => e.LevelHotel).HasDefaultValueSql("((2))");

				entity.Property(e => e.Name).HasMaxLength(50);

				entity.HasOne(d => d.City)
					.WithMany(p => p.Hotels)
					.HasForeignKey(d => d.CityId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Hotel__CityId__2E1BDC42");
			});

			modelBuilder.Entity<Location>(entity =>
			{
				entity.ToTable("Location");

				entity.HasOne(d => d.City)
					.WithMany(p => p.Locations)
					.HasForeignKey(d => d.CityId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Location__CityId__31EC6D26");

				entity.HasOne(d => d.Country)
					.WithMany(p => p.Locations)
					.HasForeignKey(d => d.CountryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Location__Countr__30F848ED");

				entity.HasOne(d => d.Hotel)
					.WithMany(p => p.Locations)
					.HasForeignKey(d => d.HotelId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Location__HotelI__32E0915F");
			});

			modelBuilder.Entity<Ofertum>(entity =>
			{
				entity.HasOne(d => d.Customer)
					.WithMany(p => p.Oferta)
					.HasForeignKey(d => d.CustomerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Oferta__Customer__571DF1D5");

				entity.HasOne(d => d.Saller)
					.WithMany(p => p.Oferta)
					.HasForeignKey(d => d.SallerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Oferta__SallerId__5812160E");

				entity.HasOne(d => d.Tour)
					.WithMany(p => p.Oferta)
					.HasForeignKey(d => d.TourId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Oferta__TourId__59063A47");
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.ToTable("Product");

				entity.Property(e => e.Name).HasMaxLength(50);
			});

			modelBuilder.Entity<Saller>(entity =>
			{
				entity.ToTable("Saller");

				entity.Property(e => e.Name).HasMaxLength(50);

				entity.Property(e => e.Position)
					.HasMaxLength(100)
					.HasDefaultValueSql("('сотрудник')");

				entity.Property(e => e.Surname).HasMaxLength(50);
			});

			modelBuilder.Entity<Tour>(entity =>
			{
				entity.ToTable("Tour");

				entity.Property(e => e.Name).HasMaxLength(50);

				entity.HasOne(d => d.DateTour)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.DateTourId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Tour__DateTourId__49C3F6B7");

				entity.HasOne(d => d.Food)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.FoodId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Tour__FoodId__4BAC3F29");

				entity.HasOne(d => d.Location)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.LocationId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Tour__LocationId__4AB81AF0");

				entity.HasOne(d => d.Product)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.ProductId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Tour__ProductId__48CFD27E");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
