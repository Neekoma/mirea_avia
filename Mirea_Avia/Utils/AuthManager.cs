using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Mirea_Avia.Models.Users;
using System.Security.Claims;

namespace Mirea_Avia.Utils
{
    /// <summary>
    /// Управление учетными записями пользователя
    /// </summary>
    public static class AuthManager
    {
        /// <summary>
        /// Войти в учетную запись
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        public static void Auth(User user, out ClaimsIdentity claimsIdentity, out AuthenticationProperties authProperties)
        {

            IEnumerable<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };

            claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };
        }
    }
}
