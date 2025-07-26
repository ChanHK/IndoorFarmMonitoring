using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Logging.AddConsole();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IPlantConfigService, PlantConfigService>();
builder.Services.AddScoped<ICombinedTrayService, CombinedTrayService>();
builder.Services.AddHttpClient<IPlantConfigRepository, PlantConfigRepository>();
builder.Services.AddHttpClient<ISensorRepository, SensorRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
