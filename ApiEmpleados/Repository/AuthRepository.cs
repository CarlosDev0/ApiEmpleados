using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiEmpleados.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly DataContext _Context;

        public AuthRepository(IMapper mapper, IConfiguration configuration, DataContext dataContext)
        {
            _mapper = mapper;
            this.configuration = configuration;
            _Context = dataContext;
        }
        public string CreateToken(UserAuthDto user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            };
            var appSettingsToken = configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null");

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<AnswerAuthentication> Login(UserAuthDto userReceived)
        {
            AnswerAuthentication _answerAuth = new AnswerAuthentication();
            var user = await _Context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(userReceived.username.ToLower()));
            if (user is null)
            {
                _answerAuth.Answer = false;
                _answerAuth.status = "User not found.";
                return _answerAuth;
            }

            if (VerifyPasswordHash(userReceived.password, user.PasswordHash, user.PasswordSalt))
            {
                var empleado = await _Context.Empleados.FirstOrDefaultAsync(u => u.UserId.Equals(user.Id));
                _answerAuth.Answer = true;
                _answerAuth.status = "SUCCESS";
                _answerAuth.sessionToken = CreateToken(userReceived);
                _answerAuth.user = userReceived.username;
                _answerAuth.empleadoId = empleado?.IdEmpleado;
                _answerAuth.nombre = empleado?.Nombre;
            }
            else
            {
                _answerAuth.Answer = false;
                _answerAuth.status = "User not found.";
            }

            return _answerAuth;
        }
        

        public async Task<ServiceResponse<int>> Register(UserDto userDto)
        {
            var response = new ServiceResponse<int>();
            if (await UserExist(userDto.username)) 
            {
                response.Success = false;
                response.Message = "User already Exists";
                return response;
            }
            CreatePasswordHash(userDto.password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();
            user.Username = userDto.username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync(); //Perform the operation in database.
            response.Data = user.Id;
            response.Success = true;
            
            //Guardar un registro en Empleado con el Id recibido.
            
            var empleado = _mapper.Map<Empleado>(userDto);
            empleado.UserId = user.Id;
            empleado.User = user;
            //Empleado empleado = new Empleado();
            //empleado.Nombre = userDto.Nombre;
            //empleado.UserId = user.Id;
            //empleado.Cedula = userDto.Cedula;
            //empleado.
            _Context.Empleados.Add(empleado);
            await _Context.SaveChangesAsync(); //Perform the operation in database.

            return response;
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await _Context.Users.AnyAsync(u => u.Username.ToLower() == userName.ToLower()))
               return true;
            
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        { 
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
