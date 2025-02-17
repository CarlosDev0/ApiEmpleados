using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using ApiEmpleados.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;

namespace ApiEmpleados.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private IProyectoService proyectoService;
        public ProyectoController(IProyectoService _proyectoService)
        {
            proyectoService = _proyectoService;
        }
        [HttpGet("GetProyectos")]
        public async Task<ServiceResponse<IEnumerable<Proyecto>>> GetProyectos()
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            return await proyectoService.GetAllProyectos();
        }
    }
}
