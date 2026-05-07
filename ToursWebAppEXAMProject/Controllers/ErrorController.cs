using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{

    [Route("Error")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IActionResult Error()
        {
            // Получаем информацию об исключении, если она есть
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            // Логируем ошибку, если она существует
            if (exception != null)
            {
                _logger.LogError(exception, "Глобальная ошибка: {Message}", exception.Message);
            }

            // Создаем модель представления для отображения информации об ошибке
            var errorInfo = new ErrorViewModel() 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorInfo);
        }
    }
}
