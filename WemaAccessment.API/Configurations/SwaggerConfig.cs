using Microsoft.OpenApi.Models;

namespace WemaAccessment.API.Configurations
{
  public static class SwaggerConfig
  {
    public static void Configure(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Seyi",
          Description = "<h2>Description</h2><p>This is a test documentation.</p><br />" +
             "><ul><li><strong>Authorization (header)</strong><ul><li>" +
             "<p>The API uses no authorization presently.</p>" +
             "</li></ul></li></ul>",
          Contact = new OpenApiContact
          {
            Name = "Seyi Oyeniyi",
            Email = "Seyi12@gmail.com",
            Url = new Uri("https://linkedin.com/in/seyioyeniyi")
          },
        });
      });
    }       
  }
}