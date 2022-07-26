create database TourFirmaDB;
go


/*use TourFirmaDB;
go
*/

create table Product
(
Id int identity(1,1) primary key,
Name nvarchar(50) check (Name<>'') not null,
Description nvarchar(max) check (Description<>'')
);
go
create table Country
(
Id int identity(1,1) primary key,
Name nvarchar(50) check(Name<>'') default('страна') not null,
);
go
create table City
(
Id int identity(1,1) primary key,
Name nvarchar(50) check (Name<>'') not null,
CountryId int not null foreign key references Country(Id),
);
go
create table Hotel
(
Id int identity(1,1) primary key,
Name nvarchar(50) check (Name<>'') not null,
LevelHotel int default(2) not null,
CityId int not null foreign key references City(Id),
);
go
create table Location
(
Id int identity(1,1) primary key,
CountryId int not null foreign key references Country(Id),
CityId int not null foreign key references City(Id),
HotelId int not null foreign key references Hotel(Id)
);
go
create table Food
(
Id int identity(1,1) not null primary key ,
ModeOfEating nvarchar(50) check(ModeOfEating<>'') not null,
);
go
create table DateTour
(
Id int identity(1,1) primary key,
DateStart datetime not null,
DateEnd datetime not null,
NumberOfDays int not null, 
NumberOfNights int not null,
);
go
create table Tour
(
Id int identity(1,1) not null primary key ,
Name nvarchar(50) check(Name<>'') not null,
ProductId int not null foreign key references Product(Id),
DateTourId int not null foreign key references DateTour(Id),
LocationId int not null foreign key references Location(Id),
FoodId int not null foreign key references Food(Id),
);
go
create table Customer
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) check(Name<>'') not null,
Surname nvarchar(50) check(Surname<>'') not null,
Gender nvarchar(10) not null, 
Age int not null,
);
go
create table Saller
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) check(Name<>'') not null,
Surname nvarchar(50) check(Surname<>'') not null,
Position nvarchar(100) default('сотрудник') not null, 
);
go
create table Oferta
(
Id int identity(1,1) not null primary key,
CustomerId int not null foreign key references Customer(Id),
SallerId int not null foreign key references Saller(Id),
TourId int not null foreign key references Tour(Id),
);
go
--заполним ее данными

insert into Product(Name, Description) values
								('Жемчужина Нила', 'Египет принято называть «даром Нила», поскольку без реки этой плодородной и густонаселённой земли, не говоря уже о великой цивилизации, возникшей пять тысяч лет назад, не существовало бы вовсе. На своеобразие и историю страны во многом повлиял разительный контраст между изобилием нильской Долины и Дельты и скудостью окружающей пустыни. Для древних египтян именно здесь находилась родина – Кемет («Чёрная земля»), богатая плодородным Нилом, где природа и люди процветали под сенью милостивых богов, – в противоположность пустыне, воплощению смерти и хаоса, находившейся под властью Сета, бога ветров и стихийных бедствий.'),
								('Пустыня Европы', 'Олешковские пески – это удивительное место в Херсонской области, в Украине, которое напоминает собой настоящую пустыню. За это его прозвали украинской Сахарой. В действительности здесь не только пустыня. Есть также участки с травянистой растительностью, сосновыми борами, камышевыми зарослями. А окружают парки густые искусственные леса. Уникальный природный объект находится недалеко от побережья Черного моря в южной части материковой Украины. Общая площадь национального парка составляет 1600 км2. Территория растянулась на 150 км с юга на север и на 30 км с запада на восток.'),
								('Край тысячи озер', 'Страна озер, невероятной северной природы и край мастеров - Финляндия. На территории Финляндии сохранились старинные поселения финнов, карелов, поморов, вепсов. Здесь вы познакомитесь с уникальной архитектурой, насладитесь притягательными видами и проникнитесь культурой коренных народов. Финляндия хранит в себе многовековую историю. Раскрыть ее помогут старинные деревянные строения, каньоны и водные прогулки по озерным шхерам добавят ярких штрихов вашему отдыху.'),
								('Между Европой и Азией', 'Стамбул – крупнейший город, но, вопреки расхожему мнению, не столица Турции. Как и все мегаполисы, расположенные у большой воды, он невероятно фотогеничен. В том числе, благодаря уникальному расположению между Европой и Азией – город и символически, и географически поделен на две части, и у каждой – свое лицо. Стамбул (Istanbul) – крупнейший город Турции и один из самых больших городов мира, морской порт, крупный промышленно-торговый и культурный центр Турции. Бывшая столица Римской, Византийской, Латинской и Османской империй. Расположен на берегах пролива Босфор. Основная часть города находится в Европе, меньшая – в Азии. Европейская часть, в свою очередь, делится на две части бухтой Золотой Рог. Азиатская и европейская части разделены проливом Босфор, через который построен вантовый мост. В Стамбуле и ближайших пригородах проживает более 13 млн. человек. ');
