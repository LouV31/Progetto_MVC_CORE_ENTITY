using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PrenotazioneController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Prenotazione
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Prenotazioni.Include(p => p.Camera).Include(p => p.Cliente).Include(p => p.Pensione);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prenotazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _db.Prenotazioni
                .Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .Include(p => p.Servizi).ThenInclude(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // GET: Prenotazione/Create
        public IActionResult Create()
        {
            ViewData["IdCamera"] = new SelectList(_db.Camera, "IdCamera", "Numero");
            ViewData["IdCliente"] = new SelectList(_db.Clienti, "IdCliente", "CodiceFiscale");
            ViewData["IdPensione"] = new SelectList(_db.Pensioni, "IdPensione", "TipoPensione");
            return View();
        }

        // POST: Prenotazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,IdCamera,IdPensione,DataInizio,DataFine,Acconto")] Prenotazione prenotazione)
        {

            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                _db.Add(prenotazione);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(_db.Camera, "IdCamera", "Numero", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_db.Clienti, "IdCliente", "CodiceFiscale", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_db.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
            return View(prenotazione);
        }

        // GET: Prenotazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _db.Prenotazioni.FindAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(_db.Camera, "IdCamera", "IdCamera", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_db.Clienti, "IdCliente", "Cellulare", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_db.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrenotazione,IdCliente,IdCamera,IdPensione,DataInizio,DataFine,Acconto")] Prenotazione prenotazione)
        {
            if (id != prenotazione.IdPrenotazione)
            {
                return NotFound();
            }

            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(prenotazione);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.IdPrenotazione))
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
            ViewData["IdCamera"] = new SelectList(_db.Camera, "IdCamera", "Numero", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_db.Clienti, "IdCliente", "CodiceFiscale", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_db.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
            return View(prenotazione);
        }

        // GET: Prenotazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _db.Prenotazioni
                .Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _db.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _db.Prenotazioni.Remove(prenotazione);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioneExists(int id)
        {
            return _db.Prenotazioni.Any(e => e.IdPrenotazione == id);
        }
    }
}
