using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAccessment.Data.Dto;

namespace WemaAccessment.Common.Integration
{
  public interface IThirdPartyIntegration 
  {
    Task<GetBankListResponseModel> GetBankList();
  }
}