go
insert into Country (Name) values
								('Египет'),
								('Украина'),
								('Финляндия'),
								('Турция');
go
insert into City (Name, CountryId) values
								('Каир', 1),
								('Херсон', 2),
								('Турку', 3),
								('Стамбул', 4);
go
insert into Hotel (Name, LevelHotel, CityId) values
								('ГрандОтель', 4, 1),
								('Приют в пустыне', 3, 2),
								('ТуркуЮнион', 4, 3),
								('Султанский дворец', 5, 4);
go
insert into Location (CountryId, CityId, HotelId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4);
go
insert into Food (ModeOfEating) values
								('Только завтрак'),
								('Только обед'),
								('Только ужин'),
								('Только завтрак и обед'),
								('Только завтрак и ужин'),
								('Завтрак, обед и ужин');
go
insert into DateTour(DateStart, DateEnd, NumberOfDays, NumberOfNights) values
								('2201-01-01 00:00:00.000', '2201-01-08 00:00:00.000', 7, 7),
								('2201-01-01 00:00:00.000', '2201-01-02 00:00:00.000', 1, 1),
								('2201-01-02 00:00:00.000', '2201-01-04 00:00:00.000', 2, 2),
								('2201-01-03 00:00:00.000', '2201-01-06 00:00:00.000', 3, 3);
go
insert into Tour (Name, ProductId, DateTourId, LocationId, FoodId) values
								('Тур по Египту', 1, 1, 1, 5),
								('Тур по Украине', 2, 2, 2, 1),
								('Тур по Финляндии', 3, 3, 3, 6),
								('Тур по Турции', 4, 4, 4, 4);
go
insert into Customer(Name, Surname, Gender, Age) values
								('Семен', 'Степанов', 'мужчина', 33),
								('Алена', 'Михайлова', 'женщина', 31),
								('Влад', 'Ярославский', 'мужчина', 45),
								('Юлия', 'Венедиктова', 'женщина', 42)	;
go
insert into Saller(Name, Surname, Position) values
								('Егор', 'Степанян', 'менеджер'),
								('Анфиса', 'Курдюкова', 'менеджер'),
								('Игорь', 'Артемов', 'старший менеджер'),
								('Ирина', 'Васильева', 'менеджер');
go
insert into Oferta(TourId, CustomerId, SallerId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4);
go

/*
/*Задание 1. 
Для базы данных «Спортивный магазин» из практического задания модуля «Триггеры, хранимые процедуры и пользовательские функции» 
создайте следующие пользовательские функции: 
1. Пользовательская функция возвращает количество уникальных покупателей 
2. Пользовательская функция возвращает среднюю цену товара конкретного вида. Вид товара передаётся в качестве параметра. 
Например, среднюю цену обуви 
3. Пользовательская функция возвращает среднюю цену продажи по каждой дате, когда осуществлялись продажи 
4. Пользовательская функция возвращает информацию о последнем проданном товаре. Критерий определения последнего проданного товара: дата продажи 
5. Пользовательская функция возвращает информацию о первом проданном товаре. Критерий определения первого проданного товара: дата продажи
6. Пользовательская функция возвращает информацию о заданном виде товаров конкретного производителя. Вид товара и название производителя 
передаются в качестве параметров 
7. Пользовательская функция возвращает информацию о покупателях, которым в этом году исполнится 45 лет */


--1. Пользовательская функция возвращает количество уникальных покупателей 

create function numberUniqueBuyers()
returns int
as
begin
declare @NumberBuyers int
set @NumberBuyers = (select count(Id) from Clients)				--используется count(Id), т.к. клиенты не повторяются
return @NumberBuyers
end;
go

--проверка

select dbo.numberUniqueBuyers() as 'Количество уникальных покупателей';
go


--2. Пользовательская функция возвращает среднюю цену товара конкретного вида. Вид товара передаётся в качестве параметра. 
--Например, среднюю цену обуви 

