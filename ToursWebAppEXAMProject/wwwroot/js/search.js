document.addEventListener("DOMContentLoaded", () => {

    const countrySelect = document.getElementById("countrySelect");
    const citySelect = document.getElementById("citySelect");

    const citiesCache = {};

    async function loadCities(countryId) {

        if (!countryId) {
            citySelect.innerHTML = '<option value="">Выберите город</option>';
            return;
        }

        if (citiesCache[countryId]) {
            renderCities(citiesCache[countryId]);
            return;
        }

        const response = await fetch(`/Search/GetCities?countryId=${countryId}`);
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

    countrySelect.addEventListener("change", function () {
        loadCities(this.value);
    });

    citySelect.addEventListener("change", function () {
        const countryId = countrySelect.value;
        const cityId = this.value;

        window.location.href = `/search?countryId=${countryId}&cityId=${cityId}`;
    });
});