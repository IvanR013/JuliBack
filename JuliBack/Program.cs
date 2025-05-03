using CloudinaryDotNet;
using JuliBack.Contexto;
using JuliBack.Recursos;
using JuliBack.Repositories;
using JuliBack.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para permitir solicitudes desde localhost con credenciales
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Permitir credenciales
        });
});

// Configurar Cloudinary
var cloudinaryAccount = new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);

// Add services to the container.

builder.Services.AddSingleton(cloudinary);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IimageRepository, ImageRep>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<IuserRepository, UserRepository>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(5, 7, 44))
));
var app = builder.Build();


// Ejecutar el seeder una sola vez
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<AppDbContext>();
//    var seeder = services.GetRequiredService<Seeder>();
//    await seeder.CreateUsers();
//}

// Usar la política de CORS configurada antes de otros middlewares
app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
