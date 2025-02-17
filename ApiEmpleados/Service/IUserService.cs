using ApiEmpleados.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ApiEmpleados.Service
{
    public interface IUserService
    {
        AnswerAuthentication Login(UserDto userReceived);

    }
}
