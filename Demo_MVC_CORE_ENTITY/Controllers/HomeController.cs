using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Data;
using Progetto_MVC_CORE_ENTITY.Models;
using System.Diagnostics;

namespace Progetto_MVC_CORE_ENTITY.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // IN CONSOLE.LOG SI VEDE NON SONO RIUSCITO IN TEMPO A STAMPARLA
        public async Task<IActionResult> CercaPrenotazione(string? codiceFiscale)
        {
            try
            {

                var prenotazioni = await _db
                    .Prenotazioni.Include(p => p.Cliente)
                    .Where(p => p.Cliente.CodiceFiscale == codiceFiscale)
                    .Select(p => new
                    {
                        p.IdPrenotazione,
                        p.DataInizio,
                        p.DataFine,
                        p.Pensione.TipoPensione,
                        Cliente = new
                        {
                            p.Cliente.Nome,
                            p.Cliente.Cognome,
                            p.Cliente.CodiceFiscale,
                            p.Cliente.Email,
                            p.Cliente.Cellulare
                        },
                        Servizi = p.Servizi.Select(s => new
                        {
                            s.IdServizio,
                            s.Costo,
                            s.TipoServizio.NomeTipoServizio,
                            s.TipoServizio.IdTipoServizio

                        }),
                        Camera = new
                        {
                            p.Camera.Numero,

                            p.Camera.TipoCamera
                        }
                    }).ToListAsync();
                return Json(prenotazioni);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero dati");
                return BadRequest("Errore nel recupero dati");
            }
        }
    }
}
