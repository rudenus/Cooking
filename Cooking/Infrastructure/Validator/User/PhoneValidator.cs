using System.Text.RegularExpressions;

namespace Cooking.Infrastructure.Validator.User
{
    internal class PhoneValidator
    {
        private static readonly Regex PhonePattern = new Regex(
                @"^(?:(?:8?(?<Case1>\d{10}))|(?<Case2>\d{11}))",
                RegexOptions.Compiled);

        public static bool TryParse(string value, out string phone)
        {
            phone = null;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var phoneDraft = string.Join(string.Empty, value.Where(char.IsLetterOrDigit));
            var matches = PhonePattern.Matches(phoneDraft);

            if (matches.Count() != 1)
            {
                return false;
            }

            var match = matches[0];

            var case1 = match.Result("${Case1}");

            if (!string.IsNullOrEmpty(case1))
            {
                phone = $"7{case1}";
                return true;
            }

            var case2 = match.Result("${Case2}");

            if (!string.IsNullOrEmpty(case2))
            {
                phone = case2;
                return true;
            }

            return false;
        }
    }
}
