namespace ToursWebAppEXAMProject.EnumsDictionaries
{
	public class CitiesListDictionary
	{
		public List<string>? CountriesList { get; set; }

		public List<List<string>>? CitiesList { get; set; }

		public Dictionary<string, List<string>>? CitiesFromSelectList { get; set; }

		public CitiesListDictionary()
		{
			CountriesList = new List<string>(){ "Украина", "Франция","Италия","Испания"};

			CitiesList = new List<List<string>>() 
			{  
				new List<string>(){ "Киев", "Харьков", "Одесса" },
				new List<string>(){"Париж", "Бордо", "Марсель" },
				new List<string>(){"Рим", "Флоренция", "Венеция" },
				new List<string>(){"Мадрид", "Барселона", "Севилья" }
			};

			CitiesFromSelectList = new Dictionary<string, List<string>>()
			{
				{"Украина", new List<string>(){"Киев", "Харьков", "Одесса" }},
				{"Франция", new List<string>(){"Париж", "Бордо", "Марсель" }},
				{"Италия", new List<string>(){"Рим", "Флоренция", "Венеция" }},
				{"Испания", new List<string>(){"Мадрид", "Барселона", "Севилья" }}
			};
		}
	}
}
