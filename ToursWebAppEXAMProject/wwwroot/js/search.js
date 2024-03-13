export { countriesElement, citiesElement, backCountryElement, backCityElement }
export { CountriesAndCitiesArray, CountriesAndMapsArray }

import { GetCountriesCitiesAssocArray, GetCountriesMapsAssocArray, changeCountry, changeCity } from './functions.js';

// 1. Находим все нужные Html элементы
const countriesElement = document.getElementById('countriesSelect');
const citiesElement = document.getElementById('citiesSelect');
// тут б. данные о последних выбранных стране и городе
let backCountryElement = document.getElementById('backCountry');
let backCityElement = document.getElementById('backCity');
// данные одной строкой - о странах и городах, о странах и картах
const allCountriesAndCitiesElement = document.getElementById('AllCountriesAndCities');
const allCountriesAndMapsElement = document.getElementById('AllCountriesAndMaps');
console.log("1. Находим Html элементы");

// 2. Сохраним значение строк данных о всех странах и городах, о всех странах и их картах из БД
const allCountriesAndCitiesByString = allCountriesAndCitiesElement.value;
const allCountriesAndMapsByString = allCountriesAndMapsElement.value;
console.log(`2.1. Переменная для стран и городов одной строкой: \n ${allCountriesAndCitiesByString}`);
console.log(`2.2. Переменная для стран и карт одной строкой: \n ${allCountriesAndMapsByString}`);

// 3. Из данных строк сформируем ассоциативные массивы
// 3.1. ключ - "страна" :  значение - ["город", "город", "город", "город"]
const CountriesAndCitiesArray = GetCountriesCitiesAssocArray(allCountriesAndCitiesByString);
console.log("3.1. Создаем ассоц.массив для стран и городов");

// 3.2. ключ - "страна" :  значение - "путь к карте страны"
const CountriesAndMapsArray = GetCountriesMapsAssocArray(allCountriesAndMapsByString);
console.log("3.2. Создаем ассоц.массив для стран и карт");

// 4. Подпишемся на событие изменение списка городов в связи с изменением страны
countriesElement.addEventListener("change", changeCountry);
console.log("4.1. Подпишемся на событие изменения страны в списке стран");

// 5. Подпишемся на событие изменение города в созданном списке городов
citiesElement.addEventListener("change", changeCity);
console.log("4.2. Подпишемся на событие изменения города в списке городов");

//6. Установим ссылки на элементы для backCountryElement и backCityElement
backCountryElement.value = countriesElement.value;
backCityElement.value = citiesElement.value;
console.log(`5.1. Переменная для последней выбранной страны: \n ${backCountryElement.value}`);
console.log(`5.2. Переменная для последнего выбранного городай: \n ${backCityElement.value}`);