using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEmpleados.Dtos
{
    public class RegistroEmpleadoDto
    {
        public Guid IdEmpleado { get; set; }
        public int IdRegistro { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Proyecto { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public int Duracion { get; set; }
        //public string Fecha { get; set; }

    }
}
