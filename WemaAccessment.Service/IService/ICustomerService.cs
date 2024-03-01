using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.ResponseModels;

namespace WemaAccessment.Service.IService
{
  public interface ICustomerService
  {
    Task<ResponseModel> VerifiyCustomer(VerifyCustomerDto varifyCustomerDto);
    Task<ResponseModel> OnboardCustomer(CustomerDto customerDto);
    Task<ResponseModel<List<GetCustomerDto>>> GetAllCustomer();
  }
}
