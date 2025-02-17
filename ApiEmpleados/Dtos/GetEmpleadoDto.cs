using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiEmpleados.Dtos
{
    public class GetEmpleadoDto
    {
        public Guid IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public Boolean Estado { get; set; }
    }
}
