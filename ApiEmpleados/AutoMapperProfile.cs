using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Empleado, GetEmpleadoDto>();
            CreateMap<GetEmpleadoDto, Empleado>();
            CreateMap<AddEmpleadoDto, Empleado>();
            CreateMap<AddRegistroDto, Registro>();
            CreateMap<Registro, RegistroEmpleadoDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDto, Empleado>();
            


        }
    }
}
