using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential= true;
});

builder.Services.AddDbContext<OllieShopContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OllieShopConnection"))
);

//註冊IHttpContextAccessor，與identityCheck Class配合使用
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IdentityCheck>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
