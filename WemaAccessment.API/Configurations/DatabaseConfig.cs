using Microsoft.EntityFrameworkCore;
using WemaAccessment.Data.Data;

namespace WemaAccessment.API.Configurations
{
  public static class DatabaseConfig
  {
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
      services.AddScoped<DbContext, DataContext>();

      services.AddDbContext<DataContext>(options =>
          options.UseSqlServer(config.GetConnectionString("default"),
          sqlServerOptionsAction: sqlOptions =>
          {
            sqlOptions.EnableRetryOnFailure();
          }));
    }


    public static void Configure(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<DataContext>();
      }
    }
  }
}
