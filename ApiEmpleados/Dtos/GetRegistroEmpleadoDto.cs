using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEmpleados.Dtos
{
    public class GetRegistroEmpleadoDto
    {
        public Guid IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public int IdRegistro { get; set; }
        
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        //public string Fecha { get; set; }
        
    }
}
