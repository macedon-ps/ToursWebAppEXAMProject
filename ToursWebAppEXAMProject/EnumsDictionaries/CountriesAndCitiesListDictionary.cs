namespace ToursWebAppEXAMProject.EnumsDictionaries
{
	public class CountriesAndCitiesListDictionary
	{
		public List<string> CountriesList { get; set; } = null!;

		public List<List<string>> CitiesList { get; set; } = null!;

		public Dictionary<string, List<string>> Dictionary { get; set; } = null!;

		public CountriesAndCitiesListDictionary()
		{
			Dictionary = new Dictionary<string, List<string>>()
			{
				{"Украина", new List<string>(){"Киев", "Харьков", "Одесса" }},
				{"Франция", new List<string>(){"Париж", "Бордо", "Марсель" }},
				{"Италия", new List<string>(){"Рим", "Флоренция", "Венеция" }},
				{"Испания", new List<string>(){"Мадрид", "Барселона", "Севилья" }}
			};

			CountriesList = Dictionary.Keys.ToList<string>();
			CitiesList = Dictionary.Values.ToList<List<string>>();
		}

		public string CountryName(string countryNameForValidation)
		{
			var countryName = string.Empty;
			var countriesWithCitiesDictionary = new CountriesAndCitiesListDictionary().Dictionary;
			foreach (var item in countriesWithCitiesDictionary)
			{
				if (item.Key == countryNameForValidation)
				{
					countryName = countryNameForValidation;
					return countryName;
				}
			}
			return countryName;
		}
		
		public List<string> GetAllCountries()
		{
			var countries = new List<string>();
			var countriesWithCitiesDictionary = new CountriesAndCitiesListDictionary().Dictionary;
			foreach(var item in countriesWithCitiesDictionary)
			{
				countries.Add(item.Key);
			}
			return countries;
		}

		public List<string> GetAllCitiesOfCountry (string countryName) 
		{
			var countriesWithCitiesDictionary = new CountriesAndCitiesListDictionary().Dictionary;
			var cities = countriesWithCitiesDictionary[countryName];
			return cities;
		}

		public string CityName(string countryNameSelected)
		{
			var cityName = GetAllCitiesOfCountry(countryNameSelected).First();
			return cityName;
		}
	}
}
