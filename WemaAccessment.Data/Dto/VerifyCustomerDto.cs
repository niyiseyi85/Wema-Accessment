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
}
