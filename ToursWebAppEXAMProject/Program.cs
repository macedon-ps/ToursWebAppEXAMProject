using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ConfigFiles;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Services.Hubs;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.Services.Email;

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
builder.Services.AddTransient<IBaseInterface<Asker>, BaseRepository<Asker>>();
builder.Services.AddTransient<IBaseInterface<Customer>, BaseRepository<Customer>>();
builder.Services.AddTransient<IBaseInterface<Correspondence>, BaseRepository<Correspondence>>();
builder.Services.AddTransient<IBaseInterface<Saller>, BaseRepository<Saller>>();
builder.Services.AddTransient<IBaseInterface<Offer>, BaseRepository<Offer>>();
builder.Services.AddTransient<IBaseInterface<Blog>, BaseRepository<Blog>>();
builder.Services.AddTransient<IBaseInterface<New>, BaseRepository<New>>();
builder.Services.AddTransient<IBaseInterface<EditAboutPageViewModel>, BaseRepository<EditAboutPageViewModel>>();
builder.Services.AddTransient<IEditTechTaskInterface, EditTechTasksRepository>();
builder.Services.AddTransient<IQueryResultInterface, QueryResultRepository>();
builder.Services.AddTransient<SearchUtils>();
builder.Services.AddTransient<TechTaskUtils>();
builder.Services.AddTransient<SupportUtils>();
builder.Services.AddTransient<AboutUtils>();
builder.Services.AddTransient<FeedbackUtils>();
builder.Services.AddTransient<FileUtils>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<NewsUtils>();
builder.Services.AddTransient<BlogUtils>();
builder.Services.AddTransient<CountryUtils>();
builder.Services.AddTransient<CityUtils>();

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
builder.Configuration.Bind("EmailConfiguration", new ConfigEmail());

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
