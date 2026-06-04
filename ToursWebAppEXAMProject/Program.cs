using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using ToursWebAppEXAMProject.ConfigFiles;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Migrations;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.Services.Email;
using ToursWebAppEXAMProject.Services.Hubs;
using ToursWebAppEXAMProject.Services.ImageStorage;
using ToursWebAppEXAMProject.Services.TechTasks;
using ToursWebAppEXAMProject.Utils;

var builder = WebApplication.CreateBuilder(args);

// настройка логирования через NLog и удаление всех стандартных провайдеров логирования
builder.Logging.ClearProviders();
builder.Host.UseNLog();
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
builder.Services.AddTransient<IBaseInterface<Asker>, BaseRepository<Asker>>();
builder.Services.AddTransient<IBaseInterface<Customer>, BaseRepository<Customer>>();
builder.Services.AddTransient<IBaseInterface<Correspondence>, BaseRepository<Correspondence>>();
builder.Services.AddTransient<IBaseInterface<Saller>, BaseRepository<Saller>>();
builder.Services.AddTransient<IBaseInterface<Offer>, BaseRepository<Offer>>();
builder.Services.AddTransient<IBaseInterface<Blog>, BaseRepository<Blog>>();
builder.Services.AddTransient<IBaseInterface<New>, BaseRepository<New>>();
builder.Services.AddTransient<IBaseInterface<AboutPageVersion>, BaseRepository<AboutPageVersion>>();
builder.Services.AddTransient<IBaseInterface<PhotoGalleryImage>, BaseRepository<PhotoGalleryImage>>();
builder.Services.AddTransient<IBaseInterface<TechTaskItem>, BaseRepository<TechTaskItem>>();
builder.Services.AddTransient<IEditTechTaskInterface, EditTechTasksRepository>();
builder.Services.AddTransient<ITechTaskService, TechTaskService>();
builder.Services.AddTransient<IQueryResultInterface, QueryResultRepository>();
builder.Services.AddTransient<SearchUtils>();
builder.Services.AddTransient<SupportUtils>();
builder.Services.AddTransient<AboutUtils>();
builder.Services.AddTransient<FeedbackUtils>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<NewsUtils>();
builder.Services.AddTransient<BlogUtils>();
builder.Services.AddTransient<CountryUtils>();
builder.Services.AddTransient<CityUtils>();
builder.Services.AddTransient<ProductUtils>();
builder.Services.AddTransient<TechTaskItemUtils>();
builder.Services.AddTransient<ImageStorageService>();
builder.Services.AddScoped<MigrationService>();

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

// cookie не будет продлеваться, сессия закончится строго при закрытии браузера
builder.Services.ConfigureApplicationCookie(options =>
{
    options.SlidingExpiration = false;
});

builder.Services.AddDbContext<TourFirmaDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddDbContext<SqlServerDBContext>(options =>
options.UseNpgsql(Environment.GetEnvironmentVariable("NEON_CONNECTION")));

// сопоставляем параметры конфигурационного файла appsettings.json: ключ "Project" со свойствами класса ConfigData и ключ  "EmailConfiguration" со свойствами класса EmailConfig
builder.Configuration.Bind("Project", new ConfigData());
builder.Configuration.Bind("EmailConfiguration", new ConfigEmail());

// версия для Development, пароль для email берется из класса ConfigEmail, а в Production - из переменных окружения
//builder.Configuration.Bind("MapApi", new ConfigMapApi());
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migration = scope.ServiceProvider.GetRequiredService<MigrationService>();

    await migration.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

// отключил, чтобы не конфликтовало с Render, не сбрасывало Deploy
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
