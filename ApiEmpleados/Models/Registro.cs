using System.ComponentModel.DataAnnotations;

namespace ApiEmpleados.Models
{
    public class Registro
    {
        [Key]
        public int IdRegistro { get; set; }

        
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        //public string Fecha { get; set; }
        //public Registro(int idRegistro, Guid idEmpleado, DateTime inicio, DateTime fin)
        //{
        //    this.IdRegistro= idRegistro;
        //    this.IdEmpleado = idEmpleado;
        //    this.Inicio= inicio;
        //    this.Fin= fin;
        //   // this.Fecha = fecha;
        //}
        //public Registro(Guid idEmpleado, DateTime inicio, DateTime fin)
        //{
        //    this.IdEmpleado = idEmpleado;
        //    this.Inicio = inicio;
        //    this.Fin = fin;
        //    //this.Fecha = fecha;
        //}
        public Empleado? Empleado { get; set; }
        public Guid EmpleadoId { get; set; }
        
        public int? ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
