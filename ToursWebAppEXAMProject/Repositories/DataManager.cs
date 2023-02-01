using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
	public class DataManager
	{
		public IBaseInterface<Product> ProductBaseInterface { get; set; }
		public IBaseInterface<Country> CountryBaseInterface { get; set; }
		public IBaseInterface<City> CityBaseInterface { get; set; }
		public IBaseInterface<Hotel> HotelBaseInterface { get; set; }
		public IBaseInterface<Location> LocationBaseInterface { get; set; }
		public IBaseInterface<DateTour> DatetourBaseInterface { get; set; }
		public IBaseInterface<Food> FoodBaseInterface { get; set; }
		public IBaseInterface<Tour> TourBaseInterface { get; set; }
		public IBaseInterface<Customer> CustomertBaseInterface { get; set; }
		public IBaseInterface<Saller> SallerBaseInterface { get; set; }
		public IBaseInterface<Offer> OfferBaseInterface { get; set; }
		public IBaseInterface<Blog> BlogBaseInterface { get; set; }
		public IBaseInterface<New> NewBaseInterface { get; set; }

		public IEditTechTaskInterface TechTaskInterface { get; set; }

		public ICollectionOfCitiesAfterParams CollectionOfCitiesAfterParamsInterface { get; set; }


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
							IBaseInterface<Offer> offerBaseInterface,
							IBaseInterface<Blog> blogBaseInterface,
							IBaseInterface<New> newBaseInterface,
							IEditTechTaskInterface techTaskInterface,
							ICollectionOfCitiesAfterParams collectionOfCitiesAfterParamsInterface
							)
		{
			this.ProductBaseInterface = productBaseInterface;
			this.CountryBaseInterface = countryBaseInterface;
			this.CityBaseInterface = cityBaseInterface;
			this.HotelBaseInterface = hotelBaseInterface;
			this.LocationBaseInterface = locationBaseInterface;
			this.DatetourBaseInterface = datetourBaseInterface;
			this.FoodBaseInterface = foodBaseInterface;
			this.TourBaseInterface = tourBaseInterface;
			this.CustomertBaseInterface = customertBaseInterface;
			this.SallerBaseInterface = sallerBaseInterface;
			this.OfferBaseInterface = offerBaseInterface;
			this.BlogBaseInterface = blogBaseInterface;
			this.NewBaseInterface = newBaseInterface;
			this.TechTaskInterface = techTaskInterface;
			this.CollectionOfCitiesAfterParamsInterface = collectionOfCitiesAfterParamsInterface;
		}
	}
}
