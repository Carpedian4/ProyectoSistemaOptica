using Microsoft.EntityFrameworkCore;
using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.Repositorio.Repositorios;
using ProyectoSistemaOptica.Server.Components;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region configura el constructor de la aplicacion y sus servicios 


builder.Services.AddHttpClient("BlazorClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5015/");
});

// Registrar el HttpClient como scoped para que pueda ser inyectado con @inject
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorClient"));

// 2. Tu configuración de Controllers (se mantiene)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 3. Tu configuración de Swagger (se mantiene)
builder.Services.AddSwaggerGen();

// 4. Tu configuración de DBContext y Repositorios (se mantiene)
var ConnectionStrings = builder.Configuration.GetConnectionString("ConnSqlServer")
                               ?? throw new InvalidOperationException(
                                   "El string de conexion no existe.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(ConnectionStrings));

builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();

// 5. Tu configuración de Blazor Components (se mantiene)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

#endregion

var app = builder.Build();

#region Construccion de la aplicacion y areas de middlewares

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

// Mapeo final de la aplicación Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ProyectoSistemaOptica.Server.Client._Imports).Assembly);

#endregion

app.Run();