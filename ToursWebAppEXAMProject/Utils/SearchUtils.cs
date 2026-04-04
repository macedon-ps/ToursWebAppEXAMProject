using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.DTOs;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class SearchUtils
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly TourFirmaDBContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<SearchUtils> _logger;

        private const string SearchCacheKey = "SEARCH_COUNTRIES_DATA";

        public SearchUtils(
            IBaseInterface<Country> AllCountries,
            IBaseInterface<City> AllCities,
            TourFirmaDBContext context,
            IMemoryCache cache,
            ILogger<SearchUtils> logger)
        {
            _AllCountries = AllCountries;
            _AllCities = AllCities;
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public SearchProductViewModel GetModel(int? countryId, int? cityId, string? map)
        {
            var vm = new SearchProductViewModel();

            try
            {
                var countries = GetCountriesData();

                if(countryId !=0 && cityId != 0)
                {
                    vm.CountryIdSelected = countryId;
                    vm.CityIdSelected = cityId;

                    var cities = countries.FirstOrDefault(c => c.Id == countryId)?.Cities ?? new List<CityDto>();

                    vm.CitiesList = new SelectList(
                        cities,
                        "Id",
                        "Name",
                        vm.CityIdSelected
                    );
                }
                               
                vm.CountriesData = countries;

                vm.CountriesList = new SelectList(
                    countries,
                    "Id",
                    "Name",
                    vm.CountryIdSelected
                );

                vm.MapImagePath = map ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания SearchProductViewModel");
            }

            return vm;
        }

        private List<CountryDto> GetCountriesData()
        {
            if (_cache.TryGetValue(SearchCacheKey, out List<CountryDto> cached))
            {
                return cached;
            }

            var data = _context.Countries
                .Include(c => c.Cities)
                .AsNoTracking()
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Map = c.CountryMapPath,
                    Cities = c.Cities.Select(ct => new CityDto
                    {
                        Id = ct.Id,
                        Name = ct.Name
                    }).ToList()
                })
                .ToList();

            _cache.Set(SearchCacheKey, data, TimeSpan.FromMinutes(30));

            return data;
        }

        // TODO: тут что-то не работает (countryId, cityId - не используются)
        public QueryResultProductViewModel GetQueryResulpProductsViewModel(SearchFormViewModel formViewModel, List<Product> products, int? countryId, int? cityId)
        {
            var viewModel = new QueryResultProductViewModel
            {
                Products = products,
                DateFrom = formViewModel.DateFrom,
                DateTo = formViewModel.DateTo,
                NumberOfDaysFromSelectList = formViewModel.NumberOfDaysFromSelectList,
                NumberOfPeopleFromSelectList = formViewModel.NumberOfPeopleFromSelectList,
                Country = _AllCountries.GetAllItems().FirstOrDefault(c => c.Id == formViewModel.CountryIdSelected) ?? new Country(),
                City = _AllCities.GetAllItems().FirstOrDefault(c => c.Id == formViewModel.CityIdSelected) ?? new City(),
            };

            return viewModel;
        }
    }
}
