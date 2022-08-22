using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToursWebAppEXAMProject.Controllers;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.Services;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������, ���. ��������� ����������� � �������������
builder.Services.AddControllersWithViews();
// ����������� �������, ���. ��������� ��������� � �����, ���. ��� ���������
builder.Services.AddTransient<IProduct, ProductsRepository>();
builder.Services.AddTransient<IArticle, ArticlesRepository>();
builder.Services.AddTransient<ICity, CitiesRepository>();
builder.Services.AddTransient<ICountry, CountriesRepository>();
builder.Services.AddTransient<ICustomer, CustomersRepository>();
builder.Services.AddTransient<IDateTour, DateToursRepository>();
builder.Services.AddTransient<IFood, FoodsRepository>();
builder.Services.AddTransient<IHotel, HotelsRepository>();
builder.Services.AddTransient<ILocation, LocationsRepository>();
builder.Services.AddTransient<INew, NewsRepository>();
builder.Services.AddTransient<IOfertum, OfertumsRepository>();
builder.Services.AddTransient<ISaller, SallersRepository>();
builder.Services.AddTransient<ITour, ToursRepository>();
builder.Services.AddTransient<DataManager>();

// ����������� ������� ������������� MS SQL Server � ��
builder.Services.AddDbContext<TourFirmaDBContext>(x=>x.UseSqlServer(ConfigData.ConnectionString));

// ������������ ��������� ����������������� ����� appsettings.json (���� "Project") �� ���������� ������ ConfigData 
builder.Configuration.Bind("Project", new ConfigData());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Search/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
