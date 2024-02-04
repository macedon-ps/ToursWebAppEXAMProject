using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Hubs;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.Services;
using ToursWebAppEXAMProject.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������, ���. ��������� ����������� � �������������
builder.Services.AddControllersWithViews();

// ����������� SignalR
builder.Services.AddSignalR();

// ����������� ����������� ������  
builder.Services.AddMemoryCache();

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
builder.Services.AddTransient<IBaseInterface<EditAboutPageViewModel>, BaseRepository<EditAboutPageViewModel>>();
builder.Services.AddTransient<IEditTechTaskInterface, EditTechTasksRepository>();
builder.Services.AddTransient<IQueryResultInterface, QueryResultRepository>();

// ����������� �������������� � �����������
// ����������� ���������� Identity � ���������������� ������� User, ����������� IdentityRole, ������� �������������� � �����������
builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.SignIn.RequireConfirmedEmail = true;   // ��������� ������������� ����� email
    opts.Password.RequiredLength = 5;   // ����������� �����
    opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
    opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
    opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
    opts.Password.RequireDigit = false; // ��������� �� �����
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TourFirmaDBContext>();

// ����������� ������� ������������� MS SQL Server � ��
//builder.Services.AddDbContext<TourFirmaDBContext>(x=>x.UseSqlServer(ConfigData.ConnectionString));

builder.Services.AddDbContext<TourFirmaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������������ ��������� ����������������� ����� appsettings.json: ���� "Project" �� ���������� ������ ConfigData � ����  "EmailConfiguration" �� ���������� ������ EmailConfig
builder.Configuration.Bind("Project", new ConfigData());
builder.Configuration.Bind("EmailConfiguration", new EmailConfig());

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

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
