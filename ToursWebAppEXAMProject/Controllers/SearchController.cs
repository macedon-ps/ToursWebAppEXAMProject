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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SearchController(SearchUtils searchUtils)
		{
			_SearchUtils = searchUtils;
        }

        /// <summary>
        /// Метод вывода стартовой страницы Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int? countryId, int? cityId, string? map)
        {
            var searchViewModel = _SearchUtils.GetModel(countryId, cityId, map);
            _logger.Debug("Получена вью-модель SearchProductViewModel по дефолту. ");

            _logger.Trace("Переход по маршруту /Search/Index.\n");
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
    }
}
