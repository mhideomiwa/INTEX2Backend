using Intex2Backend.Data;
using Intex2Backend.UserData;
using Microsoft.EntityFrameworkCore;
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

app.UseCors(p => p.WithOrigins("http://localhost:3000"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}