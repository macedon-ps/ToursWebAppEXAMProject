use TourFirmaDB;
go

create table Blog
(
Id int identity(1,1) primary key not null,
Name nvarchar(50) check (Name<>'') default('��������� �����') not null,
Message nvarchar(200) check (Message<>'') default('���������') not null,
FullMessageLine nvarchar(max) check (FullMessageLine<>'') default('��� ������ ���������') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('������� �������� ���� �����') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('������ �������� ���� �����') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime not null
);
go
create table New
(
Id int identity(1,1) primary key not null,
Name nvarchar(50) check (Name<>'') default('��������� �������') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('������� �������� �������') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('������ �������� �������') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime not null
);
go

--�������� �� �������

insert into Blog(Name) values
								('��������� � C#'),
								('��������� �� Android'),
								('��������� � Javascript'),
								('��������� � MS SQL'),
								('��������� � ������� ����������������');
go								
insert into New (Name) values
								('������� 1'),
								('������� 2'),
								('������� 3'),
								('������� 4'),
								('������� 5');
go