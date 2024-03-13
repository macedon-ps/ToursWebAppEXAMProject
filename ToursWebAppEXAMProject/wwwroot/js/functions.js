export { GetCountriesCitiesAssocArray, GetCountriesMapsAssocArray, changeCountry, changeCity }

import { countriesElement, citiesElement, backCountryElement, backCityElement } from './search.js'
import { CountriesAndCitiesArray, CountriesAndMapsArray } from './search.js'


// 1. Находим все нужные Html элементы
const countryMapElement = document.getElementById('countryMap');
const countryDescElement = document.getElementById('countryDesc');
const cityDescElement = document.getElementById('cityDesc');
const localDescElement = document.getElementById('localDesc');


// Функция изменения страны при выборе в выпадающем списке стран
function changeCountry()
{
    let selectCountryName = "";

    // Найдем значение выбранной страны из списка стран
    if (countriesElement.options[countriesElement.selectedIndex].value) {
        selectCountryName = countriesElement.options[countriesElement.selectedIndex].value;
    }
    
    backCountryElement.value = selectCountryName;

    // Вызовем функцию создания нового и замены старого списка городов
    UpdateCitiesElement(selectCountryName, CountriesAndCitiesArray);

    // Вызовем функцию изменения карты страны
    UpdateCountryMap(selectCountryName);
    
    // Вызовем функцию изменения описания страны
    UpdateCountryDesc();
}

// Функция изменение города при выборе в выпадающем списке городов
function changeCity() {

    let selectCityName = "";

    // Найдем значение выбранного города из списка городов
    if (citiesElement.options[citiesElement.selectedIndex]) {
        selectCityName = citiesElement.options[citiesElement.selectedIndex].value;
    }
   
    backCityElement.value = selectCityName;

    // Вызовем функцию изменения описания города
    UpdateCityDesc();

    // Вызовем функцию изменения описания достопримечательностей
    UpdateLocalDesc();
}


// Функция создания ассоциативного массива из строки стран и городов
function GetCountriesCitiesAssocArray(allByOneString) {
    let assocArray = new Map();
    let key_AssocArray = "";
    let value_AssocArray = "";
    let values_AssocArray = [];
    let allArray = allByOneString.slice(0, -1).split("\n");
    // slice(0,-1) - возвращаем копию массива с элементами от 0 до 2-го с конца, т.е. без последней ","
    // т.о. убираем последний пустой элемент массива
    // split("\n") - полученную строку разбиваем по разделителю "\n"
    
    // преобразуем обычный массив allArray в ассоциативный массив assocArray
    for (var i = 0; i < allArray.length; i++) {
        let item = allArray[i];
        // возвращаем копию массива с элементами от 0 до 2-го с конца, т.е. без последней запятой (,)
        item = item.slice(0, -1);
        // делим каждый элемент на "ключ" и "значение" 
        let massiv = item.split(":");
        key_AssocArray = massiv[0];
        value_AssocArray = massiv[1];
        if (value_AssocArray.includes(",")) {
            // создаем массив values_AssocArray по разделителю ","
            values_AssocArray = value_AssocArray.split(',');
        } else {
            values_AssocArray.push(value_AssocArray);
        }
        // добавляем новый элемент ассоциативного массива в виде пары "ключ" : "значение"
        assocArray.set(key_AssocArray, values_AssocArray);
        // обнуление значения, чтобы очищалось значение списка горордов
        values_AssocArray = [];
    }
    return assocArray;
}    

// Функция создания ассоциативного массива из строки стран и карт
function GetCountriesMapsAssocArray(allByOneString) {

    let assocArray = new Map();
    let key_AssocArray = "";
    let value_AssocArray = "";
    let allArray = allByOneString.slice(0, -2).split("\n");
    // slice(0,-1) - возвращаем копию массива с элементами от 0 до 3-го с конца, т.е. без последней "\n" ("\r\n"")
    // т.о. убираем последний пустой элемент массива
    // split("\n") - полученную строку разбиваем по разделителю "\n"
    
    // преобразуем обычный массив allArray в ассоциативный массив assocArray
    for (var i = 0; i < allArray.length; i++) {

        let item = allArray[i];
        // делим каждый элемент на "ключ" и "значение" по символу "#" (м.исп-ть др. спец. символы как разделители, 
        // но ":" есть в адресе)
        let massiv = item.split("#");
        key_AssocArray = massiv[0];
        value_AssocArray = massiv[1];
        
        // добавляем новый элемент ассоциативного массива в виде пары "ключ" : "значение"
        assocArray.set(key_AssocArray, value_AssocArray);
    }
    return assocArray;
}


// Функция выбора из ассоциативного массива - массива значений городов по ключу - названию страны 
function GetCitiesArray(countryName, allCountriesAssocArray) {

    let cities = [];
    allCountriesAssocArray.forEach(function (_value, _key) {
        if (_key === countryName) {
            cities = _value;
        }
    });
    return cities;
}

// Функция для вывода пути к карте страны по ее названию
function GetMapPath(countryName) {
    let mapPath = "";
    CountriesAndMapsArray.forEach(function (_value, _key) {
        if (_key === countryName) {
            mapPath = _value;
        }
    });
    return mapPath;
}


// Функция создания Html элемента <select></select> для списка городов
function UpdateCitiesElement(selectCountryName, assocArray) {

    // Создадим новый список городов и присваиваем ему id
    let newCityListSelectElement = document.createElement("select");
    newCityListSelectElement.setAttribute("id", "citiesSelect");

    let newCitiesArray = GetCitiesArray(selectCountryName, assocArray);

    for (var i = 0; i < newCitiesArray.length; i++) {
        var option = document.createElement("option");
        newCityListSelectElement.append(option);
        option.value = newCitiesArray[i];
        option.text = newCitiesArray[i];

        // зададим выбранный город, по умолчанию - с индексом 0
        if (i === 0) newCityListSelectElement.value = newCityListSelectElement.options[i].value;
    }

    // Заменим старый список городов новым, только созданным
    citiesElement.innerHTML = newCityListSelectElement.innerHTML;

    // Повторно подпишемся на событие изменение города в новом созданном списке городов
    citiesElement.addEventListener("change", changeCity);

    // Найдем значение выбранного города из списка городов
    let selectCityName = "";

    if (citiesElement.options[citiesElement.selectedIndex]) {
        selectCityName = citiesElement.options[citiesElement.selectedIndex].value;
    }

    // Устанавливаем выбранный элемент для страницы возврата
    backCityElement.value = selectCityName;
}

function UpdateCountryMap(selectCountryName) {
    let countryMap = GetMapPath(selectCountryName);
    countryMapElement.innerHTML = countryMap;
}
function UpdateCountryDesc() {
    console.log("Обновляем описание страны");
}
function UpdateCityDesc() {
    console.log("Обновляем описание города");
}
function UpdateLocalDesc() {
    console.log("Обновляем описание достопримечательности");
    console.log("Выводим фото достопримечательности");
}