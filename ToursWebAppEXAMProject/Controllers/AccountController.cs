using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using TourWebAppEXAMProject.Services.Email;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод регистрации нового пользователя, Get метод
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Метод регистрации нового пользователя, Post метод
        /// </summary>
        /// <param name="model">вьюмодель регистрации нового пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // создание токена  с подтверждением по email

            if (ModelState.IsValid)
            {
                // добавляем пользователя
                User user = new User { Email = model.Email, UserName = model.Email, BirthYear = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);

                // если пользователь создан
                if (result.Succeeded)
                {
                    // генерация токена для пользователя и адреса для колбека
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = token },
                        protocol: HttpContext.Request.Scheme);

                    // отправка сообщения на эл.почту для подтверждения регистрации
                    EmailService emailService = new EmailService();
                     await emailService.SendEmailAsync(model.Email, $"Confirm the registration of a new user {user.UserName}", $"Подтвердите регистрацию нового пользователя {user.UserName} в приложении, перейдя по ссылке: <a href='{callbackUrl}'>Confirm a new user's registration for {user.UserName}</a>");
                    var message = $"Для завершения регистрации нового пользователя приложения {user.UserName}, проверьте электронную почту {user.Email} и перейдите по ссылке, указанной в письме";

                    return View("Message", message);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Метод подтверждения регистрации нового пользователя через email
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="code">токен</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                
                return View("Error", new ErrorViewModel("Ваш Email не прошел подтверждение, т.к. пользователь не зарегистрирован или не имеет подтверждающего токена регистрации"));
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error", new ErrorViewModel("Ваш Email не прошел подтверждение, т.к. пользователь не зарегистрирован"));
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //TODO: создать вью успешного результата подтверждения регистрации нового пользователя с логином - email по ссылке в письме на его электроннe. почтe
                //TODO: продумать механизм получения роли ("editor", "admin" или "superadmin")
                return RedirectToAction("Index", "Home");  // тут м.б. страница успешного потверждения регистрации нового пользователя
            }
            else
            {
                return View("Error", new ErrorViewModel("Ваш Email не прошел подтверждение"));
            }
        }

        /// <summary>
        /// Метод ввода данных для аутентификации и авторизации пользователя, Get метод
        /// </summary>
        /// <param name="returnUrl">обратный адрес, т.е. адрес, с которого пользовател зашел</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Метод ввода данных для аутентификации и авторизации пользователя, Post метод
        /// </summary>
        /// <param name="model">вью модель для данных аутентификации</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }
                }
               
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Метод выхода из состояния авторизации
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            // удаляем токен
            await _signInManager.SignOutAsync();
            return View();
        }

        /// <summary>
        /// Метод обработки отказа в доступе
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Метод предложения пройти подтверждение email
        /// </summary>
        /// <returns></returns>
        public IActionResult NotConfirmedEmail()
        {
            return View();
        }
    }
}
