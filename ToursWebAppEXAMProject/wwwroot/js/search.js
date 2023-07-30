export { countriesListSelectElement, citiesListSelectElement, backCountryValueElement, backCityValueElement }
export { countriesWithCitiesAssocArray }

import { GetAllCountriesWithCitiesAssocArray, changeCountry, changeCity } from './functions.js';
// import { backCountryValueElement, backCityValueElement } from './functions.js';

// Скрипт по изменению данных 2-х Html-элементов Select (список стран в БД и список городов для выбранной страны из БД) 
// по событию изменения значения первого Select (список стран). Изменения происходят без перезагрузки страницы.

// 1. Находим все нужные Html элементы
// 1.1. Находим select эдементы по аттрибуту "id" - список стран и список городов
let countriesListSelectElement = document.getElementById('countriesSelect');
let citiesListSelectElement = document.getElementById('citiesSelect');

// 1.2. Находим по id input type="hidden", в кот. хранится информация о всех странах и городах, кот. есть в БД 
const allCountriesAndCitiesElement = document.getElementById('AllCountriesAndCities');

// 1.3. Находим данные о последних выбранных стране и городе для возврата на предыдущую страницу
let backCountryValueElement = document.getElementById('backCountry');
let backCityValueElement = document.getElementById('backCity');
console.log("1. Найдем 5 элементов по их id");

// 2. Сохраним значение строки данных о всех странах и городах из БД
const allCountriesWithCitiesByOneString = allCountriesAndCitiesElement.value;
console.log(`2. Присвоим значение переменной allCountriesWithCitiesByOneString = ${allCountriesWithCitiesByOneString}`);

// 3. Из данной строки сформируем ассоциативный массив, где ключ - название страны, а значение - массив строк - названий городов
// 3.1. Создаем сам ассоциативный массив 
const countriesWithCitiesAssocArray = GetAllCountriesWithCitiesAssocArray(allCountriesWithCitiesByOneString);
console.log(`3. Создадим ассоц. массив countriesWithCitiesAssocArray: ${countriesWithCitiesAssocArray.value}`);

// 4. Подпишемся на событие изменение списка городов в связи с изменением страны
countriesListSelectElement.addEventListener("change", changeCountry);
console.log("4. Подпишемся на событие изменения страны в списке стран");

// 5. Подпишемся на событие изменение города в созданном списке городов
citiesListSelectElement.addEventListener("change", changeCity);
console.log("5. Подпишемся на событие изменения города в списке городов");

//6. Установим ссылки на элементы для backCountryValueElement и backCityValueElement
backCountryValueElement.value = countriesListSelectElement.value;
backCityValueElement.value = citiesListSelectElement.value;