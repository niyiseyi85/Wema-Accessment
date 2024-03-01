using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Data.Dto
{
  public class GetBankListResponseModel
  {
    public List<BankModel> Result { get; set; }
    public bool HasError { get; set; }
    public string ErrorMessage { get; set; }
  }

  public class BankModel
  {
    public string BankCode { get; set; }
    public string BankName { get; set; }
  }
}
