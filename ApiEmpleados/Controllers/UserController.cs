using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using ApiEmpleados.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public UserController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] UserAuthDto user) {
            //{            "password": "string",  "username": "Carlos"}
        AnswerAuthentication _answerAuth = new AnswerAuthentication();
            _answerAuth = await _authRepository.Login(user);
            if (_answerAuth.Answer)
                return Ok(JsonConvert.SerializeObject(_answerAuth));
            return Unauthorized();
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (token is null || token == string.Empty)
            {
                return Unauthorized();
            }
            var userName = User.Claims.Where(x => x.Type! == ClaimTypes.Name).FirstOrDefault()!.Value;

            if (userDto.Nombre != string.Empty && userDto.Cedula != string.Empty
                && userDto.username != string.Empty && userDto.Nombre != string.Empty)
            {
                ServiceResponse<int> _answerAuth = await _authRepository.Register(userDto);
                if (_answerAuth.Success)
                    return Ok(JsonConvert.SerializeObject(_answerAuth));
                return UnprocessableEntity();
            }
            else { 
                return BadRequest();
            }
        }
    }
}
