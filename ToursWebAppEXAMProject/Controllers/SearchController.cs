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
		/// Метод Index(), кот. выводит дефолтный вид страницы Search
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Index()
		{
			// задаем дефолтные значения для стартовой страницы
			var searchViewModel = GetModel("Украина");

			WriteLogs("Переход по маршруту /Search/Index. ", NLogsModeEnum.Trace);
            WriteLogs("Выведено меню поиска турпродуктов.\n", NLogsModeEnum.Debug);

            return View(searchViewModel);
		}

		/// <summary>
		/// POST версия метода Index(SearchProductViewModel searchProductViewModel), кот. принимает введенную модель
		/// </summary>
		/// <param name="searchProductViewModel"></param>
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
                
                return View("success", searchViewModel);
			}
            WriteLogs("SearchProductViewModel не прошла валидацию. ", NLogsModeEnum.Warn);
            WriteLogs("Переход по маршруту /Search/Index.\n", NLogsModeEnum.Trace);
            
			return View();
		}


		/// <summary>
		/// Метод GetProduct(), кот. возвращает данные турпродукта по его id
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
		/// Метод GetAllProducts(), кот. возвращает данные всех туристических продуктов из БД
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
		/// Метод GetQueryResultProducts(), кот. возвращает данные некоторых туристических продуктов из БД по имени / ключевому слову
		/// </summary>
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

		/// <summary>
		/// Метод TechTaskSearch() для отображения данных о выполнении ТЗ на странице Search
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
		/// Метод TechTaskSearch(TechTaskViewModel model) для редактирования и сохранения данных о выполнении ТЗ на странице Search
		/// </summary>
		/// <param name="model">Сохраняемая вью-модель</param>
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
