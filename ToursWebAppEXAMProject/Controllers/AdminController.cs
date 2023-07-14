using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AdminController : Controller
	{
		private readonly DataManager DataManager;

		private readonly IWebHostEnvironment hostingEnvironment;

		public AdminController(DataManager DataManager, IWebHostEnvironment hostingEnvironment)
		{
			this.DataManager = DataManager;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /Admin/Index.\n", NLogsModeEnum.Trace);
			
			return View();
		}

		[HttpGet]
		public IActionResult EditMenu(string type)
		{
            WriteLogs("Переход по маршруту /Admin/EditMenu.\n", NLogsModeEnum.Trace);
            
			var model = new EditMenuViewModel(true, "", type);

			return View(model);
		}

		[HttpGet]
		public IActionResult GetQueryResultEntities(bool isFullName, string fullNameOrKeywordOfItem, string type)
		{
            WriteLogs("Переход по маршруту /Admin/GetQueryResultItemsAfterFullName. ", NLogsModeEnum.Trace);
            
			// реализовано switch(type) для выборки items по типам (New, Blog, Product)
			object items = new object();

			switch (type)
			{
				case "New":
					items = DataManager.NewBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
					break;
				case "Blog":
					items = DataManager.BlogBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
					break;
				case "Product":
					items = DataManager.ProductBaseInterface.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
					break;
			}

			if (items == null)
			{
                WriteLogs("Нет сущностей (новостей, блогов или турпродуктов). Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);
                
				var errorInfo = new ModelsErrorViewModel(typeof(List<New>));
				return View("ModelsError", errorInfo);
			}

            WriteLogs("Выводятся все сущности (новости, блоги или турпродукты).\n", NLogsModeEnum.Debug);
           
			return View(items);
		}

		[HttpGet]
		public IActionResult EditItem(string type, int id)
		{
            WriteLogs("Переход по маршруту /Admin/EditItem. ", NLogsModeEnum.Trace);
            
			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					var modelNew =	DataManager.NewBaseInterface.GetItemById(id);
                   
					WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemNew", modelNew);
				
				case "ToursWebAppEXAMProject.Models.Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);

                    WriteLogs("Возвращено /Admin/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemBlog", modelBlog);

				case "ToursWebAppEXAMProject.Models.Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);

                    WriteLogs("Возвращено /Admin/EditItemProduct.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemProduct", modelProduct);
			}

            WriteLogs("Возвращено представление /Admin/Index.cshtml\n", NLogsModeEnum.Trace);
            						
			return View("Index");
		}

		[HttpGet]
		public IActionResult DeleteItem(string type, int id)
		{
			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					var modelNew = DataManager.NewBaseInterface.GetItemById(id);
					DataManager.NewBaseInterface.DeleteItem(modelNew, id);

                    WriteLogs("Возвращено /Admin/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelNew);
				
				case "ToursWebAppEXAMProject.Models.Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);
					DataManager.BlogBaseInterface.DeleteItem(modelBlog, id);

                    WriteLogs("Возвращено /Admin/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelBlog);
				
				case "ToursWebAppEXAMProject.Models.Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);
					DataManager.ProductBaseInterface.DeleteItem(modelProduct, id);

                    WriteLogs("Возвращено /Admin/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("SuccessForDelete", modelProduct);
			}

            WriteLogs("Возвращено /Admin/Index.cshtml\n", NLogsModeEnum.Trace);
            
			return View("Index");
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemNew(New model, IFormCollection formValues, IFormFile titleImagePath)
       	{
            WriteLogs("Запущен процесс сохранения новости в БД. ", NLogsModeEnum.Debug);
            			
			if (ModelState.IsValid)
			{
                WriteLogs("New прошла валидацию. ", NLogsModeEnum.Debug);
                
				if (titleImagePath != null)
				{
					var filePath = $"/images/NewsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Титульная картинка новости сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }
					
					model.FullDescription = formValues["fullInfoAboutNew"];
					model.TitleImagePath = filePath;
					model.DateAdded = DateTime.Now;
										
					DataManager.NewBaseInterface.SaveItem(model, model.Id);

                    WriteLogs($"Новость с id = {model.Id} сохранена в БД. ", NLogsModeEnum.Debug);
                    WriteLogs("Возвращено /Admin/Success.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("Success", model);
				}
			}

            WriteLogs("New не прошла валидацию. ", NLogsModeEnum.Warn);
            						
			if(titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutNew"];

                WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);
                
				return View("EditItemNew", model);
			}

            WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);

            return View("EditItemNew", model);
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemBlog(Blog model, IFormCollection formValues, IFormFile titleImagePath)
		{
			WriteLogs("Запущен процесс сохранения блога в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("Blog прошла валидацию. ", NLogsModeEnum.Debug);
                
				if (titleImagePath != null)
				{
					var filePath = $"/images/BlogsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Титульная картинка блога сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }

					model.FullDescription = formValues["fullInfoAboutBlog"];
					model.TitleImagePath = filePath;
					model.DateAdded = DateTime.Now;

					DataManager.BlogBaseInterface.SaveItem(model, model.Id);

                    WriteLogs("Блог успешно сохранен в БД. ", NLogsModeEnum.Debug);
                    WriteLogs("Возвращено /Admin/Success.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("Success", model);
				}
			}

            WriteLogs("Модель Blog не прошла валидацию. ", NLogsModeEnum.Warn);
            
			if (titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutBlog"];

                WriteLogs("Возвращено /Admin/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);
                
				return View("EditItemBlog", model);
			}

            WriteLogs("Возвращено /Admin/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);

            return View("EditItemBlog", model);
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemProduct(Product model, IFormCollection formValues, IFormFile titleImagePath)
		{

            WriteLogs("Сохранение турпродукта в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("Product прошла валидацию. ", NLogsModeEnum.Debug);
                
				if (titleImagePath != null)
				{
					var filePath = $"/images/ProductsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);
                       
						WriteLogs($"Титульная картинка турпродукта сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }

					model.FullDescription = formValues["fullInfoAboutProduct"];
					model.TitleImagePath = filePath;
					model.DateAdded = DateTime.Now;

					DataManager.ProductBaseInterface.SaveItem(model, model.Id);
                    
					WriteLogs("Турпродукт сохранен в БД. ", NLogsModeEnum.Debug);
                    
                    // TODO: запрос на создание/редактирование/удаление страны и/или города, если да, то вывод нового вью для создания/редактирования/удаления страны и/или города, сохранение изменений в БД

                    WriteLogs("Возвращено /Admin/Success.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("Success", model);
				}
			}

            WriteLogs("Product не прошла валидацию. ", NLogsModeEnum.Warn);
           
			if (titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutProduct"];

                WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);
                
				return View("EditItemProduct", model);
			}

            WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);

            return View("EditItemProduct", model);
		}

		[HttpGet]
		public IActionResult Success(Object model)
		{

            WriteLogs("Переход по маршруту /Admin/Success.\n", NLogsModeEnum.Trace);
            
			return View(model);
		}

		public IActionResult CreateEntity(string type) 
		{
			var model = new Object();

			switch (type)
			{
				case "New":
					model = new New();

                    WriteLogs("Возвращено /Admin/EditItemNew.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemNew", model);

				case "Blog":
					model = new Blog();

                    WriteLogs("Возвращено /Admin/EditItemBlog.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemBlog", model);

				case "Product":
					model = new Product();

                    WriteLogs("Возвращено /Admin/EditItemProduct.cshtml\n", NLogsModeEnum.Trace);
                    
					return View("EditItemProduct", model);
			}
			return RedirectToAction("Index");
		}

		public IActionResult TechTaskAdmin()
		{
            WriteLogs("Переход по маршруту Admin/TechTaskAdmin.\n", NLogsModeEnum.Trace);
            
			var pageName = "Admin";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskAdmin(TechTaskViewModel model)
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
                WriteLogs("Возвращено /Admin/TechTaskAdmin.cshtml\n", NLogsModeEnum.Trace);
                				
				return View(model);
			}

            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Admin/TechTaskAdmin.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}
