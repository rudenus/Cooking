using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Cooking.Infrastructure
{
    public static class ControllerBaseExtensions
    {
        //не нравится, надо вырезать
        public static Guid GetUserId(this ControllerBase controller)
        {
            //return new Guid("9d6f89b7-48ae-4bdb-b686-7abf02ef548e");//пока заглушка для простоты

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

        //жесткий костыль
        public static Guid? GetNullableUserId(this ControllerBase controller)
        {
            //return new Guid("9d6f89b7-48ae-4bdb-b686-7abf02ef548e");//пока заглушка для простоты

            var value = controller.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (value == null)
            {
                return null;
            }

            if (!Guid.TryParse(value.Value, out Guid userId))
            {
                return null;
            }
            return userId;
        }
    }
}
