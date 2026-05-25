using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using DealerAutoMVC.Models;
using Microsoft.AspNetCore.Http;

namespace DealerAutoMVC.Controllers
{
    public class TransakcjaController : Controller
    {
        private readonly DealerAutoContext _context;
        private bool CzyZalogowany()
        {
            return HttpContext.Session.GetString("Login") != null;
        }

        public TransakcjaController(DealerAutoContext context)
        {
            _context = context;
        }

        // GET: Transakcja
        public async Task<IActionResult> Index()
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            var transakcje = _context.Transakcje
                .Include(t => t.Klient)
                .Include(t => t.ModelSamochodu)
                    .ThenInclude(m => m.Marka);

            return View(await transakcje.ToListAsync());
        }

        // GET: Transakcja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcje
                .Include(t => t.Klient)
                .Include(t => t.ModelSamochodu)
                    .ThenInclude(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transakcja == null)
            {
                return NotFound();
            }

            return View(transakcja);
        }

        // GET: Transakcja/Create
        public IActionResult Create()
        {
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "DaneKlienta");

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel"
            );

            return View();
        }

        // POST: Transakcja/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KlientId,ModelSamochoduId,DataTransakcji,CenaSprzedazy,FormaPlatnosci")] Transakcja transakcja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transakcja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["KlientId"] = new SelectList(
                _context.Klienci,
                "Id",
                "DaneKlienta",
                transakcja.KlientId
            );

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                transakcja.ModelSamochoduId
            );

            return View(transakcja);
        }

        // GET: Transakcja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcje.FindAsync(id);

            if (transakcja == null)
            {
                return NotFound();
            }

            ViewData["KlientId"] = new SelectList(
                _context.Klienci,
                "Id",
                "DaneKlienta",
                transakcja.KlientId
            );

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                transakcja.ModelSamochoduId
            );

            return View(transakcja);
        }

        // POST: Transakcja/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KlientId,ModelSamochoduId,DataTransakcji,CenaSprzedazy,FormaPlatnosci")] Transakcja transakcja)
        {
            if (id != transakcja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transakcja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransakcjaExists(transakcja.Id))
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

            ViewData["KlientId"] = new SelectList(
                _context.Klienci,
                "Id",
                "DaneKlienta",
                transakcja.KlientId
            );

            ViewData["ModelSamochoduId"] = new SelectList(
                _context.ModeleSamochodow
                    .Include(m => m.Marka)
                    .ToList(),
                "Id",
                "MarkaModel",
                transakcja.ModelSamochoduId
            );

            return View(transakcja);
        }

        // GET: Transakcja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcje
                .Include(t => t.Klient)
                .Include(t => t.ModelSamochodu)
                    .ThenInclude(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transakcja == null)
            {
                return NotFound();
            }

            return View(transakcja);
        }

        // POST: Transakcja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transakcja = await _context.Transakcje.FindAsync(id);

            if (transakcja != null)
            {
                _context.Transakcje.Remove(transakcja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransakcjaExists(int id)
        {
            return _context.Transakcje.Any(e => e.Id == id);
        }
    }
}