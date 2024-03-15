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
        private readonly ProductUtils _ProductUtils;
        
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ProductsController(ProductUtils ProductUtils)
        {
            _ProductUtils = ProductUtils;
        }

        /// <summary>
        /// Метод вывода всех турпродуктов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProducts()
        {
            var products = _ProductUtils.GetProducts();
            _logger.Debug("Получена модель IEnumerable<Product>. ");

            if (products == null)
            {
                _logger.Warn("В БД нет ни одного турпродукта. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного турпродукта."));
            }
            else
            {
                _logger.Debug("Выводятся все турпродукты. ");

                _logger.Trace("Переход по маршруту /Products/GetAllProducts.\n");
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
            _logger.Debug($"Получена модель Product по id = {id}. ");

            if (product.Id == 0)
            {
                _logger.Warn($"В БД нет турпродукта с id = {id}. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет турпродукта с id = {id}.\n"));
            }
            else
            {
                _logger.Debug($"Выводится турпродукт с id = {id}. ");

                _logger.Trace($"Переход по маршруту /Products/GetProduct?id={id}.\n");
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
            _logger.Debug("Создается вью-модель CreateProductViewModel. ");

            _logger.Trace("Переход по маршруту /Products/CreateProduct.cshtml\n");
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
            var product = _ProductUtils.GetProductForEdit(id);
            _logger.Debug($"Получена модель Product по id={id}. ");

            _logger.Trace("Переход по маршруту /Products/EditProduct.\n");
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
            var products = _ProductUtils.QueryResult(isFullName, insertedText);
            _logger.Debug("Получена модель IEnumerable<Product>. ");

            if (products == null)
            {
                _logger.Warn($"По результатам запроса получен пустой список турпродуктов по запросу \"...{insertedText}...\". ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет турпродуктов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.Debug("Получен список турпродуктов по результатам запроса. ");
                _logger.Debug($"Выводятся все турпродукты по запросу. ");

                _logger.Trace("Переход по маршруту /Products/GetQueryResultProducts.\n");
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
            _logger.Debug($"Удален турпродукт по id={id}. ");

            _logger.Trace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
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
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель Product прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        await _ProductUtils.SaveImagePathAsync(changeTitleImagePath);
                    }

                    product = _ProductUtils.SetProductModel(product, formValues, changeTitleImagePath);

                    if (product.CountryId !=0 && product.CityId !=0)
                    {
                        _ProductUtils.SaveProduct(product);
                        _logger.Debug("Турпродукт успешно сохранен в БД. ");

                        _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                        return View("Success", product);
                    }
                    else
                    {
                        _logger.Warn("Модель Product не прошла валидацию. Не задана страна и/или город. ");
                        product = _ProductUtils.SetProductModelByFormValues(product, formValues);

                        _logger.Trace("Возвращено /Products/EditProduct.cshtml\n");
                        return View("EditProduct", product);
                    }
                }
                else
                {
                    _logger.Warn("Модель Product не прошла валидацию. ");
                    product = _ProductUtils.SetProductModelByFormValues(product, formValues);

                    _logger.Trace("Возвращено /Products/EditProduct.cshtml\n");
                    return View("EditProduct", product);
                }
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
