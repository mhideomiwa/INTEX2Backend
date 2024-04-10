using Intex2Backend.Data;
using Intex2Backend.UserData;
using Microsoft.EntityFrameworkCore;
using System.Resources;
//TODO: Add models to Data folder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<CPOLContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:CPOLConnection"]);
});

builder.Services.AddDbContext<UsersContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:UserConnection"]);
});

builder.Services.AddScoped<IBackendRepository, EFBackendRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Security Middleware
app.Use(async (ctx, next) =>
{
    var cspPolicies = new List<string>
    {
        "default-src 'self'",
        $"img-src 'self' https://brickset.com https://lego.com https://media-amazon.com https://brickeconomy.com",
        "script-src 'self' https://bootstrap.com",
        "style-src 'self' 'unsafe-inline' https://bootstrap.com",
        //"connect-src 'self' https://api.example.com"

        //default-src 'self': Restricts all resources to the same origin by default.
        //img - src 'self' https://example.com: Allows loading images from the same origin and https://example.com.
        //script - src 'self' https://trusted-scripts.com: Allows loading scripts from the same origin and https://trusted-scripts.com.
        //style - src 'self' 'unsafe-inline': Allows loading styles from the same origin and also allows inline styles('unsafe-inline' is required for inline styles).
        //connect - src 'self' https://api.example.com: Allows connecting to the same origin and https://api.example.com (for APIs, WebSockets, etc.).
    };

    ctx.Response.Headers.Add("Content-Security-Policy", string.Join("; ", cspPolicies));
    await next();
});

app.UseCors(p => p.WithOrigins("http://localhost:3000"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}