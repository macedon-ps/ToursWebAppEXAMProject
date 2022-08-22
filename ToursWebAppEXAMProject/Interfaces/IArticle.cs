using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IArticle
	{
		IEnumerable<Article> GetAllArticles();

		Article GetById(int id);

		void SaveArticle(Article article);

		void DeleteArticle(int id);
	}
}