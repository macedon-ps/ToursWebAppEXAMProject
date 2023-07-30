// экспортируемые во вне функции и ссылка на HTML элемент
export { GetAllCountriesWithCitiesAssocArray, changeCountry, changeCity, UpdateCitiesSelectElement }
export { citiesListSelectElement }
// export { backCountryValueElement, backCityValueElement }

// импортируемые ссылки на HTML элементы и импортируемый ассоциативный массив
import { countriesListSelectElement, citiesListSelectElement, backCountryValueElement, backCityValueElement } from './search.js'
import { countriesWithCitiesAssocArray } from './search.js'

// 3.1. Функция создания ассоциативного массива из строки стран и городов
function GetAllCountriesWithCitiesAssocArray(allByOneString) {
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

// 4.1. Функция, кот. обрабатывает изменение списка стран
function changeCountry() {

    console.log("4.1. Поменяем страну");
    // 4.1. Создадим переменную для имени выбранной страны
    let selectCountryName = "";

    // 4.2. Найдем значение выбранной страны из списка стран
    if (countriesListSelectElement.options[countriesListSelectElement.selectedIndex].value) {
        selectCountryName = countriesListSelectElement.options[countriesListSelectElement.selectedIndex].value;
        console.log(`4.2. Выберем страну из списка стран - ${selectCountryName}`);
    }
    else {
        console.log(`4.2. Ни одна страна не выбрана`);
    }
        
    // 4.3. Устанавим выбранный элемент для страницы возврата
    backCountryValueElement.value = selectCountryName;                                 
    console.log(`4.3. Установим backCountryValueElement.value = ${backCountryValueElement.value}`);
    console.log("---------------------------------------------");

    // 5. Вызовем функцию создания нового и замены старого списка городов
    UpdateCitiesSelectElement(selectCountryName, countriesWithCitiesAssocArray);
}

// 5.1. Функция, кот. обрабатывает изменение списка городов
function changeCity() {

    console.log("5.1. Поменяем город");
    // 5.1. Создадим переменную для имени выбранного города
    let selectCityName = "";
        
    // 5.2. Найдем значение выбранного города из списка городов
    if (citiesListSelectElement.options[citiesListSelectElement.selectedIndex]) {
        selectCityName = citiesListSelectElement.options[citiesListSelectElement.selectedIndex].value;
        console.log(`5.2. Выберем город из списка городов - ${selectCityName}`);
    }
    else if (newCitiesSelectListElement.options[newCitiesSelectListElement.selectedIndex]) {
        selectCityName = newCitiesSelectListElement.options[newCitiesSelectListElement.selectedIndex].value;
        console.log(`5.2. Выберем город из списка городов - ${selectCityName}`);
    }
    else {
        console.log(`5.2. Ни один город не выбран`);
    }

    // 5.4. Устанавим выбранный элемент для страницы возврата
    backCityValueElement.value = selectCityName;
    console.log(`5.4. Установим backCityValueElement.value = ${backCityValueElement.value}`);
    console.log("---------------------------------------------");
}


// 5.5. Функция создания Html элемента <select></select> для списка городов
function UpdateCitiesSelectElement(selectCountryName, assocArray) {

    console.log("5.5. Создадим новый список городов для выбранной страны");

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
    console.log("5.6. Новый список городов создан");

    // 5.7. Проверим новый созданный список городов
    console.log(`Проверим новый список городов: ${newCityListSelectElement.innerHTML}`);

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
    citiesListSelectElement.innerHTML = newCityListSelectElement.innerHTML;
    console.log(`5.9. Заменен список городов в разметке`);

    // 5.10. Повторно подпишемся на событие изменение города в новом созданном списке городов
    citiesListSelectElement.addEventListener("change", changeCity);
    console.log("5.10. Повторно подпишемся на событие изменения города в новом списке городов");

    // 5.11. Найдем значение выбранного города из списка городов
    let selectCityName = "";

    if (citiesListSelectElement.options[citiesListSelectElement.selectedIndex]) {
        selectCityName = citiesListSelectElement.options[citiesListSelectElement.selectedIndex].value;
        console.log(`5.11. Выбран город из списка городов - ${selectCityName}`);
    }
    else {
        console.log(`5.11. Ни один город не выбран`);
    }

    // 5.12. Устанавливаем выбранный элемент для страницы возврата
    backCityValueElement.value = selectCityName;
    console.log(`5.12. Установим backCityValueElement.value = ${backCityValueElement.value}`);
    console.log("---------------------------------------------");
}

// Функция выбора из ассоциативного массива - массива значений городов по ключу - названию страны 
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