using Microsoft.AspNetCore.Identity;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class FeedbackUtils
    {
        private readonly UserManager<User> _UserManager;
        private readonly IBaseInterface<Asker> _AllAskers;
        private readonly IBaseInterface<Customer> _AllCustomers;
        private readonly IBaseInterface<Correspondence> _AllCorrespondences;

        public FeedbackUtils(UserManager<User> UserManager, IBaseInterface<Asker> AllAskers, IBaseInterface<Customer> AllCustomers, IBaseInterface<Correspondence> AllCorrespondences)
        {
            _UserManager = UserManager;
            _AllAskers = AllAskers;
            _AllCustomers = AllCustomers;
            _AllCorrespondences = AllCorrespondences;
        }

        public User? GetUser(CorrespondenceViewModel model)
        {
            var user = _UserManager.Users.FirstOrDefault(u => u.Email == model.Email);
            
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public Asker? GetAsker(CorrespondenceViewModel model)
        {
            var asker = _AllAskers.GetAllItems().
                FirstOrDefault(a => (a.Name == model.Name) && (a.Surname == model.Surname) && (a.Email == model.Email));

            if (asker != null)
            {
                return asker;
            }
            return null;
        }

        public bool IsRegisterUser(CorrespondenceViewModel model)
        {
            var result = false;
            
            // 1. проверяем, является ли зарегистрированным пользователем
            var user = _UserManager.Users.FirstOrDefault(u => u.Email == model.Email && u.EmailConfirmed == true);
            
            if (user != null)
            {
                result = true;
            }
            
            return result;
        } 
        
        public bool IsCustomer(CorrespondenceViewModel model)
        {
            var result = false;

            // 2. проверяем, являлся ли пользователем услуг компании в прошлом
            var customer = _AllCustomers.GetAllItems()
                .FirstOrDefault(x => (x.Name == model.Name) && (x.Surname == model.Surname) && (x.Email == model.Email));
            
            if (customer != null) result = true;
           
            return result;
        }
        
        public void AddToRole(User user, string role)
        {
            if(user.Id != "") 
            {
                _UserManager.AddToRoleAsync(user, role);
            }
            
            return;
        }

        public Asker CreateAsker(CorrespondenceViewModel model)
        {
            var asker = new Asker(model.Name, model.Surname, model.Email, model.Gender, model.BirthDay);

            var isCustomer = IsCustomer(model);
            if (isCustomer)
            {
                asker.IsCustomer = true;
            }
            return asker;
        }

        public void SaveAsker(Asker asker)
        {
            _AllAskers.SaveItem(asker, asker.Id);
        }

        public Task<IList<string>> GetAllRolesForUser(User user)
        {
            var roles = _UserManager.GetRolesAsync(user);
            return roles;
        }
        public Correspondence CreateCorrespondence(string question, DateTime? questionDate, int id, bool isCustomer)
        {
            var correspondence = new Correspondence(question, questionDate, id, isCustomer);
            return correspondence;
        }

        public void SaveCorrespondence(Correspondence correspondence)
        {
            _AllCorrespondences.SaveItem(correspondence, correspondence.Id);
        }
    }
}
