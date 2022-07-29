using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services;

namespace ToursWebAppEXAMProject.Controllers
{
	public class ProductsRepository : IProduct
	{
		public List<Product>? Products { get; set; }
		public bool IsConnected { get; set; } = false;

		#region Заглушка: список объектов турпродуктов. Используется до создания БД для примера
		/*
		private readonly List<TourProduct> _tourProducts = new ()
		{
			new TourProduct(){Id = 1, Name = "Жемчужина Нила", Description = "Египет принято называть «даром Нила», поскольку без реки этой плодородной и густонаселённой земли, не говоря уже о великой цивилизации, возникшей пять тысяч лет назад, не существовало бы вовсе. На своеобразие и историю страны во многом повлиял разительный контраст между изобилием нильской Долины и Дельты и скудостью окружающей пустыни. Для древних египтян именно здесь находилась родина – Кемет («Чёрная земля»), богатая плодородным Нилом, где природа и люди процветали под сенью милостивых богов, – в противоположность пустыне, воплощению смерти и хаоса, находившейся под властью Сета, бога ветров и стихийных бедствий."},
			new TourProduct(){Id = 2, Name = "Пустыня Европы", Description = "Олешковские пески – это удивительное место в Херсонской области, в Украине, которое напоминает собой настоящую пустыню. За это его прозвали украинской Сахарой. В действительности здесь не только пустыня. Есть также участки с травянистой растительностью, сосновыми борами, камышевыми зарослями. А окружают парки густые искусственные леса. Уникальный природный объект находится недалеко от побережья Черного моря в южной части материковой Украины. Общая площадь национального парка составляет 1600 км2. Территория растянулась на 150 км с юга на север и на 30 км с запада на восток."},
			new TourProduct(){Id = 3, Name = "Край тысячи озер", Description = "Страна озер, невероятной северной природы и край мастеров - Финляндия. На территории Финляндии сохранились старинные поселения финнов, карелов, поморов, вепсов. Здесь вы познакомитесь с уникальной архитектурой, насладитесь притягательными видами и проникнитесь культурой коренных народов. Финляндия хранит в себе многовековую историю. Раскрыть ее помогут старинные деревянные строения, каньоны и водные прогулки по озерным шхерам добавят ярких штрихов вашему отдыху."},
			new TourProduct(){Id = 4, Name = "Между Европой и Азией", Description = "Стамбул – крупнейший город, но, вопреки расхожему мнению, не столица Турции. Как и все мегаполисы, расположенные у большой воды, он невероятно фотогеничен. В том числе, благодаря уникальному расположению между Европой и Азией – город и символически, и географически поделен на две части, и у каждой – свое лицо. Стамбул (Istanbul) – крупнейший город Турции и один из самых больших городов мира, морской порт, крупный промышленно-торговый и культурный центр Турции. Бывшая столица Римской, Византийской, Латинской и Османской империй. Расположен на берегах пролива Босфор. Основная часть города находится в Европе, меньшая – в Азии. Европейская часть, в свою очередь, делится на две части бухтой Золотой Рог. Азиатская и европейская части разделены проливом Босфор, через который построен вантовый мост. В Стамбуле и ближайших пригородах проживает более 13 млн. человек. "}
		};
		*/
		#endregion

		// создаем подключение к БД
		private readonly SqlConnection _connection;

		public ProductsRepository()
		{
			_connection = new SqlConnection(ConfigData.ConnectionString);
			_connection.Open();
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
		}

		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public IEnumerable<Product> GetAllTourProducts()
		{
			// запрос к БД
			var commandText = "select * from Product";

			// выполнение SQL запроса
			var command = new SqlCommand(commandText, _connection);
			logger.Debug("Отправлен запрос к БД по выборке всех туров");
			Console.WriteLine("Отправлен запрос к БД по выборке всех туров");

			Products = new List<Product>();

			using var reader = command.ExecuteReader();
			while (reader.Read())
			{
				var tour = new Product
				{
					Id = reader.GetInt32(0),
					Name = reader.GetString(1),
					Description = reader.GetString(2),
				};
				Products.Add(tour);
			}

			logger.Debug("Произведена выборка всех туристических продуктов");
			Console.WriteLine("Произведена выборка всех туристических продуктов");

			foreach (var tour in Products)
			{
				Console.WriteLine($"Id = {tour.Id}");
				Console.WriteLine($"Name = {tour.Name}");
				Console.WriteLine($"Desc = {tour.Description}");
				Console.WriteLine();
			}

			_connection.Close();
			logger.Debug("Произведено отключение от базы данных");
			Console.WriteLine("Произведено отключение от базы данных");

			return Products;
		}

		public Product GetTourProduct(int id)
		{
			logger.Trace($"Запрашивваемый id туристического продукта: {id}");

			Console.WriteLine($"Запрашивваемый id туристического продукта: {id}");


			var tourProduct = Products?.FirstOrDefault(x => x.Id == id);
			if (tourProduct == null)
			{
				logger.Error($"Не найден туристический тур по id = {id}");

				Console.WriteLine($"Не найден туристический тур по id = {id}");

				return tourProduct = new Product();
			}
			else
			{
				IsConnected = true;
			}

			logger.Trace($"Попытка подключения к источнику данных: {IsConnected}");
			logger.Trace($"Подключения прошло {(IsConnected == true ? "успешно" : "неуспешно")}");

			Console.WriteLine($"Попытка подключения к источнику данных:");
			Console.WriteLine($"Подключения прошло {(IsConnected == true ? "успешно" : "неуспешно")}");

			return tourProduct;
		}
	}
}
