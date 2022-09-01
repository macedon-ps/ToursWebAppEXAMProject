using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
	public class DataManager
	{
		public IBaseInterface<Product> productBaseInterface { get; set; }
		public IBaseInterface<Country> countryBaseInterface { get; set; }
		public IBaseInterface<City> cityBaseInterface { get; set; }
		public IBaseInterface<Hotel> hotelBaseInterface { get; set; }
		public IBaseInterface<Location> locationBaseInterface { get; set; }
		public IBaseInterface<DateTour> datetourBaseInterface { get; set; }
		public IBaseInterface<Food> foodBaseInterface { get; set; }
		public IBaseInterface<Tour> tourBaseInterface { get; set; }
		public IBaseInterface<Customer> customertBaseInterface { get; set; }
		public IBaseInterface<Saller> sallerBaseInterface { get; set; }
		public IBaseInterface<Ofertum> ofertumBaseInterface { get; set; }
		public IBaseInterface<Blog> blogBaseInterface { get; set; }
		public IBaseInterface<New> newBaseInterface { get; set; }

		public DataManager(	IBaseInterface<Product> productBaseInterface,
							IBaseInterface<Country> countryBaseInterface,
							IBaseInterface<City> cityBaseInterface,
							IBaseInterface<Hotel> hotelBaseInterface,
							IBaseInterface<Location> locationBaseInterface,
							IBaseInterface<DateTour> datetourBaseInterface,
							IBaseInterface<Food> foodBaseInterface,
							IBaseInterface<Tour> tourBaseInterface,
							IBaseInterface<Customer> customertBaseInterface,
							IBaseInterface<Saller> sallerBaseInterface,
							IBaseInterface<Ofertum> ofertumBaseInterface,
							IBaseInterface<Blog> blogBaseInterface,
							IBaseInterface<New> newBaseInterface
							)
		{
			this.productBaseInterface = productBaseInterface;
			this.countryBaseInterface = countryBaseInterface;
			this.cityBaseInterface = cityBaseInterface;
			this.hotelBaseInterface = hotelBaseInterface;
			this.locationBaseInterface = locationBaseInterface;
			this.datetourBaseInterface = datetourBaseInterface;
			this.foodBaseInterface = foodBaseInterface;
			this.tourBaseInterface = tourBaseInterface;
			this.customertBaseInterface = customertBaseInterface;
			this.sallerBaseInterface = sallerBaseInterface;
			this.ofertumBaseInterface = ofertumBaseInterface;
			this.blogBaseInterface = blogBaseInterface;
			this.newBaseInterface = newBaseInterface;
		}
	}
}
