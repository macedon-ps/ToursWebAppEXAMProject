/*1. ������� �������� � ���� ������ TourFirmaDB*/

/*
1.1. ����� ���������� � ����:
��� / ���������� / ������ ���� / ��������� ���� / ��������
*/

select Tour.Name as '���', Product.Name as '����������', 
DateTour.DateStart as '������ ����', DateTour.DateEnd as '��������� ����', Product.Description as '��������'
from Tour, Product, DateTour
where Tour.ProductId = Product.Id and Tour.DateTourId=DateTour.Id


/*
1.2. ����� ���������� � ����:
��� / ���������� / ������ ���� / ��������� ���� / ������ / ����� / ���������� / ��������
*/

select Tour.Name as '���', Product.Name as '����������', 
DateTour.DateStart as '������ ����', DateTour.DateEnd as '��������� ����', 
Country.Name as '������', City.Name as '�����', Hotel.Name as '����������', Hotel.LevelHotel as '�������� ������������',
Product.Description as '��������'
from Tour, Product, DateTour, Location, Country, City, Hotel
where Tour.ProductId = Product.Id and Tour.DateTourId=DateTour.Id 
and Tour.LocationId = Location.Id and Country.Id = Location.CountryId and City.Id = Location.CityId and Hotel.Id = Location.HotelId