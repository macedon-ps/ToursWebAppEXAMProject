using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public partial class SearchController: Controller
	{
		private readonly DataManager DataManager;

		public SearchController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

        /// <summary>
        /// Метод вывода стартовой страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string countryName = "Украина")
		{
            // дефолтное значение countryName для стартовой страницы - "Украина"
            var searchViewModel = GetModel(countryName);

			WriteLogs("Переход по маршруту /Search/Index. ", NLogsModeEnum.Trace);
            WriteLogs("Выведено меню поиска турпродуктов.\n", NLogsModeEnum.Debug);

            return View(searchViewModel);
		}

        /// <summary>
        /// POST версия метода вывода страницы Search с данными поиска, введенными пользователем
        /// </summary>
        /// <param name="viewModel">Данные вью-модели</param>
		/// /// <param name="formValues">Данные формы ввода</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult Index(SearchProductViewModel viewModel, IFormCollection formValues)
		{
			var searchViewModel = GetModel(viewModel, formValues);
			
			if (ModelState.IsValid)
			{
				WriteLogs("SearchProductViewModel прошла валидацию.\n", NLogsModeEnum.Debug);
				
                // TODO: Организовать вывод результатов поиска туристических продуктов

                // WriteLogs("Выведены результаты поиска турпродуктов. Переход по маршруту /Search/Index. \n", NLogsModeEnum.Trace);
                
				// успешный вывод проверочных данных, кот. формируются на клиенте в браузере
                // return View("success", searchViewModel);

				// вывод результатов запроса пользователя
				var products = new List<Product>();

				var countryName = searchViewModel.CountryNameSelected;
				var cityName = searchViewModel.CityNameSelected;

				if(countryName!="" && cityName != "")
				{
                    products = (List<Product>)DataManager.QueryResultInterface.GetProductsByCountryNameAndCityName(countryName, cityName);
                }
				
				return View("GetAllProducts", products);


			}
            WriteLogs("SearchProductViewModel не прошла валидацию. ", NLogsModeEnum.Warn);
            WriteLogs("Переход по маршруту /Search/Index.\n", NLogsModeEnum.Trace);
            
			return View();
		}


		/// <summary>
		/// Метод вывода турпродукта по его id
		/// </summary>
		/// <param name="id">уникальный идентификатор турпродукта</param>
		/// <returns></returns>
		public IActionResult GetProduct(int id)
		{
            WriteLogs($"Переход по маршруту /Search/GetProduct?id={id}. ", NLogsModeEnum.Trace);
            
			var product = DataManager.ProductBaseInterface.GetItemById(id);

			if (product.Id == 0)
			{
                WriteLogs($"Нет турпродукта с id={id}. Возвращено /Error.cshtml\n", NLogsModeEnum.Warn);
                
				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(Product), id);
				return View("Error", errorInfo);
			}

            WriteLogs($"Выводится турпродукт с id={id}.\n", NLogsModeEnum.Debug);
            
			return View(product);
		}

		/// <summary>
		/// Метод вывода всех туристических продуктов
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllProducts()
		{
            WriteLogs("Переход по маршруту /Search/GetAllProducts. ", NLogsModeEnum.Trace);
            
			var products = DataManager.ProductBaseInterface.GetAllItems();

			if (products == null)
			{
                WriteLogs("Нет турпродуктов. Возвращено /Error.cshtml\n", NLogsModeEnum.Warn);
                
				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}

            WriteLogs("Выводятся все турпродукты\n", NLogsModeEnum.Debug);
            
			return View(products);
		}

        /// <summary>
        /// Метод вывода турпродуктов по полному названию или по ключевому слову (букве)
        /// </summary>
        /// <param name="keyword">текст для поиска</param>
		/// /// <param name="isFullName">полное название - true или ключевое слово (буква) - false</param>
        /// <returns></returns>
        public IActionResult GetQueryResultProducts(string keyword, bool isFullName)
		{
            WriteLogs("Переход по маршруту /Search/GetAllProducts. ", NLogsModeEnum.Trace);
            
			var products = DataManager.ProductBaseInterface.GetQueryResultItemsAfterFullName(keyword, isFullName);

			if (products == null)
			{
                WriteLogs("Нет турпродуктов по запросу. Возвращено /Error.cshtml\n", NLogsModeEnum.Warn);
                
				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}

            WriteLogs("Выводятся все турпродукты по запросу\n", NLogsModeEnum.Debug);
            
			return View(products);
		}

		public IActionResult GetQueryResultProductsByCountryAndCityName(string countryName, string cityName)
		{
			var products = DataManager.QueryResultInterface.GetProductsByCountryNameAndCityName(countryName, cityName);

			return View("GetAllProducts", products);
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult TechTaskSearch()
		{
            WriteLogs("Переход по маршруту Search/TechTaskSearch.\n", NLogsModeEnum.Trace);
           
			var pageName = "Search";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Search
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult TechTaskSearch(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию. ", NLogsModeEnum.Debug);
                
				double TechTasksCount = 6;
				double TechTasksTrueCount = 0;
				if (model.IsExecuteTechTask1 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask2 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask3 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask4 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask5 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask6 == true) TechTasksTrueCount++;

				double ExecuteTechTasksProgress = Math.Round((TechTasksTrueCount / TechTasksCount) * 100);
				model.ExecuteTechTasksProgress = ExecuteTechTasksProgress;

				DataManager.TechTaskInterface.SaveProgressTechTasks(model);

                WriteLogs("Показатели выполнения ТЗ сохранены. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Search/TechTaskSearch.cshtml\n", NLogsModeEnum.Trace);
                
				return View(model);
			}
            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Search/TechTaskSearch.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}
