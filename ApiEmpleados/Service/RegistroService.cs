using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Security.Claims;

namespace ApiEmpleados.Service
{
    public class RegistroService : IRegistroService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegistroService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<GetEmpleadoDto>> GetListaEmpleados()
        {
            var data = await (from c in _context.Empleados
                              

                              select new GetEmpleadoDto() { 
                                  IdEmpleado= c.IdEmpleado,
                                  Nombre = c.Nombre,
                                  Cedula = c.Cedula,
                                  Estado = c.Estado
                              }
                        
                            ).ToListAsync();
            return data.Select(c => _mapper.Map<GetEmpleadoDto>(c)).ToList();
        }
        
        public async Task<ServiceResponse<int>> InsertRegistro(AddRegistroDto registroDto)
        {
            var response = new ServiceResponse<int>();
            try
            {
                
                var registro = _mapper.Map<Registro>(registroDto);

                //var user = _mapper.Map<User>(await _context.Users.Where(c => c.Username == GetUserId()).FirstOrDefaultAsync());
                //var empleado = _mapper.Map<Empleado>(await _context.Empleados.Where(c => c.UserId == (user!.Id)).FirstOrDefaultAsync());

                //registro.EmpleadoId = empleado.IdEmpleado;
                registro.EmpleadoId = registroDto.EmpleadoId;
                await _context.Registros.AddAsync(registro);
                await _context.SaveChangesAsync();
                response.Data = registro.IdRegistro;
                response.Success = true;
                response.Message = "El registro ha sido guardado exitosamente.";

                return response;
            }
            catch (Exception) {
                response.Success = false;
                response.Message = "El registro NO fue guardado. Por favor intente nuevamente más tarde.";
                return response;
            }
        }

        public async Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosById(int idRegistro)
        {
            var dbRegistros = await _context.Registros.Where(c => c.IdRegistro == idRegistro).ToListAsync();
            return dbRegistros.Select(c => _mapper.Map<RegistroEmpleadoDto>(c)).ToList();
        }

        public async Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosofEmployee(Guid idEmpleado)
        {
            var dbRegistros = await _context.Registros.Where(c => c.EmpleadoId == idEmpleado).ToListAsync();
            return dbRegistros.Select(c => _mapper.Map<RegistroEmpleadoDto>(c)).ToList();
        }

        //private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        //    .FindFirstValue(ClaimTypes.NameIdentifier)!);
        private string GetUserId() => (_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.Name)!);

        public async Task<IEnumerable<RegistroEmpleadoDto>> GetRegistrosByDate(string employee, DateTime startdate, DateTime endDate)
        {
            try
            {
                var newRegistros = new List<RegistroEmpleadoDto>();
                foreach (var x in from w in await _context.Registros.Where(c => c.Empleado!.IdEmpleado == Guid.Parse(employee) && c.Inicio >= startdate && c.Fin <= endDate).ToListAsync()
                                  where _context.Proyectos.Any(c => c.ProyectoId == w.ProyectoId)
                                  select w)
                {

                    newRegistros.Add(new RegistroEmpleadoDto()
                    {
                        Inicio = x.Inicio,
                        Fin = x.Fin,
                        IdRegistro = x.IdRegistro,
                        IdEmpleado = x.EmpleadoId,
                        Proyecto = await (from n in _context.Proyectos where n.ProyectoId == x.ProyectoId select n.NombreProyecto).FirstAsync() ?? "",
                        NombreEmpleado = await (from n in _context.Empleados where n.IdEmpleado == x.EmpleadoId select n.Nombre).FirstAsync() ?? "",
                        Duracion = Convert.ToInt32(x.Fin.Subtract(x.Inicio).TotalMinutes)
                    }
                        ); ;
                };
                return newRegistros;
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
