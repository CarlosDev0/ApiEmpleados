using ApiEmpleados.Dtos;
using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public class ProyectoService: IProyectoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public ProyectoService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<IEnumerable<Proyecto>>> GetAllProyectos()
        {
            var answer = new ServiceResponse<IEnumerable<Proyecto>>();
            var dbProyectos = await _context.Proyectos.ToListAsync();
            var newdbE = dbProyectos.Select(c => _mapper.Map<Proyecto>(c)).ToList();
            answer.Data = newdbE;
            answer.Success = true;
            return answer;
        }
    }
}
