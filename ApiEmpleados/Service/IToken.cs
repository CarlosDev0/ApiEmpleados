using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace ApiEmpleados.Service
{
    public interface IToken
    {
        bool ValidateToken(string authToken);
        
    }
}