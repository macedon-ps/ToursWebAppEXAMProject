namespace ToursWebAppEXAMProject.Repositories
{
	public class DataManager
	{
		public ProductsRepository productsRepository;
		public CitiesRepository citiesRepository;
		public CountriesRepository countriesRepository;
		public HotelsRepository hotelsRepository;
		public LocationsRepository locationsRepository;
		public DateToursRepository dateToursRepository;
		public FoodsRepository foodsRepository;
		public ToursRepository toursRepository;
		public CustomersRepository customersRepository;
		public SallersRepository sallersRepository;
		public OfertumsRepository ofertumsRepository;
		public NewsRepository newsRepository;
		public ArticlesRepository articlesRepository;  

		public DataManager(	ProductsRepository productsRepository, 
							CitiesRepository citiesRepository, 
							CountriesRepository countriesRepository,
							HotelsRepository hotelsRepository,
							LocationsRepository locationsRepository,
							DateToursRepository dateToursRepository,
							FoodsRepository foodsRepository,
							ToursRepository toursRepository,
							CustomersRepository customersRepository,
							SallersRepository sallersRepository,
							OfertumsRepository ofertumsRepository,
							NewsRepository newsRepository,
							ArticlesRepository articlesRepository)
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
