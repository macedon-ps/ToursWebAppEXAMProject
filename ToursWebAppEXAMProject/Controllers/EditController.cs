using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    [Authorize]
    public class EditController : Controller
	{
		private readonly DataManager DataManager;

		private readonly IWebHostEnvironment hostingEnvironment;

		public EditController(DataManager DataManager, IWebHostEnvironment hostingEnvironment)
		{
			this.DataManager = DataManager;
			this.hostingEnvironment = hostingEnvironment;
		}

     
        /// <summary>
        /// Метод вывода стартовой страницы Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string type = "New")
		{
			WriteLogs("Переход по маршруту /Edit/Index.\n", NLogsModeEnum.Trace);

            // страница Index.cshtml по умолчанию принимает тип New, по нажатию на кнопки - др.типы (New, Blog, Product)
            var model = new EditMenuViewModel(false, "", type);

			return View(model);
		}

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод вывода результатов выборки по тексту для поиска, по тому, что ищем - полное название или ключевое слово (букву), пр типу данных (новости, блоги или турпродукты)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <param name="type">тип данных - новость, блог или турпродукт</param>
        /// <returns></returns>
        [HttpGet]
		public IActionResult GetQueryResultEntities(bool isFullName, string fullNameOrKeywordOfItem, string type)
		{
            WriteLogs("Переход по маршруту /Edit/GetQueryResultItemsAfterFullName. ", NLogsModeEnum.Trace);

			// реализовано switch(type) для выборки items по типам (New, Blog, Product)
			var items = new Object();
			var numberItems = 0;
			var keyWord = new string[2];

			switch (type)
			{
				case "New":
                    var news = (List<New>)DataManager.NewBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
                    numberItems = news.Count;
					keyWord[0] = "новостей"; keyWord[1] = "новости";
					items = news;
					break;
				case "Blog":
					var blogs = (List<Blog>)DataManager.BlogBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
					numberItems = blogs.Count;
                    keyWord[0] = "блогов"; keyWord[1] = "блоги";
					items = blogs;
					break;
				case "Product":
					var products = (List<Product>)DataManager.ProductBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
                    numberItems = products.Count;	
					keyWord[0] = "турподуктов"; keyWord[1] = "турпродукты";
					items = products;
					break;
			}
			if (numberItems == 0)
			{
				var message = $"Нет {keyWord[0]} по запросу \"{fullNameOrKeywordOfItem}\". Возвращено /Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);
                
				var nothingInfo = new ErrorViewModel(message);
				return View("Nothing", nothingInfo);
			}

            WriteLogs($"Выводятся все {keyWord[1]} по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);
           
			return View(items);
		}

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод редактирования отдельной сущности (новости, блога или турпродукта) по ее id
        /// </summary>
        /// <param name="type">тип данных (новость, блог или турпродукт)</param>
        /// <param name="id">универсальный идентификатор сущности (новости, блога или турпродукта)</param>
        /// <returns></returns>
        [HttpGet]
		public IActionResult EditItem(string type, int id)
		{
            WriteLogs("Переход по маршруту /Edit/EditItem. ", NLogsModeEnum.Trace);
            
			var model = new object();
			var view = "";

			switch (type)
			{
				case "New":
					var modelNew =	DataManager.NewBaseInterface.GetItemById(id);
					modelNew.DateAdded  = DateTime.Now;

                    model = modelNew;
					view = "EditItemNew";
					break;
                    												
				case "Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);
					modelBlog.DateAdded = DateTime.Now;

                    model = modelBlog;
                    view = "EditItemBlog";
                    break;

                case "Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);
					modelProduct.DateAdded = DateTime.Now;
					
                    model = modelProduct;
                    view = "EditItemProduct";
                    break;
			}

            WriteLogs($"Возвращено представление /Edit/{view}.cshtml\n", NLogsModeEnum.Trace);
            						
			return View(view, model);
		}

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод удаления отдельной сущности (новости, блога или турпродукта) по ее id
        /// </summary>
        /// <param name="type">тип данных (новость, блог или турпродукт)</param>
        /// <param name="id">универсальный идентификатор сущности (новости, блога или турпродукта)</param>
        /// <returns></returns>
        [HttpGet]
		public IActionResult DeleteItem(string type, int id)
		{
			switch (type)
			{
				case "New":
					var modelNew = DataManager.NewBaseInterface.GetItemById(id);
					DataManager.NewBaseInterface.DeleteItem(modelNew, id);

                    WriteLogs("Возвращено /Edit/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelNew);
				
				case "Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);
					DataManager.BlogBaseInterface.DeleteItem(modelBlog, id);

                    WriteLogs("Возвращено /Edit/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelBlog);
				
				case "Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);
					DataManager.ProductBaseInterface.DeleteItem(modelProduct, id);

                    WriteLogs("Возвращено /Edit/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelProduct);
			}

            WriteLogs("Возвращено /Edit/Index.cshtml\n", NLogsModeEnum.Trace);
            
			return View("Index");
		}

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод сохранения новости с данными, введенными пользователем
        /// </summary>
        /// <param name="model">Модель новости</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> SaveItemNew(New model, IFormCollection formValues, IFormFile? changeTitleImagePath)
       	{
            WriteLogs("Запущен процесс сохранения новости в БД. ", NLogsModeEnum.Debug);
            			
			if (ModelState.IsValid)
			{
                WriteLogs("Модель New прошла валидацию. ", NLogsModeEnum.Debug);

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var filePath = $"/images/NewsTitleImages/{changeTitleImagePath.FileName}";

                    using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await changeTitleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Новая титульная картинка новости сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }
                    model.TitleImagePath = filePath;
                }

                model.FullDescription = formValues["fullInfoAboutNew"];
                model.DateAdded = DateTime.Now;

                DataManager.NewBaseInterface.SaveItem(model, model.Id);

                WriteLogs("Новость успешно сохранена в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Edit/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("Success", model);
            }
            else
            {
                WriteLogs("Модель New не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Edit/EditItemNew.cshtml\n", NLogsModeEnum.Trace);

                model.FullDescription = formValues["fullInfoAboutNew"];

                return View("EditItemNew", model);
            }
        }

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод сохранения блога с данными, введенными пользователем
        /// </summary>
        /// <param name="model">Модель блога</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> SaveItemBlog(Blog model, IFormCollection formValues, IFormFile? changeTitleImagePath)
		{
			WriteLogs("Запущен процесс сохранения блога в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("Модель Blog прошла валидацию. ", NLogsModeEnum.Debug);
                
				// если мы хотим поменять картинку
				if (changeTitleImagePath != null)
				{
					var filePath = $"/images/BlogsTitleImages/{changeTitleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await changeTitleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Новая титульная картинка блога сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }
                    model.TitleImagePath = filePath;
                }
					
				model.FullDescription = formValues["fullInfoAboutBlog"];
				model.FullMessageLine = formValues["fullMessageLine"];
				model.DateAdded = DateTime.Now;

				DataManager.BlogBaseInterface.SaveItem(model, model.Id);

                WriteLogs("Блог успешно сохранен в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Edit/Success.cshtml\n", NLogsModeEnum.Trace);
                    
				return View("SuccessBlog", model);
				
			}
			else
			{
                WriteLogs("Модель Blog не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Edit/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);

                model.FullDescription = formValues["fullInfoAboutBlog"];

                return View("EditItemBlog", model);
            }
        }

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод сохранения турпродукта с данными, введенными пользователем
        /// </summary>
        /// <param name="model">Модель турпродукта</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> SaveItemProduct(Product model, IFormCollection formValues, IFormFile? changeTitleImagePath)
		{

            WriteLogs("Запущен процесс сохранения блога в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Модель Product прошла валидацию. ", NLogsModeEnum.Debug);

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var filePath = $"/images/ProductsTitleImages/{changeTitleImagePath.FileName}";

                    using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await changeTitleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Новая титульная картинка турпродукта сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }
                    model.TitleImagePath = filePath;
                }

                model.FullDescription = formValues["fullInfoAboutProduct"];
                model.DateAdded = DateTime.Now;

                DataManager.ProductBaseInterface.SaveItem(model, model.Id);

                // TODO: запрос на создание/редактирование/удаление страны и/или города, если да, то вывод нового вью для создания/редактирования/удаления страны и/или города, сохранение изменений в БД

                WriteLogs("Турпродукт успешно сохранен в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Edit/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("Success", model);

            }
            else
            {
                WriteLogs("Модель Product не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Edit/EditItemProduct.cshtml\n", NLogsModeEnum.Trace);

                model.FullDescription = formValues["fullInfoAboutProduct"];

                return View("EditItemProduct", model);
            }
		}

		/// <summary>
		/// Метод вывода страницы с полным описанием введенных пользователем и успешно сохраненых данных
		/// </summary>
		/// <param name="model">Модель сущности (новости, блога или турпродукта)</param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Success(Object model)
		{

            WriteLogs("Переход по маршруту /Edit/Success.\n", NLogsModeEnum.Trace);
            
			return View(model);
		}

        [Authorize(Roles = "superadmin,editor")]
        /// <summary>
        /// Метод создания сущности (новости, блога или турпродукта)
        /// </summary>
        /// <param name="type">Тип данных (новость, блог или турпродукт)</param>
        /// <returns></returns>
        public IActionResult CreateEntity(string type) 
		{
			var model = new Object();

			switch (type)
			{
				case "New":
					model = new New();

                    WriteLogs("Возвращено /Edit/EditItemNew.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemNew", model);

				case "Blog":
					model = new Blog();

                    WriteLogs("Возвращено /Edit/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemBlog", model);

				case "Product":
					model = new Product();

                    WriteLogs("Возвращено /Edit/EditItemProduct.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemProduct", model);
			}
			return RedirectToAction("Index");
		}

        [Authorize(Roles = "superadmin,admin")]
        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Edit
        /// </summary>
        /// <returns></returns>
        public IActionResult TechTaskEdit()
		{
            WriteLogs("Переход по маршруту Edit/TechTaskEdit.\n", NLogsModeEnum.Trace);
            
			var pageName = "Edit";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

        [Authorize(Roles = "superadmin,admin")]
        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Edit
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult TechTaskEdit(TechTaskViewModel model)
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
                WriteLogs("Возвращено /Edit/TechTaskEdit.cshtml\n", NLogsModeEnum.Trace);
                				
				return View(model);
			}

            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Edit/TechTaskEdit.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}
