using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using DealerAutoMVC.Models;

namespace DealerAutoMVC.Controllers
{
    public class ModelSamochoduController : Controller
    {
        private readonly DealerAutoContext _context;

        private bool CzyZalogowany()
        {
            return HttpContext.Session.GetString("Login") != null;
        }

        public ModelSamochoduController(DealerAutoContext context)
        {
            _context = context;
        }

        // GET: ModelSamochodu
        public async Task<IActionResult> Index()
        {
            var modele = _context.ModeleSamochodow
                .Include(m => m.Marka);

            return View(await modele.ToListAsync());
        }

        // GET: ModelSamochodu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelSamochodu = await _context.ModeleSamochodow
                .Include(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (modelSamochodu == null)
            {
                return NotFound();
            }

            return View(modelSamochodu);
        }

        // GET: ModelSamochodu/Create
        public IActionResult Create()
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            ViewData["MarkaId"] = new SelectList(_context.Marki, "Id", "Nazwa");
            return View();
        }

        // POST: ModelSamochodu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MarkaId,Nazwa,Rok,Pojemnosc,HorsePower,Cena,Przebieg,Kolor,CzySprzedany")] ModelSamochodu modelSamochodu)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (ModelState.IsValid)
            {
                _context.Add(modelSamochodu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MarkaId"] = new SelectList(_context.Marki, "Id", "Nazwa", modelSamochodu.MarkaId);
            return View(modelSamochodu);
        }

        // GET: ModelSamochodu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (id == null)
            {
                return NotFound();
            }

            var modelSamochodu = await _context.ModeleSamochodow.FindAsync(id);

            if (modelSamochodu == null)
            {
                return NotFound();
            }

            ViewData["MarkaId"] = new SelectList(_context.Marki, "Id", "Nazwa", modelSamochodu.MarkaId);
            return View(modelSamochodu);
        }

        // POST: ModelSamochodu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MarkaId,Nazwa,Rok,Pojemnosc,HorsePower,Cena,Przebieg,Kolor,CzySprzedany")] ModelSamochodu modelSamochodu)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (id != modelSamochodu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelSamochodu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelSamochoduExists(modelSamochodu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MarkaId"] = new SelectList(_context.Marki, "Id", "Nazwa", modelSamochodu.MarkaId);
            return View(modelSamochodu);
        }

        // GET: ModelSamochodu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (id == null)
            {
                return NotFound();
            }

            var modelSamochodu = await _context.ModeleSamochodow
                .Include(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (modelSamochodu == null)
            {
                return NotFound();
            }

            return View(modelSamochodu);
        }

        // POST: ModelSamochodu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }

            var modelSamochodu = await _context.ModeleSamochodow.FindAsync(id);

            if (modelSamochodu == null)
            {
                return NotFound();
            }

            bool maTransakcje = await _context.Transakcje
                .AnyAsync(t => t.ModelSamochoduId == id);

            if (maTransakcje)
            {
                TempData["Blad"] = "Nie można usunąć samochodu, ponieważ jest przypisany do transakcji.";
                return RedirectToAction(nameof(Index));
            }

            var wyposazenie = await _context.Wyposazenia
                .FirstOrDefaultAsync(w => w.ModelSamochoduId == id);

            if (wyposazenie != null)
            {
                _context.Wyposazenia.Remove(wyposazenie);
            }

        _context.ModeleSamochodow.Remove(modelSamochodu);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
        }

        private bool ModelSamochoduExists(int id)
        {
            return _context.ModeleSamochodow.Any(e => e.Id == id);
        }
    }
}