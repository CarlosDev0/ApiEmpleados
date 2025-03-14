namespace ApiEmpleados.Models
{
    public class Pago
    {
        public Guid IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public long Valor { get; set; }
    }
}
