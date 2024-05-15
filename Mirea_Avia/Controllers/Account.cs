using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mirea_Avia.Database;
using Mirea_Avia.Models.Users;
using Mirea_Avia.Utils;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Mirea_Avia.Controllers
{
    public class Account : Controller
    {

        /// <summary>
        /// Форма регистрации пользователя
        /// </summary>
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Форма авторизации пользователя
        /// </summary>
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Выход из учетной записи пользователя
        /// </summary>
        /// <returns>Редирект на домашнюю страницу</returns>
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Обработчик формы регистрации пользователя
        /// </summary>
        /// <param name="model">Форма регистрации пользователея</param>
        /// <returns>Редирект с зарегистированным и авторизованным пользоватетем или на ошибку</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.Users.FirstOrDefault(x => x.Login == model.Login || x.Email == model.Email) != null)
                    {
                        ModelState.AddModelError("UserCreationError", "Пользователь с таким логином или Email уже зарегестрирован");
                        return View(model);
                    }

                    byte[] passwordBytes = Encoding.UTF8.GetBytes(model.Password);
                    byte[] passwordHash = SHA256.HashData(passwordBytes);
                    model.Password = Convert.ToHexString(passwordHash);

                    db.Users.Add(model);

                    await db.SaveChangesAsync();
                }

                AuthManager.Auth(model, out ClaimsIdentity claimsIdentity, out AuthenticationProperties authProperties);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        /// <summary>
        ///  Обработчик формы авторизации пользователя
        /// </summary>
        /// <param name="model">Модель входа</param>
        /// <returns>Редирект с авторизованным пользоватетем или на ошибку</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(model.Password);
                    byte[] passwordHash = SHA256.HashData(passwordBytes);
                    string hexPassword = Convert.ToHexString(passwordHash);

                    User user = db.Users.FirstOrDefault(x => x.Login == model.Login);

                    if (user == null)
                    {
                        ModelState.AddModelError("UserLoginError", "Не верный логин или пароль");
                        return View(model);
                    }

                    if (user.Password != hexPassword)
                        user = null;          

                    AuthManager.Auth(user, out ClaimsIdentity claimsIdentity, out AuthenticationProperties authProperties);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
