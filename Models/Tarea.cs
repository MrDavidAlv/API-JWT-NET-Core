using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiEmpleados.Models
{
    [Table("tarea", Schema = "tareas")]

    public class Tarea
    {
        [Key]
        public int id_tarea { get; set; }

        [Required]
        [MaxLength(100)]
        public string titulo { get; set; }

        [MaxLength(500)]
        public string descripcion { get; set; }

        [Required]
        public DateTime fecha_creacion { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
    
    }
}
