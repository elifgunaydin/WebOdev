using KuaforWebSitesi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Herhangi bir kaynaktan gelen isteklere izin verir
              .AllowAnyMethod()  // Herhangi bir HTTP methoduna izin verir (GET, POST, vb.)
              .AllowAnyHeader(); // Herhangi bir header'a izin verir
    });
});
// Add services to the container.

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{

    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturumun zaman a��m� s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax; // SameSite politikas�n� ayarlama (iste�e ba�l�)
});
// SSL hatalar�n� ge�ici olarak yoksaymak i�in a�a��daki kodu ekleyebilirsiniz
ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Musteri/MusteriGiris";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


builder.Services.AddControllers();
builder.Services.AddControllersWithViews(); // MVC i�in gerekli hizmeti ekleyin




builder.Services.AddDbContext<KuaforDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PasswordHasher<Musteri>>(); // PasswordHasher'� DI sistemine ekleyin
builder.Services.AddScoped<PasswordHasher<Calisan>>(); // PasswordHasher'� DI sistemine ekleyin


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseDeveloperExceptionPage(); // Geli�tirici ortam�nda detayl� hata mesajlar� g�sterir

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
