using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAccessment.Common.Integration;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.ResponseModels;
using WemaAccessment.Service.IService;

namespace WemaAccessment.Service.Service
{
  public class ThirdPartyService : IThirdPartyService
  {
    private readonly IThirdPartyIntegration _thirdpartyIntegration;
    public ThirdPartyService(IThirdPartyIntegration thirdpartyIntegration)
    {
      _thirdpartyIntegration = thirdpartyIntegration;
    }
    public async Task<ResponseModel<List<GetBankListDto>>> GetBankList()
    {
      var response = new ResponseModel<List<GetBankListDto>>();
      var bankList = await _thirdpartyIntegration.GetBankList();
      
      if (bankList.HasError)
      {
        response.HasError = true;
        response.Message = bankList.ErrorMessage;
      }
      else
      {
        var banksListDto = new List<GetBankListDto>();
        foreach (var bank in bankList.Result)
        {
          var bankDto = new GetBankListDto()
          {
            BankCode = bank.BankCode,
            BankName = bank.BankName
          };
          banksListDto.Add(bankDto);
        }
        response.HasError = false;
        response.Message = "Banks retrieved successfully";
        response.Data = banksListDto;
      }
      return response;
    }
  }
}
