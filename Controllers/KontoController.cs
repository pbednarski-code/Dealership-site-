using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using DealerAutoMVC.Helpers;

namespace DealerAutoMVC.Controllers
{
    public class KontoController : Controller
    {
        private readonly DealerAutoContext _context;

        public KontoController(DealerAutoContext context)
        {
            _context = context;
        }

        // GET: Konto/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Konto/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string haslo)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(haslo))
            {
                ViewData["Blad"] = "Podaj login i hasło.";
                return View();
            }

            string hasloHash = HashHelper.ObliczHash(haslo);

            var uzytkownik = await _context.Uzytkownicy
                .FirstOrDefaultAsync(u => u.Login == login && u.HasloHash == hasloHash);

            if (uzytkownik == null)
            {
                ViewData["Blad"] = "Nieprawidłowy login lub hasło.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", uzytkownik.Id);
            HttpContext.Session.SetString("Login", uzytkownik.Login);
            HttpContext.Session.SetString("CzyAdmin", uzytkownik.CzyAdmin ? "true" : "false");

            return RedirectToAction("Index", "Home");
        }

        // GET: Konto/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Konto");
        }
    }
}