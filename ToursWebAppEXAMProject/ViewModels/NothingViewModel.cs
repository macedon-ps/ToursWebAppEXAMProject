using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class NothingViewModel
    {
        
        /// <summary>
        /// Предупреждение об отсутствии данных
        /// </summary>
        [Display(Name = "Сообщение об отсутствии данных")]
        public string WarnMessage { get; set; }

        /// <summary>
        /// Дата и время предупреждения
        /// </summary>
        [Display(Name = "Дата и время предупреждения")]
        public DateTime DateTimeWarn { get; set; }

        public NothingViewModel(string warnMessage)
        {
            DateTimeWarn = DateTime.Now;
            WarnMessage = warnMessage;
        }
    }
}