create function getAveragePriceOfProducts(@TypeProducts nvarchar(20))
returns money
as
begin
declare @AveragePrice money
set @AveragePrice = (select avg(Price) 
					 from Products join TypeProduct on TypeProduct.Id=Products.TypeProductId
					 group by TypeProduct.Name
					 having TypeProduct.Name=@TypeProducts)
return @AveragePrice
end;
go


--проверка

select dbo.getAveragePriceOfProducts('куртки') as 'Средняя цена товаров данного вида';
go


--3. Пользовательская функция возвращает среднюю цену продажи по каждой дате, когда осуществлялись продажи 

create function getAvaregePriceOnDate()
returns table
as
return (select Sales.DateSales as [Дата], avg(Sales.Number*Products.Price) as [Средняя цена продажи]
		from Products join Sales on Products.Id=Sales.ProductsId
		group by Sales.DateSales);
go

--проверка

select * from getAvaregePriceOnDate();
go


--4. Пользовательская функция возвращает информацию о последнем проданном товаре. Критерий определения последнего проданного товара: дата продажи 

create function infoAboutLastSalesProducts()
returns table
as
return (select Sales.DateSales as [Дата последней продажи], Products.Name as [Последние проданные товары]
		from Products join Sales on Products.Id=Sales.ProductsId
		where Sales.DateSales >= all(select distinct DateSales from Sales)
);
go

--проверка

select * from infoAboutLastSalesProducts();
go


--5. Пользовательская функция возвращает информацию о первом проданном товаре. Критерий определения первого проданного товара: дата продажи

create function infoAboutFirstSalesProducts()
returns table
as
return (select Sales.DateSales as [Дата первой продажи], Products.Name as [Первые проданные товары]
		from Products join Sales on Products.Id=Sales.ProductsId
		where Sales.DateSales <= all(select distinct DateSales from Sales)
);
go

--проверка

select * from infoAboutFirstSalesProducts();
go


--6. Пользовательская функция возвращает информацию о заданном виде товаров конкретного производителя. Вид товара и название производителя 
--передаются в качестве параметров 

create function infoAboutProductsOneManufacturers(@TypeProducts nvarchar(20), @NameManufacturer nvarchar(50))
returns table
as
return (select top(100) Products.Name as [Название товара], TypeProduct.Name as [Вид товара], Products.Price as [Цена товара], Manufacturer.Name as [Производитель]
		from Products join Sales on Products.Id=Sales.ProductsId
							   join Manufacturer on Manufacturer.Id=Products.ManufacturerId
							   join TypeProduct on TypeProduct.Id=Products.TypeProductId
		where TypeProduct.Name = @TypeProducts and Manufacturer.Name = @NameManufacturer
		order by Products.Name);
go


--проверка

select * from infoAboutProductsOneManufacturers('тренажеры', 'Спорттехника');
go


--7. Пользовательская функция возвращает информацию о покупателях, которым в этом году исполнится 45 лет 

create function infoAboutBuyersWhoWill45(@Age int)
returns @infoAboutBuyersTable table
		(Name nvarchar(50), Surname nvarchar(50), BirthDate date) --Email nvarchar(100), Fone nvarchar(20), Gender nvarchar(10), BirthDate date)
as
begin
insert @infoAboutBuyersTable
select Clients.Name as [Имя], Clients.Surname as [Фамилия], Clients.BirthDate as [Дата рождения]     
		from Clients
		where (year(Clients.BirthDate) = year(getdate()) - @Age and month(Clients.BirthDate) = month(getdate()) and day(Clients.BirthDate) > day(getdate())) or
			  (year(Clients.BirthDate) = year(getdate()) - @Age and month(Clients.BirthDate) > month(getdate()))
return 
end;
go


--проверка

select * from infoAboutBuyersWhoWill45(45);
go

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

create database MusicCollection_HW_4;
go

use MusicCollection_HW_4;
go

create table Styles
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) check(Name<>'') not null,
);
go
create table Artists
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) check(Name<>'') not null,
Surname nvarchar(50) check(Surname<>'') not null,
);
go
create table Countries
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) not null,
);
go
create table Publishers
(
Id int identity(1,1) not null primary key,
Name nvarchar(100) check(Name<>'') not null,
CountriesId int not null references Countries(Id),
);
go
create table Discs
(
Id int identity(1,1) not null primary key,
Name nvarchar(50) check(Name<>'') not null,
ReleaseDate date not null,
DurationDisc time check(DurationDisc>'0:0:0') not null,
NumberSongs int not null,
StylesId int not null references Styles(Id),
ArtistsId int not null references Artists(Id),
PublishersId int not null references Publishers(Id),
);
go
create table Songs
(
Id int identity(1,1) not null primary key,
Name nvarchar(MAX) check(Name<>'') not null,
Duration time check(Duration>'0:0:0') not null,
DisksId int not null references Discs(Id) on delete cascade,
StylesId int not null references Styles(Id),
ArtistsId int not null references Artists(Id),
);
go

