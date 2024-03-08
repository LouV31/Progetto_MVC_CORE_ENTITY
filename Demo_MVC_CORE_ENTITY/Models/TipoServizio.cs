using System.ComponentModel.DataAnnotations;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class TipoServizio
    {
        [Key]
        public int IdTipoServizio { get; set; }
        [Required]
        public string NomeTipoServizio { get; set; }


        public virtual ICollection<Servizio> Servizi { get; set; }
    }
}
