using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class PensioneController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PensioneController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Pensione
        public async Task<IActionResult> Index()
        {
            return View(await _db.Pensioni.ToListAsync());
        }

        // GET: Pensione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _db.Pensioni
                .FirstOrDefaultAsync(m => m.IdPensione == id);
            if (pensione == null)
            {
                return NotFound();
            }

            return View(pensione);
        }

        // GET: Pensione/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pensione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPensione,Costo")] Pensione pensione)
        {
            ModelState.Remove("Prenotazioni");
            if (ModelState.IsValid)
            {
                _db.Add(pensione);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pensione);
        }

        // GET: Pensione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _db.Pensioni.FindAsync(id);
            if (pensione == null)
            {
                return NotFound();
            }
            return View(pensione);
        }

        // POST: Pensione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPensione,TipoPensione,Costo")] Pensione pensione)
        {
            if (id != pensione.IdPensione)
            {
                return NotFound();
            }

            ModelState.Remove("Prenotazioni");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(pensione);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PensioneExists(pensione.IdPensione))
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
            return View(pensione);
        }

        // GET: Pensione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensione = await _db.Pensioni
                .FirstOrDefaultAsync(m => m.IdPensione == id);
            if (pensione == null)
            {
                return NotFound();
            }

            return View(pensione);
        }

        // POST: Pensione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pensione = await _db.Pensioni.FindAsync(id);
            if (pensione != null)
            {
                _db.Pensioni.Remove(pensione);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PensioneExists(int id)
        {
            return _db.Pensioni.Any(e => e.IdPensione == id);
        }
    }
}
