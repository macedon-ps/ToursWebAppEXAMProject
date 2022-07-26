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
Name nvarchar(50) check(Name<>'') default('������') not null,
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
Position nvarchar(100) default('���������') not null, 
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
--�������� �� �������

insert into Product(Name, Description) values
								('��������� ����', '������ ������� �������� ������ ����, ��������� ��� ���� ���� ����������� � �������������� �����, �� ������ ��� � ������� �����������, ��������� ���� ����� ��� �����, �� ������������ �� �����. �� ����������� � ������� ������ �� ������ ������� ����������� �������� ����� ��������� �������� ������ � ������ � ��������� ���������� �������. ��� ������� ������� ������ ����� ���������� ������ � ����� (�׸���� ������), ������� ����������� �����, ��� ������� � ���� ���������� ��� ����� ���������� �����, � � ����������������� �������, ���������� ������ � �����, ������������ ��� ������� ����, ���� ������ � ��������� ��������.'),
								('������� ������', '����������� ����� � ��� ������������ ����� � ���������� �������, � �������, ������� ���������� ����� ��������� �������. �� ��� ��� �������� ���������� �������. � ���������������� ����� �� ������ �������. ���� ����� ������� � ����������� ���������������, ��������� ������, ���������� ���������. � �������� ����� ������ ������������� ����. ���������� ��������� ������ ��������� �������� �� ��������� ������� ���� � ����� ����� ����������� �������. ����� ������� ������������� ����� ���������� 1600 ��2. ���������� ����������� �� 150 �� � ��� �� ����� � �� 30 �� � ������ �� ������.'),
								('���� ������ ����', '������ ����, ����������� �������� ������� � ���� �������� - ���������. �� ���������� ��������� ����������� ��������� ��������� ������, �������, �������, ������. ����� �� ������������� � ���������� ������������, ����������� ��������������� ������ � ������������ ��������� �������� �������. ��������� ������ � ���� ������������ �������. �������� �� ������� ��������� ���������� ��������, ������� � ������ �������� �� ������� ������ ������� ����� ������� ������ ������.'),
								('����� ������� � �����', '������� � ���������� �����, ��, ������� ��������� ������, �� ������� ������. ��� � ��� ����������, ������������� � ������� ����, �� ���������� �����������. � ��� �����, ��������� ����������� ������������ ����� ������� � ����� � ����� � ������������, � ������������� ������� �� ��� �����, � � ������ � ���� ����. ������� (Istanbul) � ���������� ����� ������ � ���� �� ����� ������� ������� ����, ������� ����, ������� �����������-�������� � ���������� ����� ������. ������ ������� �������, ������������, ��������� � ��������� �������. ���������� �� ������� ������� ������. �������� ����� ������ ��������� � ������, ������� � � ����. ����������� �����, � ���� �������, ������� �� ��� ����� ������ ������� ���. ��������� � ����������� ����� ��������� �������� ������, ����� ������� �������� �������� ����. � �������� � ��������� ���������� ��������� ����� 13 ���. �������. ');
go
insert into Country (Name) values
								('������'),
								('�������'),
								('���������'),
								('������');
go
insert into City (Name, CountryId) values
								('����', 1),
								('������', 2),
								('�����', 3),
								('�������', 4);
go
insert into Hotel (Name, LevelHotel, CityId) values
								('����������', 4, 1),
								('����� � �������', 3, 2),
								('����������', 4, 3),
								('���������� ������', 5, 4);
go
insert into Location (CountryId, CityId, HotelId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4);
go
insert into Food (ModeOfEating) values
								('������ �������'),
								('������ ����'),
								('������ ����'),
								('������ ������� � ����'),
								('������ ������� � ����'),
								('�������, ���� � ����');
go
insert into DateTour(DateStart, DateEnd, NumberOfDays, NumberOfNights) values
								('2201-01-01 00:00:00.000', '2201-01-08 00:00:00.000', 7, 7),
								('2201-01-01 00:00:00.000', '2201-01-02 00:00:00.000', 1, 1),
								('2201-01-02 00:00:00.000', '2201-01-04 00:00:00.000', 2, 2),
								('2201-01-03 00:00:00.000', '2201-01-06 00:00:00.000', 3, 3);
go
insert into Tour (Name, ProductId, DateTourId, LocationId, FoodId) values
								('��� �� ������', 1, 1, 1, 5),
								('��� �� �������', 2, 2, 2, 1),
								('��� �� ���������', 3, 3, 3, 6),
								('��� �� ������', 4, 4, 4, 4);
