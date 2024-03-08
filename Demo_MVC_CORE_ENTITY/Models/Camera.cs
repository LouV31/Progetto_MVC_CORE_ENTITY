using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class Camera
    {
        [Key]
        public int IdCamera { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        [ForeignKey("TipoCamera")]
        public int IdTipoCamera { get; set; }
        [Required]
        public double Costo { get; set; }

        [Required]
        public bool Disponibile { get; set; }

        public virtual TipoCamera TipoCamera { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
