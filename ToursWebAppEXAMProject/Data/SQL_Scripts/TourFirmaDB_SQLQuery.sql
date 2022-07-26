/*1. Примеры запросов к базе данных TourFirmaDB*/

/*
1.1. Вывод информации в виде:
Тур / Турпродукт / Начало тура / Окончание тура / Описание
*/

select Tour.Name as 'Тур', Product.Name as 'Турпродукт', 
DateTour.DateStart as 'Начало тура', DateTour.DateEnd as 'Окончание тура', Product.Description as 'Описание'
from Tour, Product, DateTour
where Tour.ProductId = Product.Id and Tour.DateTourId=DateTour.Id


/*
1.2. Вывод информации в виде:
Тур / Турпродукт / Начало тура / Окончание тура / Страна / Город / Гостинница / Описание
*/

select Tour.Name as 'Тур', Product.Name as 'Турпродукт', 
DateTour.DateStart as 'Начало тура', DateTour.DateEnd as 'Окончание тура', 
Country.Name as 'Страна', City.Name as 'Город', Hotel.Name as 'Гостинница', Hotel.LevelHotel as 'Качество обслуживания',
Product.Description as 'Описание'
from Tour, Product, DateTour, Location, Country, City, Hotel
where Tour.ProductId = Product.Id and Tour.DateTourId=DateTour.Id 
and Tour.LocationId = Location.Id and Country.Id = Location.CountryId and City.Id = Location.CityId and Hotel.Id = Location.HotelId