using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Common.Integration
{
  public class OTPService : IOTPService
  {
    public bool SendOTP(string phoneNumber)
    {
      return true;
    }

    public bool VerifyOTP(string PhoneNumber, int oTP)
    {
      if (oTP == 1234567)
        return true;
      return false;
    }
  }
}
