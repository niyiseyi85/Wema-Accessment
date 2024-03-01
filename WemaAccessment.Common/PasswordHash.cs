using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Common
{
  public static class PasswordHash
  {
    public static string CreateHash(string password, string salt)
    {
      var salted = string.Concat(password, salt);
      using (var hashService = new SHA256CryptoServiceProvider())
      {
        var hash = hashService.ComputeHash(Encoding.ASCII.GetBytes(salted));
        return Convert.ToBase64String(hash);
      }
    }

    public static string CreateSalt()
    {
      byte[] bytes = new byte[128 / 8];
      using (var keyGenerator = RandomNumberGenerator.Create())
      {
        keyGenerator.GetBytes(bytes);
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
      }
    }
    
    public static string Hash(string password)
    {
      var salt = CreateSalt();
      return CreateHash(password, salt);
    }
  }
}
