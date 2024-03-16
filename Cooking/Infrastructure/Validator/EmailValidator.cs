using System.Text.RegularExpressions;

namespace Cooking.Infrastructure.Validator
{
    public class EmailValidator
    {
        private static readonly Regex EmailPattern = new Regex(
                pattern: @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
                options: RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public static bool TryParse(string value, out string email)
        {
            email = null;

            if(string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (!EmailPattern.IsMatch(value))
            {
                return false;
            }

            email = value.ToLowerInvariant();

            return true;
        }
    }
}