go
insert into Customer(Name, Surname, Gender, Age) values
								('�����', '��������', '�������', 33),
								('�����', '���������', '�������', 31),
								('����', '�����������', '�������', 45),
								('����', '�����������', '�������', 42)	;
go
insert into Saller(Name, Surname, Position) values
								('����', '��������', '��������'),
								('������', '���������', '��������'),
								('�����', '�������', '������� ��������'),
								('�����', '���������', '��������');
go
insert into Oferta(TourId, CustomerId, SallerId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4);
go

/*
/*������� 1. 
��� ���� ������ ����������� ������� �� ������������� ������� ������ ���������, �������� ��������� � ���������������� ������� 
�������� ��������� ���������������� �������: 
1. ���������������� ������� ���������� ���������� ���������� ����������� 
2. ���������������� ������� ���������� ������� ���� ������ ����������� ����. ��� ������ ��������� � �������� ���������. 
��������, ������� ���� ����� 
3. ���������������� ������� ���������� ������� ���� ������� �� ������ ����, ����� �������������� ������� 
4. ���������������� ������� ���������� ���������� ���������� ��������� ������. �������� ����������� ���������� ���������� ������: ���� ������� 
5. ���������������� ������� ���������� ���������� ������� ��������� ������. �������� ����������� ������� ���������� ������: ���� �������
6. ���������������� ������� ���������� ���������� ��������� ���� ������� ����������� �������������. ��� ������ � �������� ������������� 
���������� � �������� ���������� 
7. ���������������� ������� ���������� ���������� ������������, ������� � ���� ���� ���������� 45 ��� */


--1. ���������������� ������� ���������� ���������� ���������� ����������� 

create function numberUniqueBuyers()
returns int
as
begin
declare @NumberBuyers int
set @NumberBuyers = (select count(Id) from Clients)				--������������ count(Id), �.�. ������� �� �����������
return @NumberBuyers
end;
go

--��������

select dbo.numberUniqueBuyers() as '���������� ���������� �����������';
go


--2. ���������������� ������� ���������� ������� ���� ������ ����������� ����. ��� ������ ��������� � �������� ���������. 
--��������, ������� ���� ����� 

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


--��������

select dbo.getAveragePriceOfProducts('������') as '������� ���� ������� ������� ����';
go


--3. ���������������� ������� ���������� ������� ���� ������� �� ������ ����, ����� �������������� ������� 

create function getAvaregePriceOnDate()
returns table
as
return (select Sales.DateSales as [����], avg(Sales.Number*Products.Price) as [������� ���� �������]
		from Products join Sales on Products.Id=Sales.ProductsId
		group by Sales.DateSales);
go

--��������

select * from getAvaregePriceOnDate();
go


--4. ���������������� ������� ���������� ���������� ���������� ��������� ������. �������� ����������� ���������� ���������� ������: ���� ������� 

create function infoAboutLastSalesProducts()
returns table
as
return (select Sales.DateSales as [���� ��������� �������], Products.Name as [��������� ��������� ������]
		from Products join Sales on Products.Id=Sales.ProductsId
		where Sales.DateSales >= all(select distinct DateSales from Sales)
);
go

--��������

select * from infoAboutLastSalesProducts();
go


--5. ���������������� ������� ���������� ���������� ������� ��������� ������. �������� ����������� ������� ���������� ������: ���� �������

create function infoAboutFirstSalesProducts()
returns table
as
return (select Sales.DateSales as [���� ������ �������], Products.Name as [������ ��������� ������]
		from Products join Sales on Products.Id=Sales.ProductsId
		where Sales.DateSales <= all(select distinct DateSales from Sales)
);
go

--��������

select * from infoAboutFirstSalesProducts();
go


--6. ���������������� ������� ���������� ���������� ��������� ���� ������� ����������� �������������. ��� ������ � �������� ������������� 
--���������� � �������� ���������� 

create function infoAboutProductsOneManufacturers(@TypeProducts nvarchar(20), @NameManufacturer nvarchar(50))
returns table
as
return (select top(100) Products.Name as [�������� ������], TypeProduct.Name as [��� ������], Products.Price as [���� ������], Manufacturer.Name as [�������������]
		from Products join Sales on Products.Id=Sales.ProductsId
							   join Manufacturer on Manufacturer.Id=Products.ManufacturerId
							   join TypeProduct on TypeProduct.Id=Products.TypeProductId
		where TypeProduct.Name = @TypeProducts and Manufacturer.Name = @NameManufacturer
		order by Products.Name);
go


--��������

select * from infoAboutProductsOneManufacturers('���������', '������������');
go


--7. ���������������� ������� ���������� ���������� ������������, ������� � ���� ���� ���������� 45 ��� 

