using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>(IdentityOption =>
{
    IdentityOption.Password.RequireUppercase = true;
    IdentityOption.Password.RequireLowercase = true;
    IdentityOption.Password.RequireNonAlphanumeric = false;
    IdentityOption.User.RequireUniqueEmail = true;
    IdentityOption.Lockout.AllowedForNewUsers = true;
    IdentityOption.Lockout.MaxFailedAccessAttempts = 5;
    IdentityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    IdentityOption.SignIn.RequireConfirmedEmail = false;
}).
    AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext >();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
