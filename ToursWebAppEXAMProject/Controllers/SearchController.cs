using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;
using NLog;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SearchController: Controller
	{
		private readonly SearchUtils _SearchUtils;
		private readonly TechTaskUtils _TechTaskUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SearchController(SearchUtils SearchUtils, TechTaskUtils TechTaskUtils)
		{
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
            _logger.Debug("Получена вью-модель SearchProductViewModel по дефолту. ");
            
            _logger.Trace("Переход по маршруту /Search/Index.\n");
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
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель SearchProductViewModel прошла валидацию. ");

                    var searchViewModel = _SearchUtils.GetModel(viewModel, formValues);
                    _logger.Debug("Вью-модель SearchProductViewModel заполнена данными из формы. ");

                    var countryName = searchViewModel.CountryNameSelected;
                    var cityName = searchViewModel.CityNameSelected;

                    if (countryName != null && cityName != null)
                    {

                        var products = _SearchUtils.GetProductsQueryResult(countryName, cityName);
                        if (products != null)
                        {
                            _logger.Debug("Получен список турпродуктов по результатам запроса. ");

                            _logger.Trace("Переход по маршруту ../Products/GetAllProducts.\n");
                            return View("../Products/GetAllProducts", products);
                        }
                        
                        _logger.Warn("По результатам запроса получен пустой список турпродуктов.\n");
                        _logger.Trace("Переход по маршруту /Search/Index.\n");
                        return View(viewModel);
                    }
                    _logger.Warn($"Введенные название страны {countryName} и/или название города {cityName} отсутствуют в БД");
                    _logger.Trace("Переход по маршруту /Search/Index.\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель SearchProductViewModel не прошла валидацию. ");

                    _logger.Trace("Переход по маршруту /Search/Index.\n");
                    return View(viewModel);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
		}
		       
		/// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Search
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpGet]
		public IActionResult TechTaskSearch()
		{
            var model = _TechTaskUtils.GetTechTaskForPage("Search");
            _logger.Debug("Получена вью-модель TechTaskViewModel. ");

            _logger.Trace("Переход по маршруту Search/TechTaskSearch.\n");
            return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Search
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskSearch(TechTaskViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TechTaskViewModel прошла валидацию. ");

                    _TechTaskUtils.SetTechTaskProgressAndSave(viewModel);
                    _logger.Debug("Вью-модель TechTaskViewModel заполнена данными и сохранена. ");

                    _logger.Trace("Возвращено /Search/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }

                _logger.Warn("Вью-модель TechTaskViewModel не прошла валидацию. Данные модели не сохранены. ");

                _logger.Trace("Возвращено /Search/TechTaskHome.cshtml\n");
                return View(viewModel);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
 		}
    }
}
