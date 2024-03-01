using WemaAccessment.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AutomapperConfig.Configure(service);
SwaggerConfig.Configure(service);
DatabaseConfig.ConfigureServices(service, config);
ServiceExtentionConfig.ConfigureServices(service);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
DatabaseConfig.Configure(app);
FluentValidationConfig.Configure();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
