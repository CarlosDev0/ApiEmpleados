using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public interface IProyectoService
    {
        Task<ServiceResponse<IEnumerable<Proyecto>>> GetAllProyectos();
    }
}
