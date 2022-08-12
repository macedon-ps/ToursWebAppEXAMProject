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

// ����������� ������� ������������� MS SQL Server � ��
builder.Services.AddDbContext<TourFirmaDBContext>(x=>x.UseSqlServer(ConfigData.ConnectionString));

// ������������ ��������� ����������������� ����� appsettings.json (���� "Project") �� ���������� ������ ConfigData 
builder.Configuration.Bind("Project", new ConfigData());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
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
app.MapControllerRoute(
	name: "research",
	pattern: "{controller=Search}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "admin",
	pattern: "{controller=Admin}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "support",
	pattern: "{controller=Support}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "about",
	pattern: "{controller=About}/{action=Index}/{id?}");
app.Run();
