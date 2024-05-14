using Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace Cooking.Infrastructure.Authenticator
{
    public class Authenticator
    {
        private readonly Context context;
        public Authenticator(Context context)
        {
            this.context = context;
        }

        public async Task<AuthenticateResult> Authenticate(string login, string password)
        {//говнокод, но времени мало ):
            var token = await GetToken(login, password);
            var role = (await context.Users
                .Select(x => new
                {
                    x.Login,
                    x.Role
                })
                .FirstOrDefaultAsync(x => x.Login == login))
                !.Role;

            return new AuthenticateResult()
            {
                Role = role,
                Token = token
            };
        }

        public async Task<string> GetToken(string login, string password)
        {
            var authenticateResult = await GetClaims(login, password);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: authenticateResult.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        private async Task<ClaimsIdentity> GetClaims(string login, string password)
        {
            var user = await context.Users
                .Select(x => new
                {
                    x.Login,
                    x.PasswordHash,
                    x.UserId,
                })
                .FirstOrDefaultAsync(x => x.Login == login);

            if (user == null)
            {
                throw new AuthenticationException("Не найден аккаунт с таким логином");
            }

            if (user!.PasswordHash != MD5Hash(password))
            {
                throw new AuthenticationException("Пароль не совпадает");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString())
                };
            
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }

        //считаю, что лучше задублировать 10 строк, которые крайне маловерятно изменятся, чем выделять это в отдельный компонент
        private string MD5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var temp = Convert.ToHexString(hashBytes).ToLower();

                return temp;
            }
        }
    }
}
