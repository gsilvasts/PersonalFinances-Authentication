using System.Text.RegularExpressions;

namespace PersonalFinances.Authentication.CrossCultting.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidPassword(this string password)
        {
            var expression = "^(?=.*\\d)(?=.*[A-Z][a-z])(?=.*[$*&@#])[0-9a-zA-Z$*&@#]{8,}$";

            return Regex.IsMatch(password, expression);
        }
    }
}
