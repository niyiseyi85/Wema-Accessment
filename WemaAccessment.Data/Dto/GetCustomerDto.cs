using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WemaAccessment.Data.Models;

namespace WemaAccessment.Data.Dto
{
  public class GetCustomerDto
  {
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string StateOfResidence { get; set; }
    public string LGA { get; set; }
  }
  public class GetCustomerDtoRequestMappingConfig : Profile
  {
    public GetCustomerDtoRequestMappingConfig()
    {
      CreateMap<GetCustomerDto, Customer>().ReverseMap();
    }
  }
}
