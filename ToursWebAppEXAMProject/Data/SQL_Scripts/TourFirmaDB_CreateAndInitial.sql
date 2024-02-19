create database TourFirmaDB;
go


/*use TourFirmaDB;
go
*/

create table Product
(
Id int identity(1,1) primary key,
Name nvarchar(50) check (Name<>'') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') not null,
FullDescription nvarchar(max) check (FullDescription<>'') not null,
DateAdded datetime not null
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
BirthDay datetime not null,
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
create table Offer
(
Id int identity(1,1) not null primary key,
CustomerId int not null foreign key references Customer(Id),
SallerId int not null foreign key references Saller(Id),
TourId int not null foreign key references Tour(Id),
);
go
--заполним ее данными

insert into Product(Name, ShortDescription, FullDescription) values
								('Жемчужина Нила', 'Египет - «дар Нила»', 'Египет принято называть «даром Нила», поскольку без реки этой плодородной и густонаселённой земли, не говоря уже о великой цивилизации, возникшей пять тысяч лет назад, не существовало бы вовсе. На своеобразие и историю страны во многом повлиял разительный контраст между изобилием нильской Долины и Дельты и скудостью окружающей пустыни. Для древних египтян именно здесь находилась родина – Кемет («Чёрная земля»), богатая плодородным Нилом, где природа и люди процветали под сенью милостивых богов, – в противоположность пустыне, воплощению смерти и хаоса, находившейся под властью Сета, бога ветров и стихийных бедствий.'),
								('Пустыня Европы', 'Олешковские пески - пустыня в Европе', 'Олешковские пески – это удивительное место в Херсонской области, в Украине, которое напоминает собой настоящую пустыню. За это его прозвали украинской Сахарой. В действительности здесь не только пустыня. Есть также участки с травянистой растительностью, сосновыми борами, камышевыми зарослями. А окружают парки густые искусственные леса. Уникальный природный объект находится недалеко от побережья Черного моря в южной части материковой Украины. Общая площадь национального парка составляет 1600 км2. Территория растянулась на 150 км с юга на север и на 30 км с запада на восток.'),
								('Край тысячи озер', 'Финляндия - страна тысячи озер', 'Страна озер, невероятной северной природы и край мастеров - Финляндия. На территории Финляндии сохранились старинные поселения финнов, карелов, поморов, вепсов. Здесь вы познакомитесь с уникальной архитектурой, насладитесь притягательными видами и проникнитесь культурой коренных народов. Финляндия хранит в себе многовековую историю. Раскрыть ее помогут старинные деревянные строения, каньоны и водные прогулки по озерным шхерам добавят ярких штрихов вашему отдыху.'),
								('Между Европой и Азией', 'Стамбул - мост между Европой и Азией', 'Стамбул – крупнейший город, но, вопреки расхожему мнению, не столица Турции. Как и все мегаполисы, расположенные у большой воды, он невероятно фотогеничен. В том числе, благодаря уникальному расположению между Европой и Азией – город и символически, и географически поделен на две части, и у каждой – свое лицо. Стамбул (Istanbul) – крупнейший город Турции и один из самых больших городов мира, морской порт, крупный промышленно-торговый и культурный центр Турции. Бывшая столица Римской, Византийской, Латинской и Османской империй. Расположен на берегах пролива Босфор. Основная часть города находится в Европе, меньшая – в Азии. Европейская часть, в свою очередь, делится на две части бухтой Золотой Рог. Азиатская и европейская части разделены проливом Босфор, через который построен вантовый мост. В Стамбуле и ближайших пригородах проживает более 13 млн. человек. '),
								('Среди туманов Альбиона', 'Лондон - столица туманного Альбиона', 'Лондон - столица Великобритании. Город, о котором сложно сказать что-то новое: Биг-Бен и Тауэр, Британская корона и красные кители гвардии, телефонные будки и даблдекеры, Бейкер-стрит и Лондонский глаз, флаг Юнион Джек, в конце концов. За туманами столицы альбиона скрыты уютные чаепития и громкие посиделки в пабах, уютные улицы, по которым гуляли The Beatles, и рев болельщиков на футбольных стадионах. А еще - крупные международные корпорации, политические организации и финансовые биржи. Лондон - законодатель мод и похититель сердец, оставаться равнодушным к которому просто невозможно.');
go
insert into Country (Name) values
								('Египет'),
								('Украина'),
								('Финляндия'),
								('Турция'),
								('Англия');
go
insert into City (Name, CountryId) values
								('Каир', 1),
								('Херсон', 2),
								('Турку', 3),
								('Стамбул', 4),
								('Лондон', 5);
go
insert into Hotel (Name, LevelHotel, CityId) values
								('ГрандОтель', 4, 1),
								('Приют в пустыне', 3, 2),
								('ТуркуЮнион', 4, 3),
								('Султанский дворец', 5, 4),
								('Notting Hill Gate Hotel', 4, 5);
go
insert into Location (CountryId, CityId, HotelId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4),
								(5, 5, 5);
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
								('2022-01-01 00:00:00.000', '2022-01-08 00:00:00.000', 7, 7),
								('2022-01-01 00:00:00.000', '2022-01-02 00:00:00.000', 1, 1),
								('2022-01-02 00:00:00.000', '2022-01-04 00:00:00.000', 2, 2),
								('2022-01-03 00:00:00.000', '2022-01-06 00:00:00.000', 3, 3),
								('2022-01-05 00:00:00.000', '2022-01-09 00:00:00.000', 4, 4);
go
insert into Tour (Name, ProductId, DateTourId, LocationId, FoodId) values
								('Тур по Египту', 1, 1, 1, 5),
								('Тур по Украине', 2, 2, 2, 1),
								('Тур по Финляндии', 3, 3, 3, 6),
								('Тур по Турции', 4, 4, 4, 4),
								('Тур по Англии', 5, 5, 5, 3);
go
insert into Customer(Name, Surname, Gender, BirthDay) values
								('Семен', 'Степанов', 'мужчина', '2000-12-03'),
								('Алена', 'Михайлова', 'женщина', '1997-04-16'),
								('Влад', 'Ярославский', 'мужчина', '1986-06-21'),
								('Юлия', 'Венедиктова', 'женщина', '1995-09-11'),
								('Николай', 'Половинкин', 'мужчина', '2003-02-27');
go
insert into Saller(Name, Surname, Position) values
								('Егор', 'Степанян', 'менеджер'),
								('Анфиса', 'Курдюкова', 'менеджер'),
								('Игорь', 'Артемов', 'старший менеджер'),
								('Ирина', 'Васильева', 'менеджер');
go
insert into Offer(TourId, CustomerId, SallerId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4),
								(5, 5, 3);
go

