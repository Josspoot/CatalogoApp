using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;
// Agrega estos namespaces necesarios para Identity y Entity Framework
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CatalogoApp.Presentation.Data; // Ajusta este using según la ubicación de tu ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la cadena de conexión y el DbContext (Asumiendo que usas SQLite basado en tus archivos de proyecto)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Registrar los servicios de Identity (Esto resuelve el error de UserManager)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Ruta del JSON
var jsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "data",
    "items.json"
);

// Registrar repositorio
builder.Services.AddSingleton<IItemRepository>(
    new JsonItemRepository(jsonPath)
);

// Registrar servicio
builder.Services.AddScoped<ItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseRouting();

// 3. Habilitar el middleware de autenticación (¡Debe ir ANTES de UseAuthorization!)
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 4. Mapear las páginas Razor (Requerido para que funcionen las rutas como /Account/Login que están en _LoginPartial.cshtml)
app.MapRazorPages();

app.Run();