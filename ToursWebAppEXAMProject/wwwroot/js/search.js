    const countrySelect = document.getElementById("CountryIdSelected");
    const citySelect = document.getElementById("CityIdSelected");
    const mapCountry = document.getElementById("countryMap");

    let currentCountryId = null;
    const citiesCache = {};
    const mapCache = {};

    async function loadCities(countryId) {

        if (!countryId) {
            citySelect.innerHTML = '<option value="">Выберите город</option>';
            return;
        }

        if (citiesCache[countryId]) {
            renderCities(citiesCache[countryId]);
           
            return;
        }

        // загрузка городов для выбранной страны через fetch-запрос к серверу
        const response = await fetch(`/Cities/GetCities?countryId=${countryId}`);
        const cities = await response.json();
        
        citiesCache[countryId] = cities;
        
        renderCities(cities);
    }

    function renderCities(cities) {
        
        citySelect.innerHTML = '<option value="">Выберите город</option>';
       
        cities.forEach(city => {
            const option = document.createElement("option");
            option.value = city.id;
            option.textContent = city.name;
            citySelect.appendChild(option);
        });
    }

    async function loadMap(countryId) {

        if (!countryId) {
            return;
        }

        if (mapCache[countryId]) {
            renderMap(mapCache[countryId]);
            console.log(`Map загрузилась из кеша: ${mapCache[countryId]}`);
            return;
        }

        // загрузка карты для выбранной страны через fetch-запрос к серверу
        const response = await fetch(`/Countries/GetMap?countryId=${countryId}`);
        const map = await response.json();

        mapCache[countryId] = map;
        
        renderMap(map);
    }

    function renderMap(map) {

        if (map === "" || map === "Нет ссылки на карту страны в GoogleMaps") return;

        mapCountry.innerHTML = `${map}`;
    }

    countrySelect.addEventListener("change", function (e) {
        
        const countryId = countrySelect.value;
        currentCountryId = countryId;

        loadCities(e.target.value);
        loadMap(e.target.value);

        const map = mapCountry.innerHTML;
        console.log(`map: ${map}`);

        // замена URL без перезагрузки страницы, с изменением истории браузера
        history.pushState({}, "", `/search?countryId=${countryId}`);
        // меняем заголовок страницы
        document.title = `Поиск туров: /search?countryId=${countryId ?? ""}`;
    });

    citySelect.addEventListener("change", function (e) {
        
        const countryId = countrySelect.value;
        const cityId = citySelect.value;
                
        // замена URL без перезагрузки страницы, с изменением истории браузера
        history.pushState({}, "", `/search?countryId=${countryId}&cityId=${cityId}`);
        // меняем заголовок страницы
        document.title = `Поиск туров: /search?countryId=${countryId ?? ""}&cityId=${cityId ?? ""}`;
    });

window.addEventListener("popstate", function (event) {
    const params = new URLSearchParams(window.location.search);

    const countryId = params.get("countryId");
    const cityId = params.get("cityId");
        
    if (countryId !== null) {

        loadCities(countryId);
        countrySelect.value = countryId;

        if (countryId !== currentCountryId) {
            loadMap(countryId);
            currentCountryId = countryId;
        }

        if (cityId !== null) {
            citySelect.value = cityId;
        } else {
            citySelect[0].selected;
        }
    }
});
