using ApiEmpleados.Models;

namespace ApiEmpleados.Data
{
    public class Db<T>: IDb<T>
    {
        private static bool isSeeded = false;
        private Repository<T> repository = new Repository<T>();
        private readonly IServiceProvider serviceProvider;
        public Db(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
            if (!isSeeded)
            {
                SeedRepository(repository).Wait();
                isSeeded = true;
            }
        }
        
        public Repository<T> GetRepo() {
            if (repository == null) {
                repository = new Repository<T>();
            }
            
            return repository;
        }
        private async Task SeedRepository(Repository<T> repository)
        {
            try
            {
                if (isSeeded) return;
                isSeeded = true;
                if (typeof(T) == typeof(Pago))
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var empleadoService = scope.ServiceProvider.GetRequiredService<IEmpleadoService>();
                        var empleados = await empleadoService.GetAllEmpleados();
                        foreach (var empleado in empleados)
                        {
                            var pagos = new List<Pago>();
                            for (int i = 0; i < 3; i++)
                            {
                                var pago = new Pago() { Fecha = DateTime.Now.AddDays(-6), Valor = new Random().NextInt64(), IdEmpleado = empleado.IdEmpleado };
                                pagos.Add(pago);
                            }
                            if (repository is Repository<Pago> pagoRepo)
                            {
                                foreach (var p in pagos)
                                {
                                    pagoRepo.Set(empleado.IdEmpleado, p);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