create function infoAboutBuyersWhoWill45(@Age int)
returns @infoAboutBuyersTable table
		(Name nvarchar(50), Surname nvarchar(50), BirthDate date) --Email nvarchar(100), Fone nvarchar(20), Gender nvarchar(10), BirthDate date)
as
begin
insert @infoAboutBuyersTable
select Clients.Name as [���], Clients.Surname as [�������], Clients.BirthDate as [���� ��������]     
		from Clients
		where (year(Clients.BirthDate) = year(getdate()) - @Age and month(Clients.BirthDate) = month(getdate()) and day(Clients.BirthDate) > day(getdate())) or
			  (year(Clients.BirthDate) = year(getdate()) - @Age and month(Clients.BirthDate) > month(getdate()))
return 
end;
go


--��������

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

--�������� �� �������

insert into Countries(Name) values ('���'), 
								   ('�������'),
								   ('��������������');
go
insert into Publishers(Name, CountriesId) values 
								   ('Epic Records', 1), 
								   ('������ ����������� ������������', 2), 
								   ('Lavina Music', 2),
								   ('Susy Records', 2),
								   ('Columbia', 3);
go
insert into Styles (Name) values   ('R&B'),
								   ('����-�-����'),
								   ('��� �����'),
								   ('���� ���'),
								   ('��� ���'),
								   ('����'),
								   ('Dark Power Pop');
go
insert into Artists (Name, Surname) values 
								   ('�����', '�������'),
								   ('�������', '�������'),
								   ('���������', '��������'),
								   ('�����', '������');
go
insert into Discs (Name, ReleaseDate, DurationDisc, NumberSongs, StylesId, ArtistsId, PublishersId) values
								   ('Invincible', '2001-10-30', '01:17:10', 16, 1, 1, 1),
								   ('Dangerous', '1991-11-26', '01:13:12', 14, 2, 1, 1),
								   ('Thriller', '1982-11-08', '00:42:19', 9, 2, 1, 1),
								   ('Love it ����', '2019-05-17', '01:01:06', 20, 3, 2, 2),
								   ('������', '2016-05-25', '00:44:03', 16, 4, 2, 2),
								   ('��� ���', '2016-05-19', '00:51:25', 11, 5, 3, 3),
								   ('��� ���', '2019-11-28', '00:43:17', 7, 6, 2, 4),
								   ('�����', '2013-05-13', '00:48:53', 12, 4, 3, 4),
								   ('21', '2011-01-24', '00:48:12', 11, 6, 4, 5),
								   ('Mong', '2014-05-13', '00:41:43', 11, 6, 4, 5);
go
insert into Songs (Name, Duration, DisksId, StylesId, ArtistsId) values
								   ('Unbreakable', '00:06:26', 1, 1, 1),
								   ('Remember the time', '00:04:00', 2, 2, 1),
								   ('Billie Jean', '00:10:37', 3, 2, 1),
								   ('�������...', '00:04:12', 4, 3, 2),
								   ('������', '00:03:18', 5, 4, 2),
								   ('³���� ��� ���� �����', '00:04:04', 6, 5, 3),
								   ('ѳ��� ���� ���� - �����', '00:05:12', 7, 6, 2),
								   ('�����', '00:03:44', 8, 4, 3),
								   ('Rolling in the Deep', '00:03:46', 9, 6, 4),
								   ('Kontra bas', '00:03:41', 9, 6, 4);
go

/*������� 2. 
��� ���� ������ ������������ ���������� �� ������������� ������� ������ ������� � ��������� � ��������������� � MS SQL Server� 
�������� ��������� ���������������� �������: 
1. ���������������� ������� ���������� ��� ����� ��������� ����. ��� ��������� � �������� ��������� 
2. ���������������� ������� ���������� ���������� ������� � ���������� ��������� �������, �� ������� ������������� 
3. ���������������� ������� ���������� ���������� � ���� ������, � ���� �������� ����������� �������� �����. ����� ��������� � �������� ��������� 
4. ���������������� ������� ���������� ���������� �������� � ������ ����-�-���� � ����  
5. ���������������� ������� ���������� ���������� � ������� ������������ ����� ��������� �����������. �������� ����������� ��������� � �������� ��������� 
6. ���������������� ������� ���������� ���������� ������ ������ � ����� �������� ����� 
7. ���������������� ������� ���������� ���������� �� ������������, ������� ������� ������� � ���� � ����� ������.*/


--1. ���������������� ������� ���������� ��� ����� ��������� ����. ��� ��������� � �������� ��������� 

