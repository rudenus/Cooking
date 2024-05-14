using Dal.Enums;

namespace Cooking.Infrastructure.Authenticator
{
    public class AuthenticateResult
    {
        public string Token { get; set; }

        public Role Role { get; set; }
    }
}
