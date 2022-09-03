use TourFirmaDB;
go

create table Blog
(
Id int identity(1,1) primary key,
Title nvarchar(50) check (Title<>'') default('Заголовок блога') not null,
Message nvarchar(50) check (Message<>'') default('Сообщение участника блога'),
FullMessageLine nvarchar(max) check (FullMessageLine<>'') default('Полная строка сообщений'),
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime
);
go
create table New
(
Id int identity(1,1) primary key,
Title nvarchar(50) check (Title<>'') default('Заголовок новости') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('Краткое орисание новости') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('Полное описание новости') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime
);
go

--заполним ее данными

insert into Blog(Title) values
								('Поговорим о C#'),
								('Поговорим о Javascript'),
								('Поговорим о MS SQL'),
								('Поговорим о Сетевом программировании');
								
insert into New (Title) values
								('Новость 1'),
								('Новость 2'),
								('Новость 3'),
								('Новость 4'),
								('Новость 5');