using ToursWebAppEXAMProject.Interfaces;

namespace ToursWebAppEXAMProject.Repositories
{
	public class DataManager
	{
		public IProduct productsRepository { get; set; }
		public ICity citiesRepository { get; set; }
		public ICountry countriesRepository { get; set; }
		public IHotel hotelsRepository { get; set; }
		public ILocation locationsRepository { get; set; }
		public IDateTour dateToursRepository { get; set; }
		public IFood foodsRepository { get; set; }
		public ITour toursRepository { get; set; }
		public ICustomer customersRepository { get; set; }
		public ISaller sallersRepository { get; set; }
		public IOfertum ofertumsRepository { get; set; }
		public INew newsRepository	{ get; set; }
		public IArticle articlesRepository { get; set; }

		public DataManager(	IProduct productsRepository, 
							ICity citiesRepository, 
							ICountry countriesRepository,
							IHotel hotelsRepository,
							ILocation locationsRepository,
							IDateTour dateToursRepository,
							IFood foodsRepository,
							ITour toursRepository,
							ICustomer customersRepository,
							ISaller sallersRepository,
							IOfertum ofertumsRepository,
							INew newsRepository,
							IArticle articlesRepository)
		{
			this.productsRepository = productsRepository;
			this.citiesRepository = citiesRepository;
			this.countriesRepository = countriesRepository;
			this.hotelsRepository = hotelsRepository;
			this.locationsRepository = locationsRepository;
			this.dateToursRepository = dateToursRepository;
			this.foodsRepository = foodsRepository;
			this.toursRepository = toursRepository;
			this.customersRepository = customersRepository;
			this.sallersRepository = sallersRepository;
			this.ofertumsRepository = ofertumsRepository;
			this.newsRepository = newsRepository;
			this.articlesRepository = articlesRepository;
		}
	}
}
