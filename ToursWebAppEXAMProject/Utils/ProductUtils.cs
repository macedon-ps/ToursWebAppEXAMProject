using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class ProductUtils
    {
        private readonly IBaseInterface<Product> _AllProducts;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly FileUtils _FileUtils;
        public ProductUtils(IBaseInterface<Product> Products, IBaseInterface<Country> Countries, IBaseInterface<City> Cities, FileUtils FileUtils) 
        { 
            _AllProducts = Products;
            _AllCountries = Countries;
            _AllCities = Cities;
            _FileUtils = FileUtils;
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

        public Product GetProductForEdit(int id)
        {
            var product = _AllProducts.GetItemById(id);
            product.DateAdded = DateTime.Now;

            return product;
        }

        public IEnumerable<Product> QueryResult(bool isFullName, string insertedText)
        {
            var products = _AllProducts.GetQueryResultItemsAfterFullName(insertedText, isFullName);

            return products;
        }

        public void DeleteProductById(Product product)
        {
            _AllProducts.DeleteItem(product, product.Id);
        }

        public async Task SaveImagePathAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/ProductsTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public Product SetProductModel(Product product, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            var fullInfoProduct = formValues["fullInfoAboutProduct"].ToString();
            var countryIdInfo = formValues["CountryId"].ToString();
            var cityIdInfo = formValues["CityId"].ToString();

            if (fullInfoProduct != null) product.FullDescription = fullInfoProduct;
            if (countryIdInfo != "") product.CountryId = Int32.Parse(countryIdInfo);
            if (cityIdInfo != "") product.CityId = Int32.Parse(cityIdInfo);

            if (changeTitleImagePath != null)
            {
                var folder = "/images/ProductsTitleImages/";
                product.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
            }

            product.DateAdded = DateTime.Now;

            return product;
        }

        public void SaveProduct(Product product)
        {
            if (product != null)
            {
                _AllProducts.SaveItem(product, product.Id);
            }
        }

        public Product SetProductModelByFormValues(Product product, IFormCollection formValues)
        {
            var fullInfoProduct = formValues["fullInfoAboutProduct"].ToString();
            if (fullInfoProduct != null) product.FullDescription = fullInfoProduct;

            return product;
        }
    }
}
