using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SearchController: Controller
	{
		private readonly SearchUtils _SearchUtils;
        private readonly ILogger<SearchController> _logger;

        public SearchController(SearchUtils searchUtils, ILogger<SearchController> logger)
		{
			_SearchUtils = searchUtils;
            _logger = logger;
        }

        /// <summary>
        /// Метод вывода стартовой страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int? countryId, int? cityId, string? map)
        {
            var searchViewModel = _SearchUtils.GetModel(countryId, cityId, map);
            _logger.LogDebug("Получена вью-модель SearchProductViewModel по дефолту. ");

            _logger.LogTrace("Переход по маршруту /Search/Index.\n");
            return View(searchViewModel);
        }

        /// <summary>
        /// POST версия метода вывода страницы Search с данными поиска, введенными пользователем
        /// </summary>
        /// <param name="viewModel">Данные вью-модели</param>
		/// <returns></returns>
        [HttpPost]
		public IActionResult Index(SearchProductViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель прошла валидацию");

                    var countryId = viewModel.CountryIdSelected;
                    var cityId = viewModel.CityIdSelected;

                    if (countryId.HasValue && cityId.HasValue)
                    {
                        _logger.LogTrace($"Redirect: countryId={countryId}, cityId={cityId}");

                        return RedirectToAction(
                            "GetProductsQueryResultForSearch",
                            "Products",
                            new { countryId = countryId.Value, cityId = cityId.Value }
                        );
                    }

                    _logger.LogWarning("Не выбрана страна или город");
                    return View(viewModel);
                }

                _logger.LogWarning("ModelState невалиден");
                return View(viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при получении вью-модели SearchProductViewModel .");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
            }
        }
    }
}
