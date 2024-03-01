using FluentValidation;

namespace WemaAccessment.API.Configurations
{
  public static class FluentValidationConfig
  {
    public static void Configure()
    {
      ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
    }
  }
}
