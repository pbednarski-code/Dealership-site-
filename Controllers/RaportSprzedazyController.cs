using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using DealerAutoMVC.Models;
using Microsoft.AspNetCore.Http;

namespace DealerAutoMVC.Controllers
{
    public class RaportSprzedazyController : Controller
    {
        private readonly DealerAutoContext _context;

        public RaportSprzedazyController(DealerAutoContext context)
        {
            _context = context;
        }
        private bool CzyZalogowany()
        {
            return HttpContext.Session.GetString("Login") != null;
        }
        // GET: RaportSprzedazy
        public async Task<IActionResult> Index()
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }

            var raport = await _context.Transakcje
                .Include(t => t.Klient)
                .Include(t => t.ModelSamochodu)
                    .ThenInclude(m => m.Marka)
                .Select(t => new RaportSprzedazy
                {
                    TransakcjaId = t.Id,

                    Klient = t.Klient != null
                        ? t.Klient.Imie + " " + t.Klient.Nazwisko
                        : "Brak klienta",

                    Samochod = t.ModelSamochodu != null
                        ? (
                            t.ModelSamochodu.Marka != null
                                ? t.ModelSamochodu.Marka.Nazwa + " " + t.ModelSamochodu.Nazwa
                                : t.ModelSamochodu.Nazwa
                          )
                        : "Brak samochodu",

                    DataTransakcji = t.DataTransakcji,
                    CenaSprzedazy = t.CenaSprzedazy,
                    FormaPlatnosci = t.FormaPlatnosci
                })
                .ToListAsync();

            ViewData["SumaSprzedazy"] = raport.Sum(r => r.CenaSprzedazy);
            ViewData["LiczbaTransakcji"] = raport.Count;

            return View(raport);
        }
    }
}