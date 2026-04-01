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

        public SearchController(SearchUtils searchUtils, TechTaskUtils techTaskUtils)
		{
			_SearchUtils = searchUtils;
            _TechTaskUtils = techTaskUtils;
        }

        /// <summary>
        /// Метод вывода стартовой страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int? countryId, int? cityId)
        {
            var searchViewModel = _SearchUtils.GetModel(countryId, cityId);
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
		public IActionResult Index(SearchProductViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель прошла валидацию");

                    var countryId = viewModel.CountryIdSelected;
                    var cityId = viewModel.CityIdSelected;

                    if (countryId.HasValue && cityId.HasValue)
                    {
                        _logger.Trace($"Redirect: countryId={countryId}, cityId={cityId}");

                        return RedirectToAction(
                            "GetProductsQueryResultForSearch",
                            "Products",
                            new { countryId = countryId.Value, cityId = cityId.Value }
                        );
                    }

                    _logger.Warn("Не выбрана страна или город");
                    return View(viewModel);
                }

                _logger.Warn("ModelState невалиден");
                return View(viewModel);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
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
