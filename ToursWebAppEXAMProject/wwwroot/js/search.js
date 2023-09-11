export { countriesListSelectElement, citiesListSelectElement, countryMapElement, countryDescElement, cityDescElement, localDescElement, backCountryValueElement, backCityValueElement }
export { countriesCitiesAssocArray, countriesMapsAssocArray }

import { GetCountriesCitiesAssocArray, GetCountriesMapsAssocArray, changeCountry, changeCity } from './functions.js';
// import { backCountryValueElement, backCityValueElement } from './functions.js';

// Скрипт по изменению данных 2-х Html-элементов Select (список стран в БД и список городов для выбранной страны из БД) 
// по событию изменения значения первого Select (список стран). Изменения происходят без перезагрузки страницы.

// 1. Находим все нужные Html элементы
// 1.1. Находим select эдементы по аттрибуту "id" - список стран и список городов
const countriesListSelectElement = document.getElementById('countriesSelect');
const citiesListSelectElement = document.getElementById('citiesSelect');
const countryMapElement = document.getElementById('countryMap');
const countryDescElement = document.getElementById('countryDesc');
const cityDescElement = document.getElementById('cityDesc');
const localDescElement = document.getElementById('localDesc');

// 1.2. Находим по id input type="hidden", в кот. хранится информация о странах и городах, о странах и картах из БД 
const allCountriesAndCitiesElement = document.getElementById('AllCountriesAndCities');
const allCountriesAndMapsElement = document.getElementById('AllCountriesAndMaps');

// 1.3. Находим данные о последних выбранных стране и городе для возврата на предыдущую страницу
let backCountryValueElement = document.getElementById('backCountry');
let backCityValueElement = document.getElementById('backCity');
console.log("1. Найдем 10 элементов по их id");

// 2. Сохраним значение строки данных о всех странах и городах, о всех странах и их картах из БД
const allCountriesAndCitiesString = allCountriesAndCitiesElement.value;
const allCountriesAndMapsString = allCountriesAndMapsElement.value;
console.log(`2.1. Присвоим значение переменным allCountriesAndCitiesString = ${allCountriesAndCitiesString}`);
console.log(`2.2. Присвоим значение переменным allCountriesAndMapsString = ${allCountriesAndMapsString}`);

// 3. Из данных строк сформируем ассоциативные массивы
// 3.1. ключ - "страна" :  значение - ["город", "город", "город", "город"]
const countriesCitiesAssocArray = GetCountriesCitiesAssocArray(allCountriesAndCitiesString);
console.log(`3.1. Создадим ассоц. массив countriesCitiesAssocArray: ${countriesCitiesAssocArray}`);

// 3.2. ключ - "страна" :  значение - "путь к карте страны"
const countriesMapsAssocArray = GetCountriesMapsAssocArray(allCountriesAndMapsString);
console.log(`3.2. Создадим ассоц. массив countriesCitiesAssocArray: ${countriesMapsAssocArray}`);

// 4. Подпишемся на событие изменение списка городов в связи с изменением страны
countriesListSelectElement.addEventListener("change", changeCountry);
console.log("4. Подпишемся на событие изменения страны в списке стран");

// 5. Подпишемся на событие изменение города в созданном списке городов
citiesListSelectElement.addEventListener("change", changeCity);
console.log("5. Подпишемся на событие изменения города в списке городов");

//6. Установим ссылки на элементы для backCountryValueElement и backCityValueElement
backCountryValueElement.value = countriesListSelectElement.value;
backCityValueElement.value = citiesListSelectElement.value;