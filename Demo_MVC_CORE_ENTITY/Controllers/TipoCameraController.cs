using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class TipoCameraController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TipoCameraController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: TipoCamera
        public async Task<IActionResult> Index()
        {
            return View(await _db.TipoCamera.ToListAsync());
        }

        // GET: TipoCamera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _db.TipoCamera
                .FirstOrDefaultAsync(m => m.IdTipoCamera == id);
            if (tipoCamera == null)
            {
                return NotFound();
            }

            return View(tipoCamera);
        }

        // GET: TipoCamera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCamera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeTipoCamera")] TipoCamera tipoCamera)
        {
            if (_db.TipoCamera.Any(t => t.NomeTipoCamera == tipoCamera.NomeTipoCamera))
            {
                TempData["error"] = "Tipo camera già esistente";
                return View(tipoCamera);

            }
            ModelState.Remove("Camere");
            if (ModelState.IsValid)
            {
                _db.Add(tipoCamera);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCamera);
        }

        // GET: TipoCamera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _db.TipoCamera.FindAsync(id);
            if (tipoCamera == null)
            {
                return NotFound();
            }
            return View(tipoCamera);
        }

        // POST: TipoCamera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoCamera,NomeTipoCamera")] TipoCamera tipoCamera)
        {
            if (id != tipoCamera.IdTipoCamera)
            {
                return NotFound();
            }

            var existTipoCamera = _db.TipoCamera.Any(t => t.NomeTipoCamera == tipoCamera.NomeTipoCamera && t.IdTipoCamera != tipoCamera.IdTipoCamera);
            if (existTipoCamera)
            {
                TempData["error"] = "Esiste già questo tipo di camera associato ad un altro ID";
                return View(tipoCamera);
            }
            ModelState.Remove("Camere");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(tipoCamera);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCameraExists(tipoCamera.IdTipoCamera))
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
            return View(tipoCamera);
        }

        // GET: TipoCamera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _db.TipoCamera
                .FirstOrDefaultAsync(m => m.IdTipoCamera == id);
            if (tipoCamera == null)
            {
                return NotFound();
            }

            return View(tipoCamera);
        }

        // POST: TipoCamera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCamera = await _db.TipoCamera.FindAsync(id);
            if (tipoCamera != null)
            {
                _db.TipoCamera.Remove(tipoCamera);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCameraExists(int id)
        {
            return _db.TipoCamera.Any(e => e.IdTipoCamera == id);
        }
    }
}
