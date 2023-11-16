﻿using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Url страницы, на которую нужно вернуться после авторизации
        /// </summary>
        [Required]
        public string ReturnUrl { get; set; }
    }
}