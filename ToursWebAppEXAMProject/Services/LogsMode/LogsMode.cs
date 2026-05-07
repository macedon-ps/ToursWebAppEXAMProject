using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.Services.LogsMode
{
    public class LogsMode
    {
        private readonly ILogger<LogsMode> _logger;

        public LogsMode(ILogger<LogsMode> logger)
        {
            _logger = logger;
        }   

        /// <summary>
        /// Метод создания пользовательских сообщений (логов)
        /// </summary>
        /// <param name="message">текстовое сообщение</param>
        /// <param name="LoggerMode">режим создания логов в NLog</param>
        /// <param name="isNLogUsed">создавать ли журнал логов с помощью NLog</param>
        /// <param name="isConsoleUsed">выводить ли сообщения (логи) в консоли</param>
        public void WriteLogs(string? message, NLogsModeEnum? LoggerMode = NLogsModeEnum.Info, bool? isNLogUsed = true, bool? isConsoleUsed = true)
        {
            if (message == null)
            {
                _logger.LogInformation("Не введено сообщение");
                Console.WriteLine("Не введено сообщение");
                return;
            }

            else if (isNLogUsed == false && isConsoleUsed == false)
            {
                _logger.LogWarning("Логгирование с помощью NLog отключено");
                Console.WriteLine("Логгирование в консоли отключено");
                return;
            }

            else if (isNLogUsed == false && isConsoleUsed == true)
            {
                Console.WriteLine(message);
                return;
            }

            // логи NLog и в консоли Console.Write()
            switch (LoggerMode)
            {
                case NLogsModeEnum.Info:
                    _logger.LogInformation(message);
                    break;
                case NLogsModeEnum.Debug:
                    _logger.LogDebug(message);
                    break;
                case NLogsModeEnum.Trace:
                    _logger.LogTrace(message);
                    break;
                case NLogsModeEnum.Warn:
                    _logger.LogWarning(message);
                    break;
                case NLogsModeEnum.Error:
                    _logger.LogError(message);
                    break;
            }
            if (isConsoleUsed == true)
            {
                Console.Write(message);
            }
        }
    }
}
