using GeoSearchApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .WithMethods("POST", "OPTIONS", "GET")
               .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<LocationsRepository>();


var app = builder.Build();
app.UseCors("DefaultPolicy");

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

var citiesMatrix = app.Services.GetRequiredService<LocationsRepository>();

app.Run();
