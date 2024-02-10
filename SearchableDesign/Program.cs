using SearchableDesign.Infrastructure;
using SearchableDesign.Repository.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//addded for DI
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
//database connectivity
builder.Configuration.SetBasePath(app.Environment.ContentRootPath).AddJsonFile("appsettings.json");
ConnectionString.DBConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
