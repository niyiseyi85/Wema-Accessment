using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Common.Integration
{
  public interface IOTPService
  {
    bool SendOTP(string phoneNumber);
    bool VerifyOTP(string PhoneNumber, int oTP);
  }
}
