using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class TipoServizioController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TipoServizioController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: TipoServizio
        public async Task<IActionResult> Index()
        {
            return View(await _db.TipoServizio.ToListAsync());
        }

        // GET: TipoServizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _db.TipoServizio
                .FirstOrDefaultAsync(m => m.IdTipoServizio == id);
            if (tipoServizio == null)
            {
                return NotFound();
            }

            return View(tipoServizio);
        }

        // GET: TipoServizio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoServizio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeTipoServizio")] TipoServizio tipoServizio)
        {
            if (_db.TipoServizio.Any(t => t.NomeTipoServizio == tipoServizio.NomeTipoServizio))
            {
                TempData["error"] = "Tipo camera già esistente";
                return View(tipoServizio);

            }

            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                _db.Add(tipoServizio);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServizio);
        }

        // GET: TipoServizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _db.TipoServizio.FindAsync(id);
            if (tipoServizio == null)
            {
                return NotFound();
            }
            return View(tipoServizio);
        }

        // POST: TipoServizio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoServizio,NomeTipoServizio")] TipoServizio tipoServizio)
        {
            if (id != tipoServizio.IdTipoServizio)
            {
                return NotFound();
            }

            var existTipoServizio = _db.TipoServizio.Any(t => t.NomeTipoServizio == tipoServizio.NomeTipoServizio && t.IdTipoServizio != tipoServizio.IdTipoServizio);
            if (existTipoServizio)
            {
                TempData["error"] = "Esiste già questo tipo di servizio associato ad un altro ID";
                return View(tipoServizio);
            }

            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(tipoServizio);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServizioExists(tipoServizio.IdTipoServizio))
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
            return View(tipoServizio);
        }

        // GET: TipoServizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _db.TipoServizio
                .FirstOrDefaultAsync(m => m.IdTipoServizio == id);
            if (tipoServizio == null)
            {
                return NotFound();
            }

            return View(tipoServizio);
        }

        // POST: TipoServizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoServizio = await _db.TipoServizio.FindAsync(id);
            if (tipoServizio != null)
            {
                _db.TipoServizio.Remove(tipoServizio);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoServizioExists(int id)
        {
            return _db.TipoServizio.Any(e => e.IdTipoServizio == id);
        }
    }
}
