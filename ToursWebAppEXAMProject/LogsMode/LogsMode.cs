using NLog;
using ToursWebAppEXAMProject.EnumsDictionaries;

namespace ToursWebAppEXAMProject.LogsMode
{
    public class LogsMode
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LogsMode() { }

        public static void WriteLogs(string? message, NLogsModeEnum? LoggerMode = NLogsModeEnum.Info, bool? ConsoleMode = true)
        {
            if (message == null) 
            {
                logger.Info("Не введено сообщение");
                Console.WriteLine("Не введено сообщение");
                return;
            }

            else if (LoggerMode == 0 && ConsoleMode == false) 
            {
                logger.Warn("Не выбран режим отображения логов");
                Console.WriteLine("Не выбран режим отображения логов");
                return; 
            }
            
            else if (LoggerMode == 0 && ConsoleMode == true)
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
            if(ConsoleMode == true)
            {
                Console.Write(message);
            }
        }
    }
}
