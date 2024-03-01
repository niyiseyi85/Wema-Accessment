using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WemaAccessment.Data.Dto
{
  public static class Extensions
  {
    public static bool BeAllDigits(string value)
    {
      return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^\d+$");
    }

    public static bool ContainsSpecialCharacters(string value)
    {
      // Define a regular expression pattern to match special characters.
      string pattern = @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]";
      // Use Regex.IsMatch to check if the value contains special characters.
      return Regex.IsMatch(value, pattern);
    }
    public static bool BeAllDigits(int value)
    {
      return !string.IsNullOrEmpty(value.ToString()) && Regex.IsMatch(value.ToString(), @"^\d+$");
    }
  }
}
