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
    public class PagoController : ControllerBase
    {
        IPagoService pagoService;
        public PagoController(IPagoService _pagoService)
        {
                pagoService = _pagoService;
        }
        [HttpGet ("GetPagoByEmpleadoId")]
        public async Task<IEnumerable> getPagoByEmpleadoId(Guid employeeId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;
            return await pagoService.GetPagosByEmployeeId(employeeId);
        }
    }
}
