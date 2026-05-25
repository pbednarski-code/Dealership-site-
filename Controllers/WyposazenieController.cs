using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using Microsoft.AspNetCore.Http;
using DealerAutoMVC.Models;

namespace DealerAutoMVC.Controllers
{
    public class WyposazenieController : Controller
    {
        private readonly DealerAutoContext _context;
        private bool CzyZalogowany()
        {
            return HttpContext.Session.GetString("Login") != null;
        }

        public WyposazenieController(DealerAutoContext context)
        {
            _context = context;
        }

        // GET: Wyposazenie
        public async Task<IActionResult> Index()
        {
            var wyposazenia = _context.Wyposazenia
                .Include(w => w.ModelSamochodu)
                    .ThenInclude(m => m.Marka);

            return View(await wyposazenia.ToListAsync());
        }

        // GET: Wyposazenie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyposazenie = await _context.Wyposazenia
                .Include(w => w.ModelSamochodu)
                    .ThenInclude(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wyposazenie == null)
            {
                return NotFound();
            }

            return View(wyposazenie);
        }

        // GET: Wyposazenie/Create
        public IActionResult Create()
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel"
            );

            return View();
        }

        // POST: Wyposazenie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelSamochoduId,Klimatyzacja,Nawigacja,SkorzanaTapicerka,KameraCofania,CzujnikiParkowania,AppleCarPlay")] Wyposazenie wyposazenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wyposazenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                wyposazenie.ModelSamochoduId
            );

            return View(wyposazenie);
        }

        // GET: Wyposazenie/Edit/5
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

            var wyposazenie = await _context.Wyposazenia.FindAsync(id);

            if (wyposazenie == null)
            {
                return NotFound();
            }

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                wyposazenie.ModelSamochoduId
            );

            return View(wyposazenie);
        }

        // POST: Wyposazenie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelSamochoduId,Klimatyzacja,Nawigacja,SkorzanaTapicerka,KameraCofania,CzujnikiParkowania,AppleCarPlay")] Wyposazenie wyposazenie)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (id != wyposazenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wyposazenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WyposazenieExists(wyposazenie.Id))
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

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                wyposazenie.ModelSamochoduId
            );

            return View(wyposazenie);
        }

        // GET: Wyposazenie/Delete/5
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

            var wyposazenie = await _context.Wyposazenia
                .Include(w => w.ModelSamochodu)
                    .ThenInclude(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wyposazenie == null)
            {
                return NotFound();
            }

            return View(wyposazenie);
        }

        // POST: Wyposazenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            var wyposazenie = await _context.Wyposazenia.FindAsync(id);

            if (wyposazenie != null)
            {
                _context.Wyposazenia.Remove(wyposazenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WyposazenieExists(int id)
        {
            return _context.Wyposazenia.Any(e => e.Id == id);
        }
    }
}