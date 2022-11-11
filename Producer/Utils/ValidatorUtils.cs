using System.Text.RegularExpressions;

namespace Producer.Utils
{
    public class ValidatorUtils
    {
        public static bool HasValidPassword(string psw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");

            return (lowercase.IsMatch(psw) 
                    && uppercase.IsMatch(psw) 
                    && digit.IsMatch(psw) 
                    && symbol.IsMatch(psw));
        }
    }
}
