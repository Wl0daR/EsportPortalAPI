using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllers();
builder.Services.AddDbContext<EsportContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Skonfiguruj Kestrel do u¿ywania certyfikatu SSL na porcie 7123
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7123, listenOptions =>
    {
        listenOptions.UseHttps("C:\\Certyfikaty\\OpenSSL-Win64\\selfsigned.pfx");
    });
});

var app = builder.Build();

// Skonfiguruj potok HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EsportPortalAPI V1");
        c.RoutePrefix = string.Empty;  // Sprawi, ¿e swagger bêdzie dostêpny pod adresem root, np. https://localhost:7123/
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
