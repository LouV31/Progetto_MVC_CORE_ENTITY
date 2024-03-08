using System.ComponentModel.DataAnnotations;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CodiceFiscale { get; set; }
        [Required]
        public string Città { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Cellulare { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
