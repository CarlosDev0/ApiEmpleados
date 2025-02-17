using ApiEmpleados.Dtos;
using ApiEmpleados.Repository;

namespace ApiEmpleados.Service
{
    public class UserService 
    {
        private IAuthRepository _authRepository;

        public UserService(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }
        
    }
}
