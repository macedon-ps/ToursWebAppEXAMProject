using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.CloudineryImageStorageService;
using ToursWebAppEXAMProject.Services.ImageStorage;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class ProductUtils
    {
        private readonly IBaseInterface<Product> _AllProducts;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly IQueryResultInterface _QueryResult;
        private readonly CloudinaryImageStorageService _CloudinaryImageStorageService;
        public ProductUtils(IBaseInterface<Product> Products, IBaseInterface<Country> Countries, IBaseInterface<City> Cities, IQueryResultInterface QueryResult, CloudinaryImageStorageService CloudinaryImageStorageService) 
        { 
            _AllProducts = Products;
            _AllCountries = Countries;
            _AllCities = Cities;
            _QueryResult = QueryResult;
            _CloudinaryImageStorageService = CloudinaryImageStorageService;
        }


        public IEnumerable<Product> GetProducts()
        {
            return _AllProducts.GetAllItems();
        }


        public Product GetProductById(int id)
        {
            var product = _AllProducts.GetItemById(id);

            return product;
        }


        public CreateProductViewModel GetCreateProductViewModel()
        {
            var productViewModel = new CreateProductViewModel();
            var product = new Product();
            var countries = _AllCountries.GetAllItems();
            var cities = _AllCities.GetAllItems();
            productViewModel.Product = product;
            productViewModel.Countries = countries;
            productViewModel.Cities = cities;

            return productViewModel;
        }


        public IEnumerable<Product> GetProductsQueryResultForEdit(bool isFullName, string insertedText)
        {
            var products = _AllProducts.GetQueryResultItemsAfterFullName(insertedText, isFullName);

            return products;
        }


        /// <summary>
        /// Метод поиска туристических продуктов по запросу во вью-модели SearchProductViewModel. Выборка турпродуктов как результат поиска по фильтрам (Search).
        /// </summary>
        /// <param name="searchViewModel"></param>
        /// <returns></returns>
        public List<Product> GetProductsQueryResultForSearch(int? countryId, int? cityId)
        {
            var products = new List<Product>();

            if (countryId != 0 && cityId != 0)
            {
                products = (List<Product>)_QueryResult.GetProductsByCountryIdAndCityId(countryId, cityId);
            }
            return products;
        }



        public void DeleteProductById(Product product)
        {
            _AllProducts.DeleteItem(product, product.Id);
        }


        public async Task<(string Url, string PublicId)> SaveProductImageByFileNameAsync(IFormFile? imageFileName, int productId)
        {
            var folder = ImageFolder.Products;
            var publicId = $"product_{productId}";
            return await _CloudinaryImageStorageService.UploadAsync(folder, imageFileName, publicId);
        }


        public void SaveProductModel(Product productModel)
        {
            if (productModel != null)
            {
                _AllProducts.SaveItem(productModel, productModel.Id);
            }
        }
    }
}
