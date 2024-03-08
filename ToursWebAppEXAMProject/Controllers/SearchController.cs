using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;
using TourWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SearchController: Controller
	{
		private readonly IQueryResultInterface _QueryResult;
		private readonly SearchUtils _SearchUtils;
		private readonly TechTaskUtils _TechTaskUtils;

		public SearchController(IQueryResultInterface QueryResult, SearchUtils SearchUtils, TechTaskUtils TechTaskUtils)
		{
			_QueryResult = QueryResult;
			_SearchUtils = SearchUtils;
			_TechTaskUtils = TechTaskUtils;
		}

        /// <summary>
        /// Метод вывода стартовой страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string countryName = "Украина")
		{
			// дефолтное значение countryName для стартовой страницы - "Украина"
			var searchViewModel = _SearchUtils.GetModel(countryName);

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
           	if (ModelState.IsValid)
			{
                var searchViewModel = _SearchUtils.GetModel(viewModel, formValues);

                WriteLogs("SearchProductViewModel прошла валидацию.\n", NLogsModeEnum.Debug);
				WriteLogs("Выведены результаты поиска турпродуктов. Переход по маршруту /Search/Index. \n", NLogsModeEnum.Trace);
				
				var countryName = searchViewModel.CountryNameSelected;
				var cityName = searchViewModel.CityNameSelected;
								
				if(countryName!=null && cityName!=null)
				{
                    var products = _SearchUtils.GetProductsQueryResult(countryName, cityName);
					if (products != null)
					{
                        return View("../Products/GetAllProducts", products);
                    }
                }
   			}
            WriteLogs("SearchProductViewModel не прошла валидацию. ", NLogsModeEnum.Warn);
            WriteLogs("Переход по маршруту /Search/Index.\n", NLogsModeEnum.Trace);
            
			return View(viewModel);
		}
		       
		/// <summary>
		/// Метод вывода турпродуктов по названию страны и города
		/// </summary>
		/// <param name="countryName"></param>
		/// <param name="cityName"></param>
		/// <returns></returns>
        public IActionResult GetQueryResultProductsByCountryAndCityName(string countryName, string cityName)
        {
            var products = _QueryResult.GetProductsByCountryNameAndCityName(countryName, cityName);

            return View("GetAllProducts", products);
        }

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Search
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpGet]
		public IActionResult TechTaskSearch()
		{
            WriteLogs("Переход по маршруту Search/TechTaskSearch.\n", NLogsModeEnum.Trace);
           
			var model = _TechTaskUtils.GetTechTaskForPage("Search");

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Search
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskSearch(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию. ", NLogsModeEnum.Debug);

				_TechTaskUtils.SetTechTaskProgressAndSave(model);

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
