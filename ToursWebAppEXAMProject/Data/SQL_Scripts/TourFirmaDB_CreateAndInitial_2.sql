use TourFirmaDB;
go

create table Blog
(
Id int identity(1,1) primary key,
Title nvarchar(50) check (Title<>'') default('��������� �����') not null,
Message nvarchar(50) check (Message<>'') default('��������� ��������� �����'),
FullMessageLine nvarchar(max) check (FullMessageLine<>'') default('������ ������ ���������'),
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime
);
go
create table New
(
Id int identity(1,1) primary key,
Title nvarchar(50) check (Title<>'') default('��������� �������') not null,
ShortDescription nvarchar(100) check (ShortDescription<>'') default('������� �������� �������') not null,
FullDescription nvarchar(max) check (FullDescription<>'') default('������ �������� �������') not null,
TitleImagePath nvarchar(100) check (TitleImagePath<>''),
DateAdded DateTime
);
go

--�������� �� �������

insert into Blog(Title) values
								('��������� � C#'),
								('��������� � Javascript'),
								('��������� � MS SQL'),
								('��������� � ������� ����������������');
								
insert into New (Title) values
								('������� 1'),
								('������� 2'),
								('������� 3'),
								('������� 4'),
								('������� 5');