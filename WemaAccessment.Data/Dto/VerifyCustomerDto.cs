using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Data.Dto
{
  public class VerifyCustomerDto
  {
    public string PhoneNumber { get; set; }
    public int OTP { get; set; }
  }
  public class VerifyCustomerDtoRequestValidator : AbstractValidator<VerifyCustomerDto>
  {
    public VerifyCustomerDtoRequestValidator()
    {
      RuleFor(x => x.PhoneNumber)
         .NotEmpty().WithMessage("Phone number is required.")
         .Length(11).WithMessage("Phone number length must be 11")
         .Must(Extensions.BeAllDigits).WithMessage("Phone number must be all digit")
         .Must(value => !Extensions.ContainsSpecialCharacters(value)).WithMessage("Phone number must not contain special characters");
      RuleFor(x => x.OTP)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .Must(Extensions.BeAllDigits).WithMessage("{PropertyName} must consist of digits only.");
    }
  }
}
