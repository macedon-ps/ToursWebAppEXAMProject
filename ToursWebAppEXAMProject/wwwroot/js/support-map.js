const mapPreview = document.getElementById("mapPreview");
const buttonElement = document.getElementById("showLocationBtn");
const mapContainer = document.getElementById("mapContainer");
const mapElement = document.getElementById("supportMap");

showLocationBtn.addEventListener("click", function () {

    if (!mapElement)
        return;

    navigator.geolocation.getCurrentPosition(

        function (position) {

            const lat = position.coords.latitude;
            const lng = position.coords.longitude;

            const map = L.map('supportMap')
                .setView([lat, lng], 13);

            L.tileLayer(
                'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                {
                    attribution: '© OpenStreetMap contributors'
                }
            ).addTo(map);

            L.marker([lat, lng])
                .addTo(map)
                .bindPopup("Вы здесь")
                .openPopup();

            setTimeout(() => {
                map.invalidateSize();
            }, 100);

        },

        function (error) {

            console.error(error);

            alert("Не удалось получить геолокацию.");

        }

    );

    mapPreview.style.display = "none";
    mapContainer.style.display = "block";

});