using LibreriaBack.Servicios.Implementacion;
using LibreriaBack.Servicios;
using Microsoft.Extensions.FileProviders;
using LibreriaFrontendWeb.Hub;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://0.0.0.0:7276");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IPDFService, PDFService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEstadisticasService, EstadisticasService>();
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.Name = "LibreriaSession";
    options.Cookie.Path = "/";
    options.Cookie.Domain = null;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Set-Cookie");
    });
});


var app = builder.Build();

// ORDEN SÚPER IMPORTANTE para que las sessions funcionen en IIS
app.UseCors();
app.UseSession(); // PRIMERO

// Comentá esto temporalmente para testear
app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Presentacion")),
    RequestPath = ""
});

app.UseDefaultFiles();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Login/Login.html");
});

app.MapHub<ProgressHub>("/progressHub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();