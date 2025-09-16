using Microsoft.EntityFrameworkCore;
using ProyectoSistemaOptica.BD;
using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.Server.Client.Pages;
using ProyectoSistemaOptica.Server.Components;
using System.Text.Json.Serialization; // <- para ReferenceHandler

// configura el constructor de la aplicacion 
var builder = WebApplication.CreateBuilder(args);

#region configura el constructor de la aplicacion y sus servicios 

// Registramos los controladores y configuramos JSON para ignorar ciclos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSwaggerGen();

var ConnectionStrings = builder.Configuration.GetConnectionString("ConnSqlServer")
                               ?? throw new InvalidOperationException(
                                   "El string de conexion no existe.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(ConnectionStrings));

// Add services to the container.
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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ProyectoSistemaOptica.Server.Client._Imports).Assembly);

#endregion

app.Run();
