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
BirthDay datetime not null,
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
create table Offer
(
Id int identity(1,1) not null primary key,
CustomerId int not null foreign key references Customer(Id),
SallerId int not null foreign key references Saller(Id),
TourId int not null foreign key references Tour(Id),
);
go
--�������� �� �������

insert into Product(Name, ShortDescription, FullDescription) values
								('��������� ����', '������ - ���� ����', '������ ������� �������� ������ ����, ��������� ��� ���� ���� ����������� � �������������� �����, �� ������ ��� � ������� �����������, ��������� ���� ����� ��� �����, �� ������������ �� �����. �� ����������� � ������� ������ �� ������ ������� ����������� �������� ����� ��������� �������� ������ � ������ � ��������� ���������� �������. ��� ������� ������� ������ ����� ���������� ������ � ����� (�׸���� ������), ������� ����������� �����, ��� ������� � ���� ���������� ��� ����� ���������� �����, � � ����������������� �������, ���������� ������ � �����, ������������ ��� ������� ����, ���� ������ � ��������� ��������.'),
								('������� ������', '����������� ����� - ������� � ������', '����������� ����� � ��� ������������ ����� � ���������� �������, � �������, ������� ���������� ����� ��������� �������. �� ��� ��� �������� ���������� �������. � ���������������� ����� �� ������ �������. ���� ����� ������� � ����������� ���������������, ��������� ������, ���������� ���������. � �������� ����� ������ ������������� ����. ���������� ��������� ������ ��������� �������� �� ��������� ������� ���� � ����� ����� ����������� �������. ����� ������� ������������� ����� ���������� 1600 ��2. ���������� ����������� �� 150 �� � ��� �� ����� � �� 30 �� � ������ �� ������.'),
								('���� ������ ����', '��������� - ������ ������ ����', '������ ����, ����������� �������� ������� � ���� �������� - ���������. �� ���������� ��������� ����������� ��������� ��������� ������, �������, �������, ������. ����� �� ������������� � ���������� ������������, ����������� ��������������� ������ � ������������ ��������� �������� �������. ��������� ������ � ���� ������������ �������. �������� �� ������� ��������� ���������� ��������, ������� � ������ �������� �� ������� ������ ������� ����� ������� ������ ������.'),
								('����� ������� � �����', '������� - ���� ����� ������� � �����', '������� � ���������� �����, ��, ������� ��������� ������, �� ������� ������. ��� � ��� ����������, ������������� � ������� ����, �� ���������� �����������. � ��� �����, ��������� ����������� ������������ ����� ������� � ����� � ����� � ������������, � ������������� ������� �� ��� �����, � � ������ � ���� ����. ������� (Istanbul) � ���������� ����� ������ � ���� �� ����� ������� ������� ����, ������� ����, ������� �����������-�������� � ���������� ����� ������. ������ ������� �������, ������������, ��������� � ��������� �������. ���������� �� ������� ������� ������. �������� ����� ������ ��������� � ������, ������� � � ����. ����������� �����, � ���� �������, ������� �� ��� ����� ������ ������� ���. ��������� � ����������� ����� ��������� �������� ������, ����� ������� �������� �������� ����. � �������� � ��������� ���������� ��������� ����� 13 ���. �������. '),
								('����� ������� ��������', '������ - ������� ��������� ��������', '������ - ������� ��������������. �����, � ������� ������ ������� ���-�� �����: ���-��� � �����, ���������� ������ � ������� ������ �������, ���������� ����� � ����������, ������-����� � ���������� ����, ���� ����� ����, � ����� ������. �� �������� ������� �������� ������ ������ �������� � ������� ��������� � �����, ������ �����, �� ������� ������ The Beatles, � ��� ����������� �� ���������� ���������. � ��� - ������� ������������� ����������, ������������ ����������� � ���������� �����. ������ - ������������ ��� � ���������� ������, ���������� ����������� � �������� ������ ����������.');
go
insert into Country (Name) values
								('������'),
								('�������'),
								('���������'),
								('������'),
								('������');
go
insert into City (Name, CountryId) values
								('����', 1),
								('������', 2),
								('�����', 3),
								('�������', 4),
								('������', 5);
go
insert into Hotel (Name, LevelHotel, CityId) values
								('����������', 4, 1),
								('����� � �������', 3, 2),
								('����������', 4, 3),
								('���������� ������', 5, 4),
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
								('������ �������'),
								('������ ����'),
								('������ ����'),
								('������ ������� � ����'),
								('������ ������� � ����'),
								('�������, ���� � ����');
go
insert into DateTour(DateStart, DateEnd, NumberOfDays, NumberOfNights) values
								('2022-01-01 00:00:00.000', '2022-01-08 00:00:00.000', 7, 7),
								('2022-01-01 00:00:00.000', '2022-01-02 00:00:00.000', 1, 1),
								('2022-01-02 00:00:00.000', '2022-01-04 00:00:00.000', 2, 2),
								('2022-01-03 00:00:00.000', '2022-01-06 00:00:00.000', 3, 3),
								('2022-01-05 00:00:00.000', '2022-01-09 00:00:00.000', 4, 4);
go
insert into Tour (Name, ProductId, DateTourId, LocationId, FoodId) values
								('��� �� ������', 1, 1, 1, 5),
								('��� �� �������', 2, 2, 2, 1),
								('��� �� ���������', 3, 3, 3, 6),
								('��� �� ������', 4, 4, 4, 4),
								('��� �� ������', 5, 5, 5, 3);
go
insert into Customer(Name, Surname, Gender, BirthDay) values
								('�����', '��������', '�������', '2000-12-03'),
								('�����', '���������', '�������', '1997-04-16'),
								('����', '�����������', '�������', '1986-06-21'),
								('����', '�����������', '�������', '1995-09-11'),
								('�������', '����������', '�������', '2003-02-27');
go
insert into Saller(Name, Surname, Position) values
								('����', '��������', '��������'),
								('������', '���������', '��������'),
								('�����', '�������', '������� ��������'),
								('�����', '���������', '��������');
go
insert into Offer(TourId, CustomerId, SallerId) values
								(1, 1, 1),
								(2, 2, 2),
								(3, 3, 3),
								(4, 4, 4),
								(5, 5, 3);
go

