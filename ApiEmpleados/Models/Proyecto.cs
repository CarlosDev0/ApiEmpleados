using System.ComponentModel.DataAnnotations;

namespace ApiEmpleados.Models
{
    public class Proyecto
    {
        [Key]
        public int ProyectoId { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public ICollection<Registro>?   Registros { get; set; }
    }
}
