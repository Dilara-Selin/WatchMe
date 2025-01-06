using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Services;

var builder = WebApplication.CreateBuilder(args);

// Loglama seviyesini ayarla
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// DbContext servisini ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 

// Diğer servisleri ekle
builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();
builder.Services.AddScoped<EmailService>(); 
builder.Services.AddScoped<ResetPasswordService>(); 
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ResetPasswordService>();
builder.Services.AddScoped<MovieService>();

// SOAPClient servisini Scoped olarak ekle
builder.Services.AddScoped<SOAPClient>();

// CORS ve Authorization yapılandırması
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<TVShowService>();

builder.Services.AddScoped<MovieService>();
// Add Authorization

builder.Services.AddAuthorization();

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware'ler
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// Default rotayı ekle
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=WelcomePage}/{id?}");

// Reset Password rotası
app.MapControllerRoute(
    name: "reset-password",
    pattern: "api/auth/reset-password",
    defaults: new { controller = "Auth", action = "ResetPassword" });

// Request loglama middleware'i
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request URL: {context.Request.Path}");
    await next();
});

app.Run();