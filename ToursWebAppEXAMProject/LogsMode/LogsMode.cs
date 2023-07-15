using NLog;
using ToursWebAppEXAMProject.EnumsDictionaries;

namespace ToursWebAppEXAMProject.LogsMode
{
    public class LogsMode
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LogsMode() { }

        /// <summary>
        /// Метод создания пользовательских сообщений (логов)
        /// </summary>
        /// <param name="message">текстовое сообщение</param>
        /// <param name="LoggerMode">режим создания логов в NLog</param>
        /// <param name="isNLogUsed">создавать ли журнал логов с помощью NLog</param>
        /// <param name="isConsoleUsed">выводить ли сообщения (логи) в консоли</param>
        public static void WriteLogs(string? message, NLogsModeEnum? LoggerMode = NLogsModeEnum.Info, bool? isNLogUsed = true, bool? isConsoleUsed = true)
        {
            if (message == null) 
            {
                logger.Info("Не введено сообщение");
                Console.WriteLine("Не введено сообщение");
                return;
            }

            else if (isNLogUsed == false && isConsoleUsed == false) 
            {
                logger.Warn("Логгирование с помощью NLog отключено");
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
                    logger.Info(message);
                    break;
                case NLogsModeEnum.Debug:
                    logger.Debug(message); 
                    break;
                case NLogsModeEnum.Trace: 
                    logger.Trace(message); 
                    break; 
                case NLogsModeEnum.Warn:
                    logger.Warn(message); 
                    break;
                case NLogsModeEnum.Error:
                    logger.Error(message);      
                    break;
            }
            if(isConsoleUsed == true)
            {
                Console.Write(message);
            }
        }
    }
}
