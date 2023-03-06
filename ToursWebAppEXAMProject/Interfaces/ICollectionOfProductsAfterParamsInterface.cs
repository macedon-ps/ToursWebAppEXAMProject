using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICollectionOfProductsAfterParamsInterface
	{
		IEnumerable<Product> GetQueryResultItemsAfterParams(string countryName);
	}
}
