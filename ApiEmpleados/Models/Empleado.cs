using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEmpleados.Models
{
    public class Empleado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid IdEmpleado { get; set; }
        //public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public Boolean Estado { get; set; } = true;
        //public Empleado(Guid idEmpleado, string nombre, string cedula)
        //{
        //    this.IdEmpleado = idEmpleado;
        //    this.Nombre = nombre;
        //    this.Cedula = cedula;
        //}
        //public Empleado(string nombre, string cedula)
        //{
        //    this.Nombre = nombre;
        //    this.Cedula = cedula;
        //}

        public User? User { get; set; }
        public int UserId { get; set; }  //Identifica el Foreign key. Esta es la clase dependiente. Relación: 1:1
    }
}
