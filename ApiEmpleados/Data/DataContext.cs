

using ApiEmpleados.Models;

namespace ApiEmpleados.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {

        }
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Registro> Registros => Set<Registro>();
        public DbSet<Proyecto> Proyectos => Set<Proyecto>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Rol> Roles => Set<Rol>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, RolName = "Empleado"},
                new Rol { Id = 2, RolName = "Administrador" },
                new Rol { Id = 3, RolName = "Gerente" }
                );
            
        }
        
    }
}
