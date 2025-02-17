using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public interface IRegistroService
    {
        Task<IEnumerable<GetEmpleadoDto>> GetListaEmpleados();
        Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosByDate(string employee, DateTime startdate, DateTime endDate);
        Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosById(int idRegistro);
        Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosofEmployee(Guid idEmpleado);
        Task<ServiceResponse<int>> InsertRegistro(AddRegistroDto registroDto);
    }
}