create function getDiscsOfYear(@YearDiscs int)
returns table
as 
return (select Artists.Name+' '+Artists.Surname as [�����������], Discs.Name as [�������� �������/�����], Discs.ReleaseDate as [���� �������], 
Discs.NumberSongs as [���������� �����], Discs.DurationDisc as [����������������� �����], Publishers.Name as [������������]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
		where year(Discs.ReleaseDate) = @YearDiscs);
go

--��������

select * from getDiscsOfYear(2016);
go


--2. ���������������� ������� ���������� ���������� ������� � ���������� ��������� �������, �� ������� ������������� 

create function infoAboutDiscsSameTitlesDifferentArtists()
returns table
as
return (select Artists.Name+' '+Artists.Surname as [�����������], Discs.Name as [�������� �������/�����], Discs.ReleaseDate as [���� �������], 
Discs.NumberSongs as [���������� �����], Discs.DurationDisc as [����������������� �����], Publishers.Name as [������������]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
		where Discs.Name in (select Name 
							 from Discs
							 group by Name
							 having count(Name) > 1));
go

--��������

select * from infoAboutDiscsSameTitlesDifferentArtists();
go


--3. ���������������� ������� ���������� ���������� � ���� ������, � ���� �������� ����������� �������� �����. ����� ��������� � �������� ��������� 

create function infoAboutSongsWithWord (@Word nvarchar(20))
returns table
as
return (select Songs.Name as [�������� �����], Artists.Name+' '+Artists.Surname as [�����������], Discs.Name as [�������� �������/�����], 
Songs.Duration as [����������������� �����], Styles.Name as [����� ����������]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Songs.Name like convert(nvarchar(1), '%') + @Word + convert(nvarchar(1), '%'));
go

--��������

select * from infoAboutSongsWithWord('��');
go


--4. ���������������� ������� ���������� ���������� �������� � ������ ����-�-���� � ����  

create function numberDiscsInStylesRnbAndSoul()
returns  int
as
begin
declare @NumberDiscs int
set @NumberDiscs = (select count(Discs.Id) 
					from Discs join Styles on Styles.Id=Discs.StylesId
					where Styles.Name in ('����-�-����', '����'))
return @NumberDiscs
end;
go

--��������

select dbo.numberDiscsInStylesRnbAndSoul() as [���������� �������� � ������ ����-�-���� � ����];
go


--5. ���������������� ������� ���������� ���������� � ������� ������������ ����� ��������� �����������. �������� ����������� ��������� � �������� ��������� 

create function infoAboutAvgDurationSongs(@NameArtists nvarchar(20), @SurnameArtists nvarchar(20))
returns time
as
begin
declare @AvgDurationSongs time
--������� �������������� �� float, ����� �������� ��������, ����� �������������� ����� � time
set @AvgDurationSongs = (select cast(cast(avg(cast(cast(Songs.Duration as datetime) as float)) as datetime) as time(1))   
						 from Songs join Artists on Artists.Id=Songs.ArtistsId
						 where Artists.Name=@NameArtists and Artists.Surname=@SurnameArtists)
return @AvgDurationSongs
end;
go

--��������

select dbo.infoAboutAvgDurationSongs('�����', '�������') as [������� ������������ ����� �����������];
go


--6. ���������������� ������� ���������� ���������� ������ ������ � ����� �������� ����� 

create function infoAboutLongestAndShortestSongs()
returns table
as
return (select Songs.Name as [�������� �����], Artists.Name+' '+Artists.Surname as [�����������], Discs.Name as [�������� �������/�����], 
Songs.Duration as [����������������� �����], Styles.Name as [����� ����������]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Songs.Duration = (select max(Duration) from Songs) or Songs.Duration = (select min(Duration) from Songs));
go

--��������

select * from infoAboutLongestAndShortestSongs();
go


--7. ���������������� ������� ���������� ���������� �� ������������, ������� ������� ������� � ���� � ����� ������.

create function infoAboutArtisrsWithMultyStyles()
returns table
as
return (select Songs.Name as [�������� �����], Artists.Name+' '+Artists.Surname as [�����������], Discs.Name as [�������� �������/�����], 
Songs.Duration as [����������������� �����], Styles.Name as [����� ����������]
		from Discs join Artists on Artists.Id=Discs.ArtistsId
				   join Publishers on Publishers.Id=Discs.PublishersId
				   join Songs on Discs.Id=Songs.DisksId
				   join Styles on Styles.Id=Songs.StylesId
		where Artists.Surname in (select Artists.Surname 
								  from Artists join Discs on Artists.Id=Discs.ArtistsId
								  group by Artists.Surname
								  having count(distinct Discs.StylesId)>1));
go

--��������

select * from infoAboutArtisrsWithMultyStyles();
go
*/