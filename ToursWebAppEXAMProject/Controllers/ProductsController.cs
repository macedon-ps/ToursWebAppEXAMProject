using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IBaseInterface<Product> _AllProducts;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductsController(IBaseInterface<Product> Products, IWebHostEnvironment hostingEnvironment)
        {
            this._AllProducts = Products;
            this._hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Метод вывода всех турпродуктов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProducts()
        {
            WriteLogs("Переход по маршруту /Products/GetAllProducts. ", NLogsModeEnum.Trace);

            var products = _AllProducts.GetAllItems();

            if (products == null)
            {
                var errorMessage = "В БД нет ни одного турпродукта";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs("Выводятся все турпродукты\n", NLogsModeEnum.Debug);

            return View(products);
        }

        /// <summary>
        /// Метод вывода турпродукта по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор турпродукта</param>
        /// <returns></returns>
        public IActionResult GetProduct(int id)
        {
            WriteLogs($"Переход по маршруту /Products/GetProduct?id={id}. ", NLogsModeEnum.Trace);

            var product = _AllProducts.GetItemById(id);

            if (product.Id == 0)
            {
                var errorMessage = $"В БД нет турпродукта с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs($"Выводится турпродукт с id = {id}.\n", NLogsModeEnum.Debug);

            return View(product);
        }

        /// <summary>
        /// Метод создания турпродукта
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateProduct()
        {
            WriteLogs("Выполняется действие /Products/CreateProduct. ", NLogsModeEnum.Trace);

            var product = new Product();

            WriteLogs("Возвращено /Products/EditProduct.cshtml\n", NLogsModeEnum.Trace);

            return View("EditProduct", product);
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
            WriteLogs("Переход по маршруту /Products/EditProduct. ", NLogsModeEnum.Trace);

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
        public IActionResult GetQueryResultProducts(bool isFullName, string fullNameOrKeywordOfItem)
        {
            WriteLogs("Переход по маршруту /Products/GetQueryResultProducts. ", NLogsModeEnum.Trace);

            var products = _AllProducts.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberProducts = products.Count();

            if (numberProducts == 0)
            {
                var message = $"Нет турпродуктов по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Edit/Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);

                var nothingInfo = new ErrorViewModel(message);
                return View("../Edit/Nothing", nothingInfo);
            }

            WriteLogs($"Выводятся все турпродукты по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);

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

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", product);
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
            
            WriteLogs("Запущен процесс сохранения турпродукта в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                try
                {
                    WriteLogs("Модель Product прошла валидацию. ", NLogsModeEnum.Debug);

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        var filePath = $"/images/ProductsTitleImages/{changeTitleImagePath.FileName}";

                        using (var fstream = new FileStream(_hostingEnvironment.WebRootPath + filePath, FileMode.Create))
                        {
                            await changeTitleImagePath.CopyToAsync(fstream);

                            WriteLogs($"Новая титульная картинка турпродукта сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                        }
                        product.TitleImagePath = filePath;
                    }

                    product.FullDescription = formValues["fullInfoAboutProduct"];
                    product.DateAdded = DateTime.Now;

                    _AllProducts.SaveItem(product, product.Id);

                    WriteLogs("Турпродукт успешно сохранен в БД. ", NLogsModeEnum.Debug);
                    WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                    return View("../Shared/Success", product);
                }
                catch (Exception error)
                {
                    return View("../Shared/Error", error.Message);
                }
            }
            else
            {
                WriteLogs("Модель Product не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Products/EditProduct.cshtml\n", NLogsModeEnum.Trace);

                product.FullDescription = formValues["fullInfoAboutProduct"];

                return View("EditProduct", product);
            }
        }
    }
}
