using Entities.Models;
using Entities.Models.DbContexts;
using Microsoft.AspNetCore.Identity;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureWebApp(builder.Configuration);

#region Identity configuration
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
    {
        config.User.RequireUniqueEmail = true;
        config.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ModelsDbContext>()
    .AddDefaultTokenProviders();
#endregion

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5024);
});

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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

#region Endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
#endregion

app.Run();