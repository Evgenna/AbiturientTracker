using University;
using Settings;
using Statistics;
using Abiturients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<UniversityConfiguration>(
    builder.Configuration.GetSection("University")
);

builder.Configuration.AddJsonFile("config.json", optional: false);

builder.Services.Configure<SettingsConfiguration>(builder.Configuration);


builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddScoped<UniversityProxy>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<DistributionService>();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

