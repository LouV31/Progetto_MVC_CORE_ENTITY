using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{
    public class CameraController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CameraController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Camera
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Camera.Include(c => c.TipoCamera);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Camera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _db.Camera
                .Include(c => c.TipoCamera)
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // GET: Camera/Create
        public IActionResult Create()
        {
            ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera");
            return View();
        }

        // POST: Camera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,IdTipoCamera,Costo,Disponibile")] Camera camera)
        {

            if (_db.Camera.Any(c => c.Numero == camera.Numero))
            {
                TempData["error"] = "Numero camera già esistente";
                ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera", camera.IdTipoCamera);
                View(camera);
            }
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("TipoCamera");
            if (ModelState.IsValid)
            {
                _db.Add(camera);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera", camera.IdTipoCamera);
            return View(camera);
        }

        // GET: Camera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _db.Camera.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera", camera.IdTipoCamera);
            return View(camera);
        }

        // POST: Camera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCamera,Numero,IdTipoCamera,Costo,Disponibile")] Camera camera)
        {
            if (id != camera.IdCamera)
            {
                return NotFound();
            }

            var existingCamera = _db.Camera.FirstOrDefault(c => c.Numero == camera.Numero && c.IdCamera != camera.IdCamera);
            if (existingCamera != null)
            {
                TempData["error"] = "Numero camera già esistente";
                ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera", camera.IdTipoCamera);
                return View(camera);
            }

            ModelState.Remove("Prenotazioni");
            ModelState.Remove("TipoCamera");
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(camera);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.IdCamera))
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
            ViewData["IdTipoCamera"] = new SelectList(_db.TipoCamera, "IdTipoCamera", "NomeTipoCamera", camera.IdTipoCamera);
            return View(camera);
        }

        // GET: Camera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _db.Camera
                .Include(c => c.TipoCamera)
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // POST: Camera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _db.Camera.FindAsync(id);
            if (camera != null)
            {
                _db.Camera.Remove(camera);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CameraExists(int id)
        {
            return _db.Camera.Any(e => e.IdCamera == id);
        }
    }
}
