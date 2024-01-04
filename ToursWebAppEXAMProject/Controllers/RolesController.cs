using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthN_AuthZ.ConfirmEmail.Controllers
{
    //[Authorize(Roles = "superadmin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Метод вывода списка всех ролей с возможностью их создания, редактирования, удаления
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        /// <summary>
        /// Метод создания новой роли, Get метод
        /// </summary>
        /// <returns></returns>
        public IActionResult Create() => View();

        /// <summary>
        /// Метод создания новой роли, Post метод
        /// </summary>
        /// <param name="name">название роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        /// <summary>
        /// Метод удаления роли
        /// </summary>
        /// <param name="id">идентификатор роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод вывода списка всех пользователей с возможностью устанавливать им роли
        /// </summary>
        /// <returns></returns>
        public IActionResult UserList() => View(_userManager.Users.ToList());

        /// <summary>
        /// Метод установки ролей для пользователей, Get метод
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        /// <summary>
        /// Метод установки ролей для пользователей, Post метод
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="roles">список ролей</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}

