using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Cooking.Infrastructure
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserId(this ControllerBase controller)
        {
            var value = controller.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (value == null)
            {
                throw new AuthenticationException("Неверный формат токена авторизации");
            }

            if (!Guid.TryParse(value.Value, out Guid userId))
            {
                throw new AuthenticationException($"Неверный формат токена авторизации");
            }
            return userId;
        }
    }
}
