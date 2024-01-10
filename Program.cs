using GeoSearchApi.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

if (connectionString == null || connectionString == string.Empty)
{
    throw new InvalidOperationException("Connection string was not found or empty");
}

builder.Services.AddSingleton(provider => new LocationsRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var citiesMatrix = app.Services.GetRequiredService<LocationsRepository>();

app.Run();
