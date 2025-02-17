using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiEmpleados.Dtos
{
    public class AddEmpleadoDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
    }
}
