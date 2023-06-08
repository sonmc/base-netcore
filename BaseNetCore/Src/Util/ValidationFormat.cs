using System.Text.RegularExpressions;

namespace BaseNetCore.Src.Utils
{
  public class ValidationFormat
  {
    public static bool IsPhoneNbr(string number)
    {
      string phone_pattern = @"^\(?([0-9]{3})\)?([0-9]{3})?([0-9]{4})$";
      if (number != null) return Regex.IsMatch(number, phone_pattern);
      else return false;
    }
  }
}
