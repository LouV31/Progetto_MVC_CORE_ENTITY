using System.ComponentModel.DataAnnotations;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class Pensione
    {
        [Key]
        public int IdPensione { get; set; }
        [Required]
        public string TipoPensione { get; set; }
        [Required]
        public double Costo { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
