using AdvertApi.Controllers.HealthChecks;
using AdvertApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IAdvertStorageService, DynamoDBAdvertStorage>();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().AddCheck<StorageHealthCheck>("Storage");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
