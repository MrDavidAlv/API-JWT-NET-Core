using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiEmpleados.Models
{
    [Table("Usuario", Schema = "Usuario")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string usuario { get; set; }

        [Required]
        public string contraseña { get; set; }

        
    }
}
