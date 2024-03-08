using Microsoft.EntityFrameworkCore;
using Progetto_MVC_CORE_ENTITY.Models;

namespace Progetto_MVC_CORE_ENTITY.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }


        public DbSet<Servizio> Servizi { get; set; }
        public DbSet<Admin> Admin { get; set; }

        public DbSet<TipoCamera> TipoCamera { get; set; }
        public DbSet<TipoServizio> TipoServizio { get; set; }
        public DbSet<Pensione> Pensioni { get; set; }
    }
}