--Наполним ее данными

insert into Countries(Name) values ('США'), 
								   ('Украина'),
								   ('Великобритания');
go
insert into Publishers(Name, CountriesId) values 
								   ('Epic Records', 1), 
								   ('Первое музыкальное издательство', 2), 
								   ('Lavina Music', 2),
								   ('Susy Records', 2),
								   ('Columbia', 3);
go
insert into Styles (Name) values   ('R&B'),
								   ('ритм-н-блюз'),
								   ('нью диско'),
								   ('инди поп'),
								   ('арт рок'),
								   ('соул'),
								   ('Dark Power Pop');
go
insert into Artists (Name, Surname) values 
								   ('Майкл', 'Джексон'),
								   ('Дмитрий', 'Монатик'),
								   ('Святослав', 'Вакарчук'),
								   ('Адель', 'Эдкинс');
go
insert into Discs (Name, ReleaseDate, DurationDisc, NumberSongs, StylesId, ArtistsId, PublishersId) values
								   ('Invincible', '2001-10-30', '01:17:10', 16, 1, 1, 1),
								   ('Dangerous', '1991-11-26', '01:13:12', 14, 2, 1, 1),
								   ('Thriller', '1982-11-08', '00:42:19', 9, 2, 1, 1),
								   ('Love it ритм', '2019-05-17', '01:01:06', 20, 3, 2, 2),
								   ('Звучит', '2016-05-25', '00:44:03', 16, 4, 2, 2),
								   ('Без меж', '2016-05-19', '00:51:25', 11, 5, 3, 3),
								   ('Без меж', '2019-11-28', '00:43:17', 7, 6, 2, 4),
								   ('Земля', '2013-05-13', '00:48:53', 12, 4, 3, 4),
								   ('21', '2011-01-24', '00:48:12', 11, 6, 4, 5),
								   ('Mong', '2014-05-13', '00:41:43', 11, 6, 4, 5);
go
insert into Songs (Name, Duration, DisksId, StylesId, ArtistsId) values
								   ('Unbreakable', '00:06:26', 1, 1, 1),
								   ('Remember the time', '00:04:00', 2, 2, 1),
								   ('Billie Jean', '00:10:37', 3, 2, 1),
								   ('Глубоко...', '00:04:12', 4, 3, 2),
								   ('Кружит', '00:03:18', 5, 4, 2),
								   ('Віддай мені свою любов', '00:04:04', 6, 5, 3),
								   ('Сідай коло мене - співай', '00:05:12', 7, 6, 2),
								   ('Обійми', '00:03:44', 8, 4, 3),
								   ('Rolling in the Deep', '00:03:46', 9, 6, 4),
								   ('Kontra bas', '00:03:41', 9, 6, 4);
go

/*Задание 2. 
Для базы данных «Музыкальная коллекция» из практического задания модуля «Работа с таблицами и представлениями в MS SQL Server» 
создайте следующие пользовательские функции: 
1. Пользовательская функция возвращает все диски заданного года. Год передаётся в качестве параметра 
2. Пользовательская функция возвращает информацию о дисках с одинаковым названием альбома, но разными исполнителями 
3. Пользовательская функция возвращает информацию о всех песнях, в чьем названии встречается заданное слово. Слово передаётся в качестве параметра 
4. Пользовательская функция возвращает количество альбомов в стилях ритм-н-блюз и соул  
5. Пользовательская функция возвращает информацию о средней длительности песни заданного исполнителя. Название исполнителя передаётся в качестве параметра 
6. Пользовательская функция возвращает информацию о самой долгой и самой короткой песне 
7. Пользовательская функция возвращает информацию об исполнителях, которые создали альбомы в двух и более стилях.*/


--1. Пользовательская функция возвращает все диски заданного года. Год передаётся в качестве параметра 

