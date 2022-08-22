using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IFood
	{
		IEnumerable<Food> GetAllFoods();

		Food GetFoodById(int id);

		void SaveFood(Food food);

		void DeleteFood(int id);
	}
}