using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public LoginController(ApplicationDbContext db, IAuthenticationSchemeProvider schemeProvider)
        {
            _db = db;
            _schemeProvider = schemeProvider;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var dbAdmin = _db.Admin.SingleOrDefault(a => a.Username == admin.Username);
            if (dbAdmin == null)
            {
                return View();
            }
            if (dbAdmin.Password != admin.Password)
            {
                TempData["error"] = "Credenziali non valide";
                return View();
            }

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, dbAdmin.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            TempData["success"] = "Login effettuato con successo";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["success"] = "Logout effettuato con successo";
            return RedirectToAction("Login");
        }
    }
}
