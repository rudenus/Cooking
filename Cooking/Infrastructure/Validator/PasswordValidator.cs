using System.Text.RegularExpressions;

namespace Cooking.Infrastructure.Validator
{
    public class PasswordValidator
    {
        private static readonly Regex PasswordPattern = new Regex(
                pattern: @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,10}$",
                options: RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public static bool TryParse(string value, out string password)
        {
            password = null;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (!PasswordPattern.IsMatch(value))
            {
                return false;
            }

            password = value;

            return true;
        }
    }
}
