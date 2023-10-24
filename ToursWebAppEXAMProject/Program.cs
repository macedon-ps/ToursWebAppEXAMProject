using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Hubs;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.Services;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������, ���. ��������� ����������� � �������������
builder.Services.AddControllersWithViews();

// ����������� SignalR
builder.Services.AddSignalR();

// ����������� ��������, ���. ��������� ���������� � ������, ���. �� ���������
builder.Services.AddTransient<IBaseInterface<Product>, BaseRepository<Product>>();
builder.Services.AddTransient<IBaseInterface<Country>, BaseRepository<Country>>();
builder.Services.AddTransient<IBaseInterface<City>, BaseRepository<City>>();
builder.Services.AddTransient<IBaseInterface<Hotel>, BaseRepository<Hotel>>();
builder.Services.AddTransient<IBaseInterface<DateTour>, BaseRepository<DateTour>>();
builder.Services.AddTransient<IBaseInterface<Food>, BaseRepository<Food>>();
builder.Services.AddTransient<IBaseInterface<Tour>, BaseRepository<Tour>>();
builder.Services.AddTransient<IBaseInterface<Customer>, BaseRepository<Customer>>();
builder.Services.AddTransient<IBaseInterface<Saller>, BaseRepository<Saller>>();
builder.Services.AddTransient<IBaseInterface<Offer>, BaseRepository<Offer>>();
builder.Services.AddTransient<IBaseInterface<Blog>, BaseRepository<Blog>>();
builder.Services.AddTransient<IBaseInterface<New>, BaseRepository<New>>();
builder.Services.AddTransient<IEditTechTaskInterface, EditTechTasksRepository>();
builder.Services.AddTransient<IQueryResultInterface, QueryResultRepository>();
builder.Services.AddTransient<DataManager>();

// ����������� ������� ������������� MS SQL Server � ��
builder.Services.AddDbContext<TourFirmaDBContext>(x=>x.UseSqlServer(ConfigData.ConnectionString));

// ������������ ��������� ����������������� ����� appsettings.json (���� "Project") �� ���������� ������ ConfigData 
builder.Configuration.Bind("Project", new ConfigData());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
	
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
