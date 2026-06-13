using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class PhotoGalleryImage
    {
        public int Id { get; set; }

        /// <summary>
        /// Адрес расположения изображения в облачном хранилище Cloudinary.
        /// </summary>
        [Required]
        [StringLength(500)]
        public string ImagePath { get; set; }


        /// <summary>
        /// Id страницы About
        /// </summary>
        public int AboutPageVersionId { get; set; }


        /// <summary>
        /// PublicId изображения в облачном хранилище Cloudinary.
        /// </summary>
        [StringLength(500)]
        public string PublicId { get; set; }


        public virtual AboutPageVersion AboutPageVersion { get; set; }
    }
}
