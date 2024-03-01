using FluentValidation;
using WemaAccessment.Common.Integration;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.IRepository;
using WemaAccessment.Data.Repository;
using WemaAccessment.Service.IService;
using WemaAccessment.Service.Service;

namespace WemaAccessment.API.Configurations
{
  public static class ServiceExtentionConfig
  {
    public static void ConfigureServices(IServiceCollection services)
    {
      //Repository
      services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<ICustomerRepository, CustomerRepository>();
      services.AddScoped<IThirdPartyIntegration, ThirdPartyIntegration>();
      //Services
      services.AddScoped<ICustomerService, CustomerService>();
      services.AddScoped<IOTPService, OTPService>();
      services.AddScoped<IThirdPartyService, ThirdPartyService>();
      services.AddScoped<IValidator<CustomerDto>, CustomerDtoRequestValidator>();
    }
  }
}