create function getDiscsOfYear(@YearDiscs int)
returns table
as 
return (select Artists.Name+' '+Artists.Surname as [Исполнитель], Discs.Name as [Название альбома/диска], Discs.ReleaseDate as [Дата выпуска], 
Discs.NumberSongs as [Количество песен], Discs.DurationDisc as [Продолжительность диска], Publishers.Name as [Издательство]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
		where year(Discs.ReleaseDate) = @YearDiscs);
go

--проверка

select * from getDiscsOfYear(2016);
go


--2. Пользовательская функция возвращает информацию о дисках с одинаковым названием альбома, но разными исполнителями 

create function infoAboutDiscsSameTitlesDifferentArtists()
returns table
as
return (select Artists.Name+' '+Artists.Surname as [Исполнитель], Discs.Name as [Название альбома/диска], Discs.ReleaseDate as [Дата выпуска], 
Discs.NumberSongs as [Количество песен], Discs.DurationDisc as [Продолжительность диска], Publishers.Name as [Издательство]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
		where Discs.Name in (select Name 
							 from Discs
							 group by Name
							 having count(Name) > 1));
go

--проверка

select * from infoAboutDiscsSameTitlesDifferentArtists();
go


--3. Пользовательская функция возвращает информацию о всех песнях, в чьем названии встречается заданное слово. Слово передаётся в качестве параметра 

create function infoAboutSongsWithWord (@Word nvarchar(20))
returns table
as
return (select Songs.Name as [Название песни], Artists.Name+' '+Artists.Surname as [Исполнитель], Discs.Name as [Название альбома/диска], 
Songs.Duration as [Продолжительность трека], Styles.Name as [Стиль исполнения]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Songs.Name like convert(nvarchar(1), '%') + @Word + convert(nvarchar(1), '%'));
go

--проверка

select * from infoAboutSongsWithWord('ко');
go


--4. Пользовательская функция возвращает количество альбомов в стилях ритм-н-блюз и соул  

create function numberDiscsInStylesRnbAndSoul()
returns  int
as
begin
declare @NumberDiscs int
set @NumberDiscs = (select count(Discs.Id) 
					from Discs join Styles on Styles.Id=Discs.StylesId
					where Styles.Name in ('ритм-н-блюз', 'соул'))
return @NumberDiscs
end;
go

--проверка

select dbo.numberDiscsInStylesRnbAndSoul() as [Количество альбомов в стилях ритм-н-блюз и соул];
go


--5. Пользовательская функция возвращает информацию о средней длительности песни заданного исполнителя. Название исполнителя передаётся в качестве параметра 

create function infoAboutAvgDurationSongs(@NameArtists nvarchar(20), @SurnameArtists nvarchar(20))
returns time
as
begin
declare @AvgDurationSongs time
--сначала преобразование во float, поиск среднего значения, затем преобразование назад в time
set @AvgDurationSongs = (select cast(cast(avg(cast(cast(Songs.Duration as datetime) as float)) as datetime) as time(1))   
						 from Songs join Artists on Artists.Id=Songs.ArtistsId
						 where Artists.Name=@NameArtists and Artists.Surname=@SurnameArtists)
return @AvgDurationSongs
end;
go

--проверка

select dbo.infoAboutAvgDurationSongs('Майкл', 'Джексон') as [Средняя длительность песни исполнителя];
go


--6. Пользовательская функция возвращает информацию о самой долгой и самой короткой песне 

create function infoAboutLongestAndShortestSongs()
returns table
as
return (select Songs.Name as [Название песни], Artists.Name+' '+Artists.Surname as [Исполнитель], Discs.Name as [Название альбома/диска], 
Songs.Duration as [Продолжительность трека], Styles.Name as [Стиль исполнения]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Songs.Duration = (select max(Duration) from Songs) or Songs.Duration = (select min(Duration) from Songs));
go

--проверка

select * from infoAboutLongestAndShortestSongs();
go


--7. Пользовательская функция возвращает информацию об исполнителях, которые создали альбомы в двух и более стилях.

create function infoAboutArtisrsWithMultyStyles()
returns table
as
return (select Songs.Name as [Название песни], Artists.Name+' '+Artists.Surname as [Исполнитель], Discs.Name as [Название альбома/диска], 
Songs.Duration as [Продолжительность трека], Styles.Name as [Стиль исполнения]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Artists.Surname in (select Artists.Surname 
								  from Artists join Discs on Artists.Id=Discs.ArtistsId
								  group by Artists.Surname
								  having count(distinct Discs.StylesId)>1));
go

--проверка

select * from infoAboutArtisrsWithMultyStyles();
go
*/