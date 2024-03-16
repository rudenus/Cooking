using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cooking.Infrastructure.Authenticator
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        const string Key = "mysupersecret_secretkey!1236";
        public const int LifeTime = 100;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
