using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public interface IPagoService
    {
        Task<List<Pago>> GetPagosByEmployeeId(Guid employeeId);
    }
}
