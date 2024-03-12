using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IBaseInterface<Product> _AllProducts;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly FileUtils _FileUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ProductsController(IBaseInterface<Product> Products, IBaseInterface<Country> Countries, IBaseInterface<City> Cities, FileUtils FileUtils)
        {
            _AllProducts = Products;
            _AllCountries = Countries;
            _AllCities = Cities;
            _FileUtils = FileUtils;
        }

        /// <summary>
        /// Метод вывода всех турпродуктов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProducts()
        {
            _logger.Trace("Переход по маршруту /Products/GetAllProducts. ");

            var products = _AllProducts.GetAllItems();

            if (products == null)
            {
                var errorMessage = "В БД нет ни одного турпродукта";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug("Выводятся все турпродукты\n");

            return View(products);
        }

        /// <summary>
        /// Метод вывода турпродукта по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор турпродукта</param>
        /// <returns></returns>
        public IActionResult GetProduct(int id)
        {
            _logger.Trace($"Переход по маршруту /Products/GetProduct?id={id}. ");

            var product = _AllProducts.GetItemById(id);

            if (product.Id == 0)
            {
                var errorMessage = $"В БД нет турпродукта с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug($"Выводится турпродукт с id = {id}.\n");

            return View(product);
        }

        /// <summary>
        /// Метод создания турпродукта
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateProduct()
        {
            var productViewModel = new CreateProductViewModel();
            var product = new Product();
            var countries = _AllCountries.GetAllItems();
            var cities = _AllCities.GetAllItems();
            productViewModel.Product = product;
            productViewModel.Countries = countries;
            productViewModel.Cities = cities;
           
            _logger.Trace("Возвращено /Products/CreateProduct.cshtml\n");

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
            _logger.Trace("Переход по маршруту /Products/EditProduct. ");

            var product = _AllProducts.GetItemById(id);
            product.DateAdded = DateTime.Now;

            return View(product);
        }

        /// <summary>
        /// Метод вывода результатов выборки турпродуктов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultProducts(bool isFullName, string insertedText)
        {
            _logger.Trace("Переход по маршруту /Products/GetQueryResultProducts. ");

            var products = _AllProducts.GetQueryResultItemsAfterFullName(insertedText, isFullName);
            var numberProducts = products.Count();

            if (numberProducts == 0)
            {
                var message = $"Нет турпродуктов по запросу \"{insertedText}\". Возвращено ../Edit/Nothing.cshtml\n";

                _logger.Warn(message);

                var nothingInfo = new ErrorViewModel(message);
                return View("Nothing", nothingInfo);
            }

            _logger.Debug($"Выводятся все турпродукты по запросу \"{insertedText}\".\n");

            return View(products);
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
            var product = _AllProducts.GetItemById(id);
            _AllProducts.DeleteItem(product, id);

            _logger.Trace("Возвращено ../Shared/SuccessForDelete.cshtml\n");

            return View("SuccessForDelete", product);
        }

        /// <summary>
        /// Метод сохранения турпродукта с данными, введенными пользователем
        /// </summary>
        /// <param name="product">Модель турпродукта</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveProduct(Product product, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            // TODO: продумать способ создания страны и города с их id до создания продукта с его CountryId и CityId
            // отобразить UI создания страны и города
            
            _logger.Debug("Запущен процесс сохранения турпродукта в БД. ");

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.Debug("Модель Product прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        var folder = "/images/ProductsTitleImages/";
                        await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
                        product.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                    }

                    product.CountryId = Int32.Parse(formValues["CountryId"]);
                    product.CityId = Int32.Parse(formValues["CityId"]);
                    product.FullDescription = formValues["fullInfoAboutProduct"];
                    product.DateAdded = DateTime.Now;

                    _AllProducts.SaveItem(product, product.Id);

                    _logger.Debug("Турпродукт успешно сохранен в БД. ");
                    _logger.Trace("Возвращено ../Shared/Success.cshtml\n");

                    return View("Success", product);
                }
                catch (Exception error)
                {
                    return View("Error", error.Message);
                }
            }
            else
            {
                _logger.Warn("Модель Product не прошла валидацию. ");
                _logger.Trace("Возвращено /Products/EditProduct.cshtml\n");

                product.FullDescription = formValues["fullInfoAboutProduct"];

                return View("EditProduct", product);
            }
        }
    }
}
