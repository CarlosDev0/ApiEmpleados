using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<GetEmpleadoDto>> GetAllEmpleados();
        Task<ServiceResponse<GetEmpleadoDto>> AddEmpleado(AddEmpleadoDto newRegistro);
        Task<IEnumerable<GetEmpleadoDto>> GetEmpleadoById(Guid idEmpleado);
    }
}