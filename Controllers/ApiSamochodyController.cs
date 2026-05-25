using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Data;
using DealerAutoMVC.Models;

namespace DealerAutoMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSamochodyController : ControllerBase
    {
        private readonly DealerAutoContext _context;

        public ApiSamochodyController(DealerAutoContext context)
        {
            _context = context;
        }

        private bool CzyPoprawnyToken(string login, string token)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(token))
            {
                return false;
            }

            return _context.Uzytkownicy.Any(u =>
                u.Login == login &&
                u.TokenApi == token);
        }

        // GET: api/ApiSamochody?login=admin&token=admin-token-123
        [HttpGet]
        public async Task<IActionResult> GetSamochody(string login, string token)
        {
            if (!CzyPoprawnyToken(login, token))
            {
                return Unauthorized("Niepoprawny login lub token.");
            }

            var samochody = await _context.ModeleSamochodow
                .Include(m => m.Marka)
                .Select(m => new
                {
                    m.Id,
                    m.MarkaId,
                    Marka = m.Marka != null ? m.Marka.Nazwa : "",
                    Model = m.Nazwa,
                    m.Rok,
                    m.Pojemnosc,
                    m.HorsePower,
                    m.Cena,
                    m.Przebieg,
                    m.Kolor,
                    m.CzySprzedany
                })
                .ToListAsync();

            return Ok(samochody);
        }

        // GET: api/ApiSamochody/5?login=admin&token=admin-token-123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSamochod(int id, string login, string token)
        {
            if (!CzyPoprawnyToken(login, token))
            {
                return Unauthorized("Niepoprawny login lub token.");
            }

            var samochod = await _context.ModeleSamochodow
                .Include(m => m.Marka)
                .Where(m => m.Id == id)
                .Select(m => new
                {
                    m.Id,
                    m.MarkaId,
                    Marka = m.Marka != null ? m.Marka.Nazwa : "",
                    Model = m.Nazwa,
                    m.Rok,
                    m.Pojemnosc,
                    m.HorsePower,
                    m.Cena,
                    m.Przebieg,
                    m.Kolor,
                    m.CzySprzedany
                })
                .FirstOrDefaultAsync();

            if (samochod == null)
            {
                return NotFound();
            }

            return Ok(samochod);
        }

        // POST: api/ApiSamochody?login=admin&token=admin-token-123
        [HttpPost]
        public async Task<IActionResult> PostSamochod(
            string login,
            string token,
            ModelSamochodu modelSamochodu)
        {
            if (!CzyPoprawnyToken(login, token))
            {
                return Unauthorized("Niepoprawny login lub token.");
            }

            _context.ModeleSamochodow.Add(modelSamochodu);
            await _context.SaveChangesAsync();

            var samochod = await _context.ModeleSamochodow
                .Include(m => m.Marka)
                .Where(m => m.Id == modelSamochodu.Id)
                .Select(m => new
                {
                    m.Id,
                    m.MarkaId,
                    Marka = m.Marka != null ? m.Marka.Nazwa : "",
                    Model = m.Nazwa,
                    m.Rok,
                    m.Pojemnosc,
                    m.HorsePower,
                    m.Cena,
                    m.Przebieg,
                    m.Kolor,
                    m.CzySprzedany
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(
                nameof(GetSamochod),
                new { id = modelSamochodu.Id, login = login, token = token },
                samochod);
        }

        // PUT: api/ApiSamochody/5?login=admin&token=admin-token-123
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSamochod(
            int id,
            string login,
            string token,
            ModelSamochodu modelSamochodu)
        {
            if (!CzyPoprawnyToken(login, token))
            {
                return Unauthorized("Niepoprawny login lub token.");
            }

            if (id != modelSamochodu.Id)
            {
                return BadRequest("Id z adresu nie zgadza się z Id obiektu.");
            }

            _context.Entry(modelSamochodu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelSamochoduExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/ApiSamochody/5?login=admin&token=admin-token-123
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSamochod(int id, string login, string token)
        {
            if (!CzyPoprawnyToken(login, token))
            {
                return Unauthorized("Niepoprawny login lub token.");
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
                return BadRequest("Nie można usunąć samochodu, ponieważ jest przypisany do transakcji.");
            }

            var wyposazenie = await _context.Wyposazenia
                .FirstOrDefaultAsync(w => w.ModelSamochoduId == id);

            if (wyposazenie != null)
            {
                _context.Wyposazenia.Remove(wyposazenie);
            }

            _context.ModeleSamochodow.Remove(modelSamochodu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelSamochoduExists(int id)
        {
            return _context.ModeleSamochodow.Any(e => e.Id == id);
        }
    }
}