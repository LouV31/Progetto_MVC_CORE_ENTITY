using System.ComponentModel.DataAnnotations;

namespace Progetto_MVC_CORE_ENTITY.Models
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
