using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mirea_Avia.Database;
using Mirea_Avia.Models.Users;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Mirea_Avia.Controllers
{
    public class Account : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
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

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
