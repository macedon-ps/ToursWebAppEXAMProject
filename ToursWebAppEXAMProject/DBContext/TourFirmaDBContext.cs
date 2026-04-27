using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ConfigFiles;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.DBContext
{
	public partial class TourFirmaDBContext : IdentityDbContext<User>
	{
		public TourFirmaDBContext() { }

		public TourFirmaDBContext(DbContextOptions<TourFirmaDBContext> options) : base(options) { }

		public virtual DbSet<Blog> Blogs { get; set; } = null!;
		public virtual DbSet<City> Cities { get; set; } = null!;
		public virtual DbSet<Country> Countries { get; set; } = null!;
		public virtual DbSet<Correspondence> Correspondences { get; set; } = null!;
		public virtual DbSet<Customer> Customers { get; set; } = null!;
		public virtual DbSet<Asker> Askers { get; set; } = null!;
		public virtual DbSet<DateTour> DateTours { get; set; } = null!;
		public virtual DbSet<Food> Foods { get; set; } = null!;
		public virtual DbSet<Hotel> Hotels { get; set; } = null!;
		public virtual DbSet<New> News { get; set; } = null!;
		public virtual DbSet<Offer> Offers { get; set; } = null!;
		public virtual DbSet<Product> Products { get; set; } = null!;
		public virtual DbSet<Saller> Sallers { get; set; } = null!;
		public virtual DbSet<Tour> Tours { get; set; } = null!;
		public virtual DbSet<TechTaskPage> TechTaskPages { get; set; } = null!;
		public virtual DbSet<TechTaskItem> TechTaskItems { get; set; } = null!;
		public virtual DbSet<AboutPageVersion> AboutPageVersions { get; set; } = null!;
		public virtual DbSet<PhotoGalleryImage> PhotoGalleryImages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ConfigData.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            // устранение ошибки определения первичного ключа для IdentityUsersLogins
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Blog>(entity =>
			{
				entity.ToTable("Blog");

				entity.Property(e => e.Name)
					.HasMaxLength(200)
					.HasDefaultValueSql("('Заголовок блога')");

				entity.Property(e => e.Message)
					.HasMaxLength(400)
					.HasDefaultValueSql("('Сообщение')");

				entity.Property(e => e.FullMessageLine).HasDefaultValueSql("('Вся строка сообщений')");

				entity.Property(e => e.ShortDescription)
					.HasMaxLength(200)
					.HasDefaultValueSql("('Краткое описание темы блога')");

				entity.Property(e => e.FullDescription).HasDefaultValueSql("('Полное описание темы блога')");

				entity.Property(e => e.TitleImagePath)
					.HasMaxLength(100)
					.HasDefaultValueSql("('Нет титульной картинки')");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");
			});

			modelBuilder.Entity<City>(entity =>
			{
				entity.ToTable("City");

				entity.Property(e => e.Name)
					.HasMaxLength(50)
                    .HasDefaultValueSql("('Название города')");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(400)
                    .HasDefaultValueSql("('Краткое описание города')");

                entity.Property(e => e.FullDescription).HasDefaultValueSql("('Полное описание города')");

                entity.Property(e => e.isCapital).HasDefaultValueSql("(0)");

                entity.Property(e => e.TitleImagePath)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Нет титульной картинки города')");

                entity.HasOne(d => d.Country)
					.WithMany(p => p.Cities)
					.HasForeignKey(d => d.CountryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__City__CountryId__2D27B809");
			});

			modelBuilder.Entity<Country>(entity =>
			{
				entity.ToTable("Country");

				entity.Property(e => e.Name)
					.HasMaxLength(50)
					.HasDefaultValueSql("('Название страны')");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(400)
                    .HasDefaultValueSql("('Краткое описание страны')");

                entity.Property(e => e.FullDescription).HasDefaultValueSql("('Полное описание страны')");

                entity.Property(e => e.Capital)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Столица страны')");

                entity.Property(e => e.TitleImagePath)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Нет титульной картинки страны')");

                entity.Property(e => e.CountryMapPath)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('Нет ссылки на карту страны в GoogleMaps')");

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

				entity.Property(e => e.Name).HasMaxLength(200);

				entity.HasOne(d => d.City)
					.WithMany(p => p.Hotels)
					.HasForeignKey(d => d.CityId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hotel__CityId__31EC6D26");
            });

			modelBuilder.Entity<New>(entity =>
			{
				entity.ToTable("New");

				entity.Property(e => e.Name)
					.HasMaxLength(200)
					.HasDefaultValueSql("('Заголовок новости')");

				entity.Property(e => e.ShortDescription)
					.HasMaxLength(400)
					.HasDefaultValueSql("('Краткое описание новости')");

				entity.Property(e => e.FullDescription).HasDefaultValueSql("('Полное описание новости')");

				entity.Property(e => e.TitleImagePath)
					.HasMaxLength(100)
                    .HasDefaultValueSql("('Нет титульной картинки')");
				
				entity.Property(e => e.DateAdded).HasColumnType("datetime");
			});

			modelBuilder.Entity<Offer>(entity =>
			{
				entity.ToTable("Offer");

				entity.HasOne(d => d.Customer)
					.WithMany(p => p.Offer)
					.HasForeignKey(d => d.CustomerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__Customer__4D94879B");

                entity.HasOne(d => d.Saller)
					.WithMany(p => p.Offer)
					.HasForeignKey(d => d.SallerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__SallerId__4E88ABD4");

                entity.HasOne(d => d.Tour)
					.WithMany(p => p.Offer)
					.HasForeignKey(d => d.TourId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__TourId__4F7CD00D");
            });

			modelBuilder.Entity<Product>(entity =>
			{
				entity.ToTable("Product");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('Название туристического продукта')");

                entity.HasOne(d => d.Country)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.CountryId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__Products__CountryId__35BCFE0A");

                entity.HasOne(d => d.City)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.CityId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__Products__CityId__34C8D9D1");

                entity.Property(e => e.ShortDescription)
					.HasMaxLength(400)
					.HasDefaultValueSql("('Краткое описание туристического продукта')");

				entity.Property(e => e.FullDescription).HasDefaultValueSql("('Полное описание туристического продукта')");

				entity.Property(e => e.TitleImagePath)
					.HasMaxLength(100)
                    .HasDefaultValueSql("('Нет титульной картинки')");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

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

				entity.Property(e => e.Name).HasMaxLength(200);

				entity.HasOne(d => d.DateTour)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.DateTourId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tour__DateTourId__403A8C7D");

                entity.HasOne(d => d.Food)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.FoodId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tour__FoodId__4222D4EF");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tour__HotelId__412EB0B6");

                entity.HasOne(d => d.Product)
					.WithMany(p => p.Tours)
					.HasForeignKey(d => d.ProductId)
					.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tour__ProductId__3F466844");
            });

			modelBuilder.Entity<TechTaskPage>().HasData(

				new TechTaskPage
				{
					Id = 1,
					PageName = "Home"
				},

				new TechTaskPage
				{
					Id = 2,
					PageName = "Search"
				},

				new TechTaskPage
				{
					Id = 3,
					PageName = "Edit"
				},

				new TechTaskPage
				{
					Id = 4,
					PageName = "Support"
				},

				new TechTaskPage
				{
					Id = 5,
					PageName = "About"
				},

				new TechTaskPage
				{
					Id = 6,
					PageName = "Common"
				}
			);

            modelBuilder.Entity<TechTaskItem>().HasData(

				new TechTaskItem
				{
					Id = 1,
					TechTaskPageId = 1,
					OrderNumber = 1,
					IsCompleted = false,

					Description =
			@"Данная страница д.содержать:
			<br /><strong>блок навигационных кнопок:</strong>
			""Новости со всего мира"", ""Блоги о туризме"", ""ТЗ для Home""
			<br /><strong>большую пейзажную фоновую картинку</strong>
			(м.б. море, тропический остров с пальмами)
			<br /><strong>списки новостей и блогов о туризме с вертикальными скроллами</strong>,
			которые загружаются при открытии страницы вертикальными полосами слева и справа;"
				},

				new TechTaskItem
				{
					Id = 2,
					TechTaskPageId = 1,
					OrderNumber = 2,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница со списком новостей</strong>,
			которая открывается по нажатию на кнопку
			<strong>""Новости со всего мира""</strong>.
			<br />В списке новостей <strong>для каждой новости</strong>
			указываются:
			титульная картинка,
			название новости,
			краткое описание,
			полное описание,
			ссылка на полное описание.
			<br />При нажатии на ссылку
			<strong>""Подробнее""</strong>
			открывается новость с:
			титульной картинкой,
			названием,
			полным описанием,
			id новости и датой сохранения."
				},

				new TechTaskItem
				{
					Id = 3,
					TechTaskPageId = 1,
					OrderNumber = 3,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница со списком блогов</strong>,
			которая открывается по нажатию на кнопку
			<strong>""Блоги о туризме""</strong>.
			<br />В списке блогов <strong>для каждого блога</strong>
			указываются:
			титульная картинка,
			название блога,
			краткое описание,
			ссылка на полное описание.
			<br />При нажатии на ссылку
			<strong>""Подробнее""</strong>
			открывается блог с:
			титульной картинкой,
			названием,
			полным описанием,
			сообщениями блоггеров,
			id блога и датой сохранения."
				},

				new TechTaskItem
				{
					Id = 4,
					TechTaskPageId = 1,
					OrderNumber = 4,
					IsCompleted = false,

					Description =
			@"Д.б. реализована
			<strong>возможность создания текстовых сообщений блоггеров,
			редактирования и удаления со стороны редактора сайта</strong>.
			<br />Д.б. реализован
			<strong>механизм получения, пересылки и сохранения текстовых сообщений блоггеров</strong>."
				},

				new TechTaskItem
				{
					Id = 5,
					TechTaskPageId = 1,
					OrderNumber = 5,
					IsCompleted = false,

					Description =
			@"Д.б. реализована
			<strong>функциональность оценивания прогресса выполнения технических задач
			для каждой страницы</strong>.
			<br />По нажатию на кнопки:
			<strong>""ТЗ для Home""</strong>,
			<strong>""ТЗ для Search""</strong>,
			<strong>""ТЗ для Admin""</strong>,
			<strong>""ТЗ для Support""</strong>,
			<strong>""ТЗ для About""</strong>
			открываются соответствующие страницы
			со шкалой прогресса
			и возможностью редактирования и сохранения выполненных ТЗ."
				},

				new TechTaskItem
				{
					Id = 6,
					TechTaskPageId = 1,
					OrderNumber = 6,
					IsCompleted = false,

					Description =
			@"Дополнительно:
			<br />М.б. создан
			<strong>дополнительный функционал:</strong>
			<strong>""последние прочитанные новости/блоги""</strong>,
			<strong>""статистика прочтения новостей/
			написания сообщений в блогах""</strong>,
			<strong>""чаще всего читают новости/блоги""</strong>
			и т.д."
				}

			);

            modelBuilder.Entity<TechTaskItem>().HasData(

				new TechTaskItem
				{
					Id = 7,
					TechTaskPageId = 2,
					OrderNumber = 1,
					IsCompleted = false,

					Description =
			@"Данная страница д.содержать:
							<br /><strong>блок навигационных кнопок: </strong> ""Поиск туров"", ""Все турпродуктв"", ""Все страны"", ""Все города"", ""ТЗ для Search""
							< br />< strong > меню поиска:</ strong > страны,
					города,
					дат тура,
					количества туристов и кнопка ""Найти тур""
							< br />< strong > информативное меню:</ strong > карта местности и описания страны,
					города,
					достопримечательностей
							< br />< strong > галерея с фото:</ strong > фотографии страны,
					городов,
					достопримечательностей

							< br /> По нажатию на кнопку < strong > ""Поиск туров"" или ""Найти тур"" </ strong > осуществляется < strong > функционал поиска турпродукта по заданным параметрам поиска </ strong > и выводится < strong > список всех найденных туристических продуктов </ strong > с вертикальным скроллом"
				},

				new TechTaskItem
				{
					Id = 8,
					TechTaskPageId = 2,
					OrderNumber = 2,
					IsCompleted = false,

					Description =
			@"<strong>Меню поиска</strong> д.содержать: 
							<br/><strong>выпадающий список стран</strong> (элемент radiobutton - выбор только 1 страны);
							<br/><strong>выпадающие календари</strong> с <strong>датами начала и окончания тура</strong>);
							<br/><strong>выпадающий список - количество дней/ночей</strong> (элемент select - выбор 1 или больше вариантов);
							<br/><strong>выпадающий список людей</strong> (взрослых) + возможность добавить детей (количество и возраст детей) (по умолчанию - 2 взрослых, без детей)
							<br/><strong>кнопка ""Найти тур""</strong>;
							< br /> Д.б.организован < strong > поиск туристических продуктов по заданным параметрам </ strong > "
				},

				new TechTaskItem
				{
					Id = 9,
					TechTaskPageId = 2,
					OrderNumber = 3,
					IsCompleted = false,

					Description =
			@"<strong>Информативное меню</strong> под меню поиска, д. содержать:
							<br/><strong>раскрытый список городов</strong> данной страны;
							<br />(элемент select - выбор 1 или больше городов) (расположено - под страной) (при выборе страны - появляются города этой страны);
							<br/><strong>д.б. карта местности</strong> (рядом с городами):
							<br/><strong>описание:</strong>
							<br />при выборе страны - в верхней трети - появляется <strong>описание о стране</strong>
							<br />при выборе города - в средней трети - появляется <strong>описание о городе</strong>
							<br />а в нижней трети - появляется <strong>описание о достопримечательности</strong>"
				},

				new TechTaskItem
				{
					Id = 10,
					TechTaskPageId = 2,
					OrderNumber = 4,
					IsCompleted = false,

					Description =
			@"Д.б. реализована <strong>возможность просматривать карту страны</strong>, с нанесенными на нее городами и турпродуктами
							"
				},

				new TechTaskItem
				{
					Id = 11,
					TechTaskPageId = 2,
					OrderNumber = 5,
					IsCompleted = false,

					Description =
			@"Д.б. реализована <strong>галерея с фото страны, городов и достопримечательностей</strong>
							<br />выводятся все фото, кот. есть в базе; если их много и они не помещаются, то появляется горизонт. скроллинг<br/>"
				},

				new TechTaskItem
				{
					Id = 12,
					TechTaskPageId = 2,
					OrderNumber = 6,
					IsCompleted = false,

					Description =
			@"Дополнительно:
							<br />м.б. реализована <strong>галерея с фото из гостинниц, кафе, других мест</strong>"
				}

			);

            modelBuilder.Entity<TechTaskItem>().HasData(

				new TechTaskItem
				{
					Id = 13,
					TechTaskPageId = 3,
					OrderNumber = 1,
					IsCompleted = false,

					Description =
			@"Данная страница д. содержать:
							<br/><strong>блок навигационных кнопок: </strong> ""Редактировать новости"", ""Редактировать блоги"", ""Редактировать турпродукты"", ""ТЗ для Edit"";
							< br />< strong > общее меню поиска сущностей(новостей / блогов / турпродуктов) по их полному названию / или по ключевому слову </ strong > из названия сущности для выборки из БД;
							< br />< strong > страницу со списком выбранных сущностей </ strong >,
					отвечающих критериям поиска."
				},

				new TechTaskItem
				{
					Id = 14,
					TechTaskPageId = 3,
					OrderNumber = 2,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница создания, редактирования и удаления новости</strong>, ее названия, краткого и полного описания, пути к титульной картинке"
				},

				new TechTaskItem
				{
					Id = 15,
					TechTaskPageId = 3,
					OrderNumber = 3,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница создания, редактирования и удаления блога</strong>, его названия, краткого и полного описания, пути к титульной картинке"
				},

				new TechTaskItem
				{
					Id = 16,
					TechTaskPageId = 3,
					OrderNumber = 4,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница создания, редактирования и удаления туристического продукта</strong>, его названия, краткого и полного описания, пути к титульной картинке"
				},

				new TechTaskItem
				{
					Id = 17,
					TechTaskPageId = 3,
					OrderNumber = 5,
					IsCompleted = false,

					Description =
			@"Д.б. организован <strong>доступ для входа на страницу - для редактирования данных БД</strong> - только для сотрудников турфирмы, у кот. <strong> роль ""superadmin"" илм ""editor""</strong>"
				},

				new TechTaskItem
				{
					Id = 18,
					TechTaskPageId = 3,
					OrderNumber = 6,
					IsCompleted = false,

					Description =
			@"Дополнительно:
							<br />М.б. созданы <strong>3 роли с разным доступом: </strong>
							<br />М.б. создана <strong>страница создания, редактирования и удаления страны</strong>, ее названия, краткого и полного описания, пути к титульной картинке
							<br />М.б. создана <strong>страница создания, редактирования и удаления города</strong>, его названия, краткого и полного описания, пути к титульной картинке"
				}

			);

            modelBuilder.Entity<TechTaskItem>().HasData(

				new TechTaskItem
				{
					Id = 19,
					TechTaskPageId = 4,
					OrderNumber = 1,
					IsCompleted = false,

					Description =
			@"Данная страница д. содержать:
							<br /><strong>блок навигационных кнопок:</strong> ""Перевод текста"", ""Карта"" или ""Прогноз погоды"", ""ТЗ для Support"";
							< br /> по нажатию на кнопку < strong > ""Перевод текста"" </ strong > -раскрывается область,
					в кот.слева - текст,
					кот.
							н.перевести,
					а справа - перевод на иностр.язык
							< br /> по нажатию на кнопку < strong > ""Карта"" </ strong > -раскрывается область,
					в кот.появляется карта данной местности
							< br /> по нажатию на кнопку < strong > ""Прогноз погоды"" </ strong > -раскрывается область,
					в кот.появляется прогноз погоды для данной местности"
				},

				new TechTaskItem
				{
					Id = 20,
					TechTaskPageId = 4,
					OrderNumber = 2,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница с возможностью перевода текстов на иностранный язык</strong> с использованием <strong>Google Translate API</strong>"
				},

				new TechTaskItem
				{
					Id = 21,
					TechTaskPageId = 4,
					OrderNumber = 3,
					IsCompleted = false,

					Description =
			@"Д.б. создана <strong>страница с выводимой картой местности</strong>, с использованием <strong>Google Map API</strong>"
				},

				new TechTaskItem
				{
					Id = 22,
					TechTaskPageId = 4,
					OrderNumber = 4,
					IsCompleted = false,

					Description =
			@"М.б. создана <strong>страница с прогнозом погоды для данной местности</strong>, с использованием <strong>Open Weather API</strong>"
				},

				new TechTaskItem
				{
					Id = 23,
					TechTaskPageId = 4,
					OrderNumber = 5,
					IsCompleted = false,

					Description =
			@"М.б. создана <strong>страница с информацией о странах, об их географическом положении, об их достопримечательностях</strong>"
				},

				new TechTaskItem
				{
					Id = 24,
					TechTaskPageId = 4,
					OrderNumber = 6,
					IsCompleted = false,

					Description =
			@"Дополнительно:
							<br />М.б. создана <strong>страница с информацией о странах, об их истории, политическом устройстве, праве, традициях и обычаях местного населения, об объектах сферы гостеприимства (гостинницы, кафе и т.д.)</strong>"
				}

			);

            modelBuilder.Entity<TechTaskItem>().HasData(

				new TechTaskItem
				{
					Id = 25,
					TechTaskPageId = 5,
					OrderNumber = 1,
					IsCompleted = false,

					Description =
			@"Данная страница д. содержать:
							<br/><strong>блок навигационных кнопок:</strong> ""Наши реквизиты"", ""Режим работы"", ""Фотогаллерея"", ""Обратная связь"", ""Edit AboutPage"", ""ТЗ для About""
							< br />< strong > блок рекизитов тур.фирмы </ strong >
							< br />< strong > блок с информацией о режиме работы турфирмы </ strong >
							< br />< strong > блок с фотографиями путешествий по странам мира </ strong > "
				},

				new TechTaskItem
				{
					Id = 26,
					TechTaskPageId = 5,
					OrderNumber = 2,
					IsCompleted = false,

					Description =
			@"Д.б. <strong>страница для создания, редактирования и удаления разных версий страницы About</strong>.
							<br />Д.б. организован <strong>доступ к данной функциональности по ролям ""superadmin"", ""editor""</strong>"
				},

				new TechTaskItem
				{
					Id = 27,
					TechTaskPageId = 5,
					OrderNumber = 3,
					IsCompleted = false,

					Description =
			@"Д.б. <strong>страница с формой обратной связи с клиентами туристической фирмы</strong>
							<br />Д.б. реализован <strong>функционал создания, отправки, сохранения вопросов от клиентов и просмотра  ответов на них от туристической фирмы</strong> (+ для боковой панели)"
				},

				new TechTaskItem
				{
					Id = 28,
					TechTaskPageId = 5,
					OrderNumber = 4,
					IsCompleted = false,

					Description =
			@"Д.б. реализована возможность <strong>звонить из приложения и отправлять сообщения по email</strong> (+ для боковой панели)"
				},

				new TechTaskItem
				{
					Id = 29,
					TechTaskPageId = 5,
					OrderNumber = 5,
					IsCompleted = false,

					Description =
			@"Д.б. реализована возможность <strong>общения с клиентами через социальные сети Facebook, WhatsApp, Telegram, Viber</strong> (+ для боковой панели)"
				},

				new TechTaskItem
				{
					Id = 30,
					TechTaskPageId = 5,
					OrderNumber = 6,
					IsCompleted = false,

					Description =
			@"Дополнительно:
							<br />М.б. реализован <strong>функционал подсчета стоимости поездки</strong> (+ для боковой панели)"
				}

			);

            OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
