using Microsoft.EntityFrameworkCore;
using WatchMe.Data;
using WatchMe.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(); // gRPC desteği
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add logging providers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add other services
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ResetPasswordService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<SOAPClient>();

// Add CORS and authorization
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

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// Middleware for logging requests
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request URL: {context.Request.Path}");
    await next();
});

// gRPC service mapping
app.MapGrpcService<MovieLikeServiceImpl>();

// Default route mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=WelcomePage}/{id?}");

// Reset Password route
app.MapControllerRoute(
    name: "reset-password",
    pattern: "api/auth/reset-password",
    defaults: new { controller = "Auth", action = "ResetPassword" });

app.Run();
