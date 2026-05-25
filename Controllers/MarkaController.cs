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
    public class MarkaController : Controller
    {
        private readonly DealerAutoContext _context;

        public MarkaController(DealerAutoContext context)
        {
            _context = context;
        }
        private bool CzyZalogowany()
        {
            return HttpContext.Session.GetString("Login") != null;
        }

        // GET: Marka
        public async Task<IActionResult> Index()
        {
            

            return View(await _context.Marki.ToListAsync());
        }

        // GET: Marka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Marki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: Marka/Create
        public IActionResult Create()
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            return View();
        }

        // POST: Marka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,KrajPochodzenia")] Marka marka)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (ModelState.IsValid)
            {
                _context.Add(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: Marka/Edit/5
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

            var marka = await _context.Marki.FindAsync(id);
            if (marka == null)
            {
                return NotFound();
            }
            return View(marka);
        }

        // POST: Marka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,KrajPochodzenia")] Marka marka)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            if (id != marka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaExists(marka.Id))
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
            return View(marka);
        }

        // GET: Marka/Delete/5
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

            var marka = await _context.Marki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: Marka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!CzyZalogowany())
            {
                return RedirectToAction("Login", "Konto");
            }
            var marka = await _context.Marki.FindAsync(id);
            if (marka != null)
            {
                _context.Marki.Remove(marka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkaExists(int id)
        {
            return _context.Marki.Any(e => e.Id == id);
        }
      
    }
}
