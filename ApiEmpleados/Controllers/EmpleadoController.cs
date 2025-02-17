using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using ApiEmpleados.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;

namespace ApiEmpleados.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private IRegistroService registroService;
        private IEmpleadoService empleadoService;
        public EmpleadoController(IRegistroService _registroService, IEmpleadoService _empleadoService)
        {
            registroService = _registroService;
            empleadoService = _empleadoService;
        }
        [HttpGet("getEmpleados")]
        public async Task<IEnumerable> getEmpleados() {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            //Empleado empleado1 = new Empleado(Guid.NewGuid(), "Juan", "70");
            //Empleado empleado2 = new Empleado(new Guid("FD713789-B5AE-49FF-8B2C-F311B9CB0DD3"), "Ana", "72");
            //List<Empleado> list = new List<Empleado>();
            //list.Add(empleado1);
            //list.Add(empleado2);
            //return list;
            return await empleadoService.GetAllEmpleados();
        }
        
        [HttpGet("getRegistrosOfEmployee")]
        public async Task<IEnumerable> getRegistrosOfEmployee(Guid idEmployee)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            return await registroService.GetRegistrosofEmployee(idEmployee);
        }

        [HttpGet("getRegistrosById")]
        public async Task<IEnumerable> getRegistrosById(int id)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;
            
            return await registroService.GetRegistrosById(id);

            //Registro record1 = new Registro(1, Guid.NewGuid(), "01:00", "02:00", "01/02/2023");
            //Registro record2 = new Registro(2, Guid.NewGuid(), "02:00", "03:00", "02/02/2023");
            //List<Registro> list = new List<Registro>();
            //list.Add(record1);
            //list.Add(record2);
            //return list;
        }

        [HttpGet("getListaEmpleados")]
        public async Task<IEnumerable> GetListaEmpleados()
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            return await registroService.GetListaEmpleados();
        }

        [HttpGet("getRegistrosByDate")]
        public async Task<IEnumerable> GetRegistrosByDate(string employee, DateTime startDate, DateTime endDate)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            return await registroService.GetRegistrosByDate(employee, startDate, endDate);
        }

        [HttpPost("PostRegistro")]
        public async Task<ServiceResponse<int>> PostRegistro(AddRegistroDto newRegistro)
        {
            var answer = await registroService.InsertRegistro(newRegistro);
            return answer;
        }

        [HttpPost("PostEmpleado")]
        public async Task<ObjectResult> PostEmpleado(AddEmpleadoDto newEmpleado)
        {
            var answer = await empleadoService.AddEmpleado(newEmpleado);
            if (answer.Success)
                return Ok(answer!);
            else
                return UnprocessableEntity(answer!);
        }
        
        [HttpGet("GetEmpleadobyId")]
        public async Task<IEnumerable<GetEmpleadoDto>> GetEmpleadoById(Guid idEmpleado)
        {
            var answer = await empleadoService.GetEmpleadoById(idEmpleado);
            return answer!;
        }
    }
}
