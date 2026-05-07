using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductUtils _ProductUtils;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductUtils ProductUtils, ILogger<ProductsController> logger)
        {
            _ProductUtils = ProductUtils;
            _logger = logger;
        }


        /// <summary>
        /// Метод вывода всех турпродуктов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProducts()
        {
            var products = _ProductUtils.GetProducts();
            _logger.LogDebug("Получена модель IEnumerable<Product>. ");

            if (products == null)
            {
                _logger.LogWarning("В БД нет ни одного турпродукта. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного турпродукта."));
            }
            else
            {
                _logger.LogDebug("Выводятся все турпродукты. ");

                _logger.LogTrace("Переход по маршруту /Products/GetAllProducts.\n");
                return View(products);
            }
        }


        /// <summary>
        /// Метод вывода турпродукта по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор турпродукта</param>
        /// <returns></returns>
        public IActionResult GetProduct(int id)
        {
            var product = _ProductUtils.GetProductById(id);
            _logger.LogDebug($"Получена модель Product по id = {id}. ");

            if (product.Id == 0)
            {
                _logger.LogWarning($"В БД нет турпродукта с id = {id}. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет турпродукта с id = {id}.\n"));
            }
            else
            {
                _logger.LogDebug($"Выводится турпродукт с id = {id}. ");

                _logger.LogTrace($"Переход по маршруту /Products/GetProduct?id={id}.\n");
                return View(product);
            }
        }


        /// <summary>
        /// Метод создания турпродукта
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateProduct()
        {
            var productViewModel = _ProductUtils.GetCreateProductViewModel();
            _logger.LogDebug("Создается вью-модель CreateProductViewModel. ");

            _logger.LogTrace("Переход по маршруту /Products/CreateProduct.cshtml\n");
            return View(productViewModel);

            
        }


        /// <summary>
        /// Метод редактирования турпродукта по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор турпродукта</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _ProductUtils.GetProductById(id);
            _logger.LogDebug($"Получена модель Product по id={id}. ");

            _logger.LogTrace("Переход по маршруту /Products/EditProduct.\n");
            return View(product);
        }


        /// <summary>
        /// Метод вывода результатов выборки турпродуктов по тому, что ищем - полное название или ключевое слово (букву). Выборка турпродуктов для редактирования (Edit).
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetProductsQueryResultForEdit(bool isFullName, string insertedText)
        {
            var products = _ProductUtils.GetProductsQueryResultForEdit(isFullName, insertedText);
            _logger.LogDebug("Получена модель IEnumerable<Product>. ");

            if (products == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список турпродуктов по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет турпродуктов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список турпродуктов по результатам запроса. ");
                _logger.LogDebug($"Выводятся все турпродукты по запросу. ");

                _logger.LogTrace("Переход по маршруту /Products/GetProductsQueryResultForEdit.\n");
                return View(products);
            }
        }


        /// <summary>
        /// Метод вывода результатов выборки турпродуктов по Id страны и города. Выборка турпродуктов в поиске (Search).
        /// </summary>
        /// <param name="countryId">id страны.</param>
        /// <param name="cityId">id города.</param>
        /// <returns>Выборка турпродуктов в поиске (Search).</returns>
        [HttpGet]
        public IActionResult GetProductsQueryResultForSearch(int? countryId, int? cityId)
        {
            var products = _ProductUtils.GetProductsQueryResultForSearch(countryId, cityId);
            _logger.LogDebug("Получена модель IEnumerable<Product>. ");

            if (products == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список турпродуктов по запросу \"/Search/?countryId={countryId}&cityId={cityId}\". ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет турпродуктов по запросу \"/Search/?countryId={countryId}&cityId={cityId}\"."));
            }
            else
            {
                _logger.LogDebug("Получен список турпродуктов по результатам запроса. ");
                _logger.LogDebug($"Выводятся все турпродукты по запросу. ");

                _logger.LogTrace("Переход по маршруту /Products/GetProductsQueryResultForSearch.\n");
                return View(products);
            }
        }


        /// <summary>
        /// Метод удаления отдельного турпродукта по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор турпродукта</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = _ProductUtils.GetProductById(id);
            if (product != null)
            {
                _ProductUtils.DeleteProductById(product);
            }
            _logger.LogDebug($"Удален турпродукт по id={id}. ");

            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", product);
        }


        /// <summary>
        /// Метод сохранения турпродукта с данными, введенными пользователем
        /// </summary>
        /// <param name="productModel">Модель турпродукта</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveProduct(Product productModel, IFormFile? titleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель Product прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        productModel.TitleImagePath = await _ProductUtils.SaveProductImageByFileNameAsync(titleImagePath);
                    }

                    if (productModel.CountryId !=0 && productModel.CityId !=0)
                    {
                        _ProductUtils.SaveProductModel(productModel);
                        _logger.LogDebug("Турпродукт успешно сохранен в БД. ");
                    }

                    productModel.DateAdded = DateTime.Now;

                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", productModel);
                }
                else
                {
                    _logger.LogWarning("Модель Product не прошла валидацию. ");
                   
                    _logger.LogTrace("Возвращено /Products/EditProduct.cshtml\n");
                    return View("EditProduct", productModel);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели Product.");
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
