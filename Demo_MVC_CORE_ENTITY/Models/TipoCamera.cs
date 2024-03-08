using System.ComponentModel.DataAnnotations;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class TipoCamera
    {
        [Key]
        public int IdTipoCamera { get; set; }
        [Required]
        public string NomeTipoCamera { get; set; }


        public virtual ICollection<Camera> Camere { get; set; }
    }
}
