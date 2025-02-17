using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados.Repository
{
    public interface IAuthRepository
    {
        string CreateToken(UserAuthDto user);
        Task<ServiceResponse<int>> Register(UserDto userDto);
        Task<AnswerAuthentication> Login(UserAuthDto userReceived);
        Task<bool> UserExist(string userName);
    }
}
