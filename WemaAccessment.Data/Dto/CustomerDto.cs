using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using WemaAccessment.Data.Models;


namespace WemaAccessment.Data.Dto
{
  public class CustomerDto
  {
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string StateOfResidence { get; set; }
    public string LGA { get; set; }
  }
  public class CustomerDtoRequestMappingConfig : Profile
  {
    public CustomerDtoRequestMappingConfig()
    {
      CreateMap<CustomerDto, Customer>().ReverseMap();
    }
  }
  public class CustomerDtoRequestValidator : AbstractValidator<CustomerDto>
  {
    public CustomerDtoRequestValidator()
    {
      RuleFor(x => x.PhoneNumber)
      .NotEmpty().WithMessage("Phone number is required.")
      .Length(11).WithMessage("Phone number length must be 11")
      .Must(Extensions.BeAllDigits).WithMessage("Phone number must be all digit")
      .Must(value => !Extensions.ContainsSpecialCharacters(value)).WithMessage("Phone number must not contain special characters");
      RuleFor(x => x.LGA)
        .NotEmpty().WithMessage("LGA is required")
        .NotNull();
      RuleFor(x => x.StateOfResidence)
        .NotEmpty().WithMessage("State Of Residence is required")
        .NotNull();
      RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required")
          .NotNull()
          .EmailAddress();
    }
  }
}
