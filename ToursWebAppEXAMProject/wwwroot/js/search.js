    const countrySelect = document.getElementById("CountryIdSelected");
    const citySelect = document.getElementById("CityIdSelected");

    const citiesCache = {};

    async function loadCities(countryId) {

        if (!countryId) {
            citySelect.innerHTML = '<option value="">Выберите город</option>';
            return;
        }

        if (citiesCache[countryId]) {
            renderCities(citiesCache[countryId]);
            console.log("Cities загрузились из кеша");
            return;
        }

        // загрузка городов для выбранной страны через fetch-запрос к серверу
        const response = await fetch(`/Cities/GetCities?countryId=${countryId}`);
        const cities = await response.json();
        
        citiesCache[countryId] = cities;
        console.log("Cities сохранены типа в кеш");

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

    countrySelect.addEventListener("change", function (e) {
        
        const countryId = countrySelect.value;
        const cityId = citySelect.value;
        
        loadCities(e.target.value);

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

        if (cityId !== null) {
            citySelect.value = cityId;
        } else {
            citySelect[0].selected;
        }
    }
});
