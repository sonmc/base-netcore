using System.Text.RegularExpressions; 

namespace Base.Utils
{
    public class StrUtil
    {
        public static string ConvertCamelToTitle(string camelCaseStr)
        {
            string titleCaseStr = Regex.Replace(camelCaseStr, "(\\B[A-Z])", " $1");
            return titleCaseStr;
        }
    }
}
