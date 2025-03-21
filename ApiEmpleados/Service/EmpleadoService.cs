﻿global using AutoMapper;
using ApiEmpleados.Dtos;
using ApiEmpleados.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEmpleados.Service
{

    public class EmpleadoService : IEmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public EmpleadoService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<IEnumerable<GetEmpleadoDto>> GetAllEmpleados()
        {
            var dbEmpleados = await _context.Empleados.ToListAsync();
            var newdbE = dbEmpleados.Select(c => _mapper.Map<GetEmpleadoDto>(c)).ToList();
            return newdbE;
        }

        public async Task<IEnumerable<GetEmpleadoDto>> GetEmpleadoById(Guid idEmpleado)
        {
            var dbEmpleados = await _context.Empleados.Where(c=> c.IdEmpleado == idEmpleado).ToListAsync();
            return dbEmpleados.Select(c => _mapper.Map<GetEmpleadoDto>(c)).ToList();
        }


        public async Task<ServiceResponse<GetEmpleadoDto>> AddEmpleado(AddEmpleadoDto newRegistro)
        {
            var answer = new ServiceResponse<GetEmpleadoDto>();
            try
            {
                var user = _context.Users.Where(c => c.Username.ToLower() == newRegistro!.UserName!.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    var empleado = _context.Empleados.Where(c => c.UserId == user.Id).FirstOrDefault();
                    if (empleado is null)
                    {
                        var registro = _mapper.Map<Empleado>(newRegistro);
                        registro.UserId = user.Id;
                        _context.Empleados.Add(registro);
                        await _context.SaveChangesAsync(); //Perform the operation in database.
                        answer.Success = true;
                        answer.Data = _mapper.Map<GetEmpleadoDto>(
                        _context.Empleados.Where(c => c.IdEmpleado == registro!.IdEmpleado).FirstOrDefault()!); //Corregido. Probar
                        return answer;
                    }
                    else {
                        answer.Message = "The user was already created as employee.";
                        return answer;
                    }
                }
                answer.Message = "Username not found";
                return answer;
            }
            catch (Exception ex)
            {
                answer.Success = false;
                if (ex.HResult == -2146233088)
                    answer.Message = "The user was already created as employee.";
                else
                    answer.Message = ex.InnerException!.ToString();
                return answer;
            }
        }
    }
}
