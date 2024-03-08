using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class ServizioController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServizioController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Servizio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Servizi.Include(s => s.Prenotazione).Include(s => s.TipoServizio).Include(s => s.Prenotazione.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _db.Servizi
                .Include(s => s.Prenotazione)
                .Include(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // GET: Servizio/Create
        public IActionResult Create()
        {
            // Recupera tutte le prenotazioni
            var prenotazioni = _db.Prenotazioni.Include(p => p.Cliente).ToList();

            // Crea una lista di SelectListItem, con il valore impostato all'IdPrenotazione e il testo impostato al CodiceFiscale del cliente
            var prenotazioniSelectList = prenotazioni.Select(p => new SelectListItem
            {
                Value = p.IdPrenotazione.ToString(),
                Text = p.Cliente.CodiceFiscale
            }).ToList();

            // Passa la lista alla vista tramite ViewData
            ViewData["IdPrenotazione"] = new SelectList(prenotazioniSelectList, "Value", "Text");
            ViewData["IdTipoServizio"] = new SelectList(_db.TipoServizio, "IdTipoServizio", "NomeTipoServizio");
            return View();
        }

        // POST: Servizio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrenotazione,IdTipoServizio,Costo")] Servizio servizio)
        {
            ModelState.Remove("Prenotazione");
            ModelState.Remove("TipoServizio");
            if (ModelState.IsValid)
            {
                _db.Add(servizio);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPrenotazione"] = new SelectList(_db.Prenotazioni, "IdPrenotazione", "IdPrenotazione", servizio.IdPrenotazione);
            ViewData["IdTipoServizio"] = new SelectList(_db.TipoServizio, "IdTipoServizio", "NomeTipoServizio", servizio.IdTipoServizio);
            return View(servizio);
        }

        // GET: Servizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _db.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            ViewData["IdPrenotazione"] = new SelectList(_db.Prenotazioni, "IdPrenotazione", "IdPrenotazione", servizio.IdPrenotazione);
            ViewData["IdTipoServizio"] = new SelectList(_db.TipoServizio, "IdTipoServizio", "NomeTipoServizio", servizio.IdTipoServizio);
            return View(servizio);
        }

        // POST: Servizio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServizio,IdPrenotazione,IdTipoServizio,Costo")] Servizio servizio)
        {
            if (id != servizio.IdServizio)
            {
                return NotFound();
            }

            ModelState.Remove("Prenotazione");
            ModelState.Remove("TipoServizio");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(servizio);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServizioExists(servizio.IdServizio))
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
            ViewData["IdPrenotazione"] = new SelectList(_db.Prenotazioni, "IdPrenotazione", "IdPrenotazione", servizio.IdPrenotazione);
            ViewData["IdTipoServizio"] = new SelectList(_db.TipoServizio, "IdTipoServizio", "NomeTipoServizio", servizio.IdTipoServizio);
            return View(servizio);
        }

        // GET: Servizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _db.Servizi
                .Include(s => s.Prenotazione)
                .Include(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // POST: Servizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servizio = await _db.Servizi.FindAsync(id);
            if (servizio != null)
            {
                _db.Servizi.Remove(servizio);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServizioExists(int id)
        {
            return _db.Servizi.Any(e => e.IdServizio == id);
        }
    }
}
