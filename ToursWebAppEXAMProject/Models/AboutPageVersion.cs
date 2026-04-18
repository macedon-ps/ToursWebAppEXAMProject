using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class AboutPageVersion
    {
        public int Id { get; set; }

        [Display(Name = "Ключевое слово")]
        [StringLength(50, ErrorMessage = "Ключевое слово не должно содержать более 50 символов")]
        public string? Keyword { get; set; } 


        [Display(Name = "Актуальная версия страницы")]
        public bool IsActual { get; set; } = false;


        [Required(ErrorMessage = "Введите общий заголовок страницы")]
        [Display(Name = "Общий заголовок страницы")]
        [StringLength(200, ErrorMessage = "Общий заголовок страницы не должен содержать более 200 символов")]
        public string MainTitle { get; set; } 


        [Required(ErrorMessage = "Введите заголовок о компании")]
        [Display(Name = "Заголовок о компании")]
        [StringLength(200, ErrorMessage = "Заголовок о компании не должен содержать более 200 символов")]
        public string AboutTitle { get; set; } 


        [Required(ErrorMessage = "Введите заголовок о реквизитах компании")]
        [Display(Name = "Заголовок о реквизитах компании")]
        [StringLength(200, ErrorMessage = "Заголовок о реквизитах компании не должен содержать более 200 символов")]
        public string DetailsTitle { get; set; } 


        [Required(ErrorMessage = "Введите заголовок о режиме работы")]
        [Display(Name = "Заголовок о режиме работы")]
        [StringLength(200, ErrorMessage = "Заголовок о режиме работы не должен содержать более 200 символов")]
        public string OperationModeTitle { get; set; } 


        [Required(ErrorMessage = "Введите заголовок о фотогалерее")]
        [Display(Name = "Заголовок о фотогалерее")]
        [StringLength(200, ErrorMessage = "Заголовок о фотогалерее не должен содержать более 200 символов")]
        public string PhotoGalleryTitle { get; set; } 

        [Required(ErrorMessage = "Введите заголовок об обратной связи")]
        [Display(Name = "Заголовок об обратной связи")]
        [StringLength(200, ErrorMessage = "Заголовок об обратной связи не должен содержать более 200 символов")]
        public string FeedbackTitle { get; set; } 


        [Display(Name = "Краткое описание общее")]
        [StringLength(200, ErrorMessage = "Краткое описание общее не должно содержать более 200 символов")]
        public string? MainShortDescription { get; set; } 

        [Display(Name = "Краткое описание компании")]
        [StringLength(200, ErrorMessage = "Краткое описание компании не должно содержать более 200 символов")]
        public string? AboutShortDescription { get; set; } 


        [Display(Name = "Краткое описание реквизитов")]
        [StringLength(200, ErrorMessage = "Краткое описание реквизитов не должно содержать более 200 символов")]
        public string? DetailsShortDescription { get; set; } 


        [Display(Name = "Краткое описание режима работы")]
        [StringLength(200, ErrorMessage = "Краткое описание режима работы не должно содержать более 200 символов")]
        public string? OperationModeShortDescription { get; set; } 


        [Display(Name = "Краткое описание фотогалереи")]
        [StringLength(200, ErrorMessage = "Краткое описание фотогалереи не должно содержать более 200 символов")]
        public string? PhotoGalleryShortDescription { get; set; } 


        [Display(Name = "Краткое описание обратной связи")]
        [StringLength(200, ErrorMessage = "Краткое описание обратной связи не должно содержать более 200 символов")]
        public string? FeedbackShortDescription { get; set; } 

        [Display(Name = "Полное описание общее")]
        public string? MainFullDescription { get; set; } 

        [Display(Name = "Полное описание компании")]
        public string? AboutFullDescription { get; set; } 


        [Display(Name = "Полное описание реквизитов")]
        public string? DetailsFullDescription { get; set; } 


        [Display(Name = "Полное описание режима работы")]
        public string? OperationModeFullDescription { get; set; }


        [Display(Name = "Полное описание фотогалереи")]
        public string? PhotoGalleryFullDescription { get; set; } 


        [Display(Name = "Полное описание обратной связи")]
        public string? FeedbackFullDescription { get; set; } 


        [Display(Name = "Путь к фотографиям общее")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям общее не должно содержать более 100 символов")]
        public string? MainImagePath { get; set; }


        [Display(Name = "Путь к фотографиям о компании")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям о компании не должно содержать более 100 символов")]
        public string? AboutImagePath { get; set; }


        [Display(Name = "Путь к фотографиям реквизитов")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям реквизитов не должно содержать более 100 символов")]
        public string? DetailsImagePath { get; set; }


        [Display(Name = "Путь к фотографиям режима работы")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям режима работы не должно содержать более 100 символов")]
        public string? OperationModeImagePath { get; set; }


        [Display(Name = "Путь к фотографиям фотогалереии")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям фотогалереи не должно содержать более 100 символов")]
        public string? PhotoGalleryImagePath { get; set; }


        [Display(Name = "Путь к фотографиям обратной связи")]
        [StringLength(100, ErrorMessage = "Путь к фотографиям обратной связи не должно содержать более 100 символов")]
        public string? FeedbackImagePath { get; set; }


        public DateTime? DateAdded { get; set; }


        public List<PhotoGalleryImage> CollectionImages { get; set; } = new List<PhotoGalleryImage>();
    }
}
