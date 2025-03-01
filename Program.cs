using MVVMExample.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string dbConnStr = builder.Configuration.GetConnectionString("CustomerDB");

builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlServer(dbConnStr));

var app = builder.Build();

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
    pattern: "{controller=Customers}/{action=Search}/{id?}");

app.Run();
