using Microsoft.AspNetCore.Identity;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace TourWebAppEXAMProject.Utils
{
    public class Feedback
    {
        public static User GetUser(CorrespondenceViewModel model, UserManager<User> userManager)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Email == model.Email && u.EmailConfirmed == true);
            
            if (user != null)
            {
                return user;
            }
            else
            {
                return new User();
            }
        }

        public static Asker GetAsker(CorrespondenceViewModel model, IBaseInterface<Asker> allAskers)
        {
            var asker = allAskers.GetAllItems().
                FirstOrDefault(a => (a.Name == model.Name) && (a.Surname == model.Surname) && (a.Email == model.Email));

            if (asker != null)
            {
                return asker;
            }
            else
            {
                return new Asker();
            }
        }

        public static bool IsRegisterUser(CorrespondenceViewModel model, UserManager<User> userManager)
        {
            var result = false;
            
            // 1. проверяем, является ли зарегистрированным пользователем
            var user = userManager.Users.FirstOrDefault(u => u.Email == model.Email && u.EmailConfirmed == true);
            
            if (user != null)
            {
                result = true;
            }
            
            return result;
        } 
        
        public static bool isCustomer(CorrespondenceViewModel model, IBaseInterface<Customer> allCustomers)
        {
            var result = false;

            // 2. проверяем, являлся ли пользователем услуг компании в прошлом
            var customer = allCustomers.GetAllItems()
                .FirstOrDefault(x => (x.Name == model.Name) && (x.Surname == model.Surname) && (x.Email == model.Email));
            
            if (customer != null) result = true;
           
            return result;
        }
        
        public static void AddToRole(User user, string role, UserManager<User> userManager)
        {
            if(user.Id != "") 
            {
                userManager.AddToRoleAsync(user, role);
            }
            
            return;
        }
    }
}
