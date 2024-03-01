using System.Reflection;
using AutoMapper;
using WemaAccessment.Data.Dto;

namespace WemaAccessment.API.Configurations
{
  public static class AutomapperConfig
  {
    public static void Configure(IServiceCollection services, params Assembly[] additionalAssemblies)
    {
      services.AddAutoMapper(additionalAssemblies);
      services.AddAutoMapper(typeof(GetCustomerDtoRequestMappingConfig));
    }
  }
}
