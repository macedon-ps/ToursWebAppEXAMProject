use TourFirmaDB;
go

create table Blog
(
Id int identity(1,1) primary key not null,
Name nvarchar(50) check (Name<>'') default('Заголовок блога') not null,
Message nvarchar(200) check (Message<>'') default('Сообщение') not null,
FullMessageLine nvarchar(max) check (FullMessageLine<>'') default('Вся строка сообщений') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('Краткое описание темы блога') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('Полное описание темы блога') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime not null
);
go
create table New
(
Id int identity(1,1) primary key not null,
Name nvarchar(50) check (Name<>'') default('Заголовок новости') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('Краткое описание новости') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('Полное описание новости') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime not null
);
go

--заполним ее данными

insert into Blog(Name) values
								('Поговорим о C#'),
								('Поговорим об Android'),
								('Поговорим о Javascript'),
								('Поговорим о MS SQL'),
								('Поговорим о Сетевом программировании');
go								
insert into New (Name) values
								('Новость 1'),
								('Новость 2'),
								('Новость 3'),
								('Новость 4'),
								('Новость 5');
go