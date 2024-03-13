export { countriesElement, citiesElement, countryMapElement, countryDescElement, cityDescElement, localDescElement, backCountryElement, backCityElement }
export { countriesCitiesAssocArray, countriesMapsAssocArray }

import { GetCountriesCitiesAssocArray, GetCountriesMapsAssocArray, changeCountry, changeCity } from './functions.js';
// import { backCountryElement, backCityElement } from './functions.js';

// Скрипт по изменению данных 2-х Html-элементов Select (список стран в БД и список городов для выбранной страны из БД) 
// по событию изменения значения первого Select (список стран). Изменения происходят без перезагрузки страницы.

// 1. Находим все нужные Html элементы
// 1.1. Находим select эдементы по аттрибуту "id" - список стран и список городов
const countriesElement = document.getElementById('countriesSelect');
const citiesElement = document.getElementById('citiesSelect');
const countryMapElement = document.getElementById('countryMap');
const countryDescElement = document.getElementById('countryDesc');
const cityDescElement = document.getElementById('cityDesc');
const localDescElement = document.getElementById('localDesc');

// 1.2. Находим по id input type="hidden", в кот. хранится информация о странах и городах, о странах и картах из БД 
const allCountriesAndCitiesElement = document.getElementById('AllCountriesAndCities');
const allCountriesAndMapsElement = document.getElementById('AllCountriesAndMaps');

// 1.3. Находим данные о последних выбранных стране и городе для возврата на предыдущую страницу
let backCountryElement = document.getElementById('backCountry');
let backCityElement = document.getElementById('backCity');
//console.log("1. Найдем 10 элементов по их id");

// 2. Сохраним значение строки данных о всех странах и городах, о всех странах и их картах из БД
const allCountriesAndCitiesString = allCountriesAndCitiesElement.value;
const allCountriesAndMapsString = allCountriesAndMapsElement.value;
//console.log(`2.1. Все страны и города одной строкой (allCountriesAndCitiesString): ${allCountriesAndCitiesString}`);
//console.log(`2.2. Все страны и карты одной строкой (allCountriesAndMapsString): ${allCountriesAndMapsString}`);

// 3. Из данных строк сформируем ассоциативные массивы
// 3.1. ключ - "страна" :  значение - ["город", "город", "город", "город"]
const countriesCitiesAssocArray = GetCountriesCitiesAssocArray(allCountriesAndCitiesString);
console.log(`3.1. Ассоц. массив стран и городов (countriesCitiesAssocArray): ${countriesCitiesAssocArray}`);

// 3.2. ключ - "страна" :  значение - "путь к карте страны"
const countriesMapsAssocArray = GetCountriesMapsAssocArray(allCountriesAndMapsString);
console.log(`3.2. Ассоц. массив стран и карт (countriesCitiesAssocArray): ${countriesMapsAssocArray}`);

// 4. Подпишемся на событие изменение списка городов в связи с изменением страны
countriesElement.addEventListener("change", changeCountry);
console.log("4. Подпишемся на событие изменения страны в списке стран");

// 5. Подпишемся на событие изменение города в созданном списке городов
citiesElement.addEventListener("change", changeCity);
console.log("5. Подпишемся на событие изменения города в списке городов");

//6. Установим ссылки на элементы для backCountryElement и backCityElement
backCountryElement.value = countriesElement.value;
backCityElement.value = citiesElement.value;