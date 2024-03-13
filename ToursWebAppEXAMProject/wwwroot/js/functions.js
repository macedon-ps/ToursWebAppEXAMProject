// экспортируемые во вне функции и ссылка на HTML элемент
export { GetCountriesCitiesAssocArray, GetCountriesMapsAssocArray, changeCountry, changeCity, UpdateCitiesElement }
export { citiesElement }
// export { backCountryElement, backCityElement }

// импортируемые ссылки на HTML элементы и импортируемый ассоциативный массив
import { countriesElement, citiesElement, countryMapElement, countryDescElement, cityDescElement, localDescElement, backCountryElement, backCityElement } from './search.js'
import { countriesCitiesAssocArray, countriesMapsAssocArray } from './search.js'

// 3.1. Функция создания ассоциативного массива из строки стран и городов
function GetCountriesCitiesAssocArray(allByOneString) {
    let assocArray = new Map();
    let key_AssocArray = "";
    let value_AssocArray = "";
    let values_AssocArray = [];
    // slice(0,-1) - возвращаем копию массива с элементами от 0 до 2-го с конца, т.е. без последней ","
    // т.о. убираем последний пустой элемент массива
    // split("\n") - полученную строку разбиваем по разделителю "\n"
    var allArray = allByOneString.slice(0, -1).split("\n");

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

// 3.2. Функция создания ассоциативного массива из строки стран и карт
function GetCountriesMapsAssocArray(allByOneString) {
    let assocArray = new Map();
    let key_AssocArray = "";
    let value_AssocArray = "";
    // slice(0,-1) - возвращаем копию массива с элементами от 0 до 3-го с конца, т.е. без последней "\n" ("\r\n"")
    // т.о. убираем последний пустой элемент массива
    // split("\n") - полученную строку разбиваем по разделителю "\n"
    var allArray = allByOneString.slice(0, -2).split("\n");

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


// 4.1. Функция, кот. обрабатывает изменение списка стран
function changeCountry() {

    console.log("4.1. Меняем страну");
    // 4.1. Создадим переменную для имени выбранной страны
    let selectCountryName = "";

    // 4.2. Найдем значение выбранной страны из списка стран
    if (countriesElement.options[countriesElement.selectedIndex].value) {
        selectCountryName = countriesElement.options[countriesElement.selectedIndex].value;
        //console.log(`4.2. Выберем страну из списка стран - ${selectCountryName}`);
    }
    else {
        console.log(`4.2. Ни одна страна не выбрана`);
    }
        
    // 4.3. Устанавим выбранный элемент для страницы возврата
    backCountryElement.value = selectCountryName;                                 
    console.log(`4.3. Установим backCountry = ${backCountryElement.value}`);

    // 4.4. Меняем карту страны
    console.log(`4.4. Меняем карту страны`);
    let countryMap = GetMapPath(selectCountryName);
    countryMapElement.innerHTML = countryMap;

    console.log(`countryMap:  ${countryMapElement.innerHTML}`);
    console.log("---------------------------------------------");

    // 5. Вызовем функцию создания нового и замены старого списка городов
    UpdateCitiesElement(selectCountryName, countriesCitiesAssocArray);
    UpdateCountryDesc();
}

// 5.1. Функция, кот. обрабатывает изменение списка городов
function changeCity() {

    console.log("5.1. Меняем город");
    // 5.1. Создадим переменную для имени выбранного города
    let selectCityName = "";
        
    // 5.2. Найдем значение выбранного города из списка городов
    if (citiesElement.options[citiesElement.selectedIndex]) {
        selectCityName = citiesElement.options[citiesElement.selectedIndex].value;
        //console.log(`5.2. Выберем город из списка городов - ${selectCityName}`);
    }
    else if (newCitiesSelectListElement.options[newCitiesSelectListElement.selectedIndex]) {
        selectCityName = newCitiesSelectListElement.options[newCitiesSelectListElement.selectedIndex].value;
        //console.log(`5.2. Выберем город из списка городов - ${selectCityName}`);
    }
    else {
        console.log(`5.2. Ни один город не выбран`);
    }
    
    // 5.4. Устанавим выбранный элемент для страницы возврата
    backCityElement.value = selectCityName;
    console.log(`5.4. Установим backCity = ${backCityElement.value}`);
    console.log("---------------------------------------------");
    UpdateCityDesc();
    UpdateLocalDesc();
}

// 5.5. Функция создания Html элемента <select></select> для списка городов
function UpdateCitiesElement(selectCountryName, assocArray) {

    console.log("5.5. Создадим список городов для выбранной страны");

    // 5.5. Создадим новый список городов и присваиваем ему id
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
    //console.log("5.6. Новый список городов создан");

    // 5.7. Проверим новый созданный список городов
    //console.log(`Проверим новый список городов: ${newCityListSelectElement.innerHTML}`);

    // 5.8. Проверим количество городов в списке
    // если 1 элемент в массиве, то установим его как выбранный элемент для списка городов
    if (newCitiesArray.length === 1) {
        console.log(`5.8. В списке всего один город = ${newCityListSelectElement.innerHTML}`);
    }
    else {
        console.log(`5.8. В списке ${newCitiesArray.length} городов`);
        console.log(`Города: ${newCitiesArray.join(', ')}`);
    }

    // 5.9. Заменим старый список городов новым, только созданным
    citiesElement.innerHTML = newCityListSelectElement.innerHTML;
    console.log(`5.9. Заменен список городов в разметке`);

    // 5.10. Повторно подпишемся на событие изменение города в новом созданном списке городов
    citiesElement.addEventListener("change", changeCity);
    console.log("5.10. Повторно подпишемся на событие изменения города в новом списке городов");

    // 5.11. Найдем значение выбранного города из списка городов
    let selectCityName = "";

    if (citiesElement.options[citiesElement.selectedIndex]) {
        selectCityName = citiesElement.options[citiesElement.selectedIndex].value;
        console.log(`5.11. Выбран город из списка городов - ${selectCityName}`);
    }
    else {
        console.log(`5.11. Ни один город не выбран`);
    }

    // 5.12. Устанавливаем выбранный элемент для страницы возврата
    backCityElement.value = selectCityName;
    console.log(`5.12. Установим backCity = ${backCityElement.value}`);
    console.log("---------------------------------------------");
}

// 5.5.1. Функция выбора из ассоциативного массива - массива значений городов по ключу - названию страны 
function GetCitiesArray(countryName, allCountriesAssocArray) {

    console.log("5.5.1. Вытянем из ассоц. массива новый список городов");

    let cities = [];
    allCountriesAssocArray.forEach(function (_value, _key) {
        if (_key === countryName) {
            cities = _value;
        }
    });
    return cities;
}

// 5.5.2. Функция для вывода пути к карте страны по ее названию
function GetMapPath(countryName) {
    // TODO: сделать вывод карты из ассоц. массива для "страна" : "путь к карте страны"
    let mapPath = "";
    countriesMapsAssocArray.forEach(function (_value, _key) {
        if (_key === countryName) {
            mapPath = _value;
        }
    });
    return mapPath;
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