using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Services;
using Entities.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<ModelsDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ModelsDbContext")));sarasa

// Generic service
// builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));sarasa
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.ConfigureWebApp(builder.Configuration);

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

app.Run();