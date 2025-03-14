using ApiEmpleados.Models;

namespace ApiEmpleados.DependencyInjection
{
    public static class ApiEmpleadosDependencyInjection
    {
        public static void AddApiEmpleadosServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            if (serviceProvider.GetService<IConfiguration>() is not IConfigurationRoot configuration) return;
            
            services.AddSingleton<IDb<Pago>, Db<Pago>>();
            //services.SeedData(configuration);
            services.AddScoped<IPagoService, PagoService>();
            
        }
        //private static void SeedData(this IServiceCollection services, IConfiguration configuration)
        //{
            
        ////var serviceProvider = services.BuildServiceProvider();
        ////var es = serviceProvider.GetRequiredService<IEmpleadoService>();
        //    using (var scope = _empleadoService.CreateScope())
        //{
        //}
        //}
    }
}
