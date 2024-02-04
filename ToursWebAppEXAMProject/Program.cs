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

// подключение сервиса, кот. добавляет контроллеры и представления
builder.Services.AddControllersWithViews();

// подключение SignalR
builder.Services.AddSignalR();

// подключение кэширования данных  
builder.Services.AddMemoryCache();

// подключение сервисов, кот. связывают интерфейсы и классы, кот. их реализует
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

// подключение аутентификации и авторизации
// регистрация фреймворка Identity с пользовательским классом User, стандартным IdentityRole, опциями аутентификации и авторизации
builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.SignIn.RequireConfirmedEmail = true;   // требуется подтверждение через email
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TourFirmaDBContext>();

// подключение сервиса использования MS SQL Server и БД
//builder.Services.AddDbContext<TourFirmaDBContext>(x=>x.UseSqlServer(ConfigData.ConnectionString));

builder.Services.AddDbContext<TourFirmaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// сопоставляем параметры конфигурационного файла appsettings.json: ключ "Project" со свойствами класса ConfigData и ключ  "EmailConfiguration" со свойствами класса EmailConfig
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
