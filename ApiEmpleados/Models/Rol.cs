namespace ApiEmpleados.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string RolName { get; set; } = string.Empty;
        public virtual ICollection<User>? Users { get; set; }
    }
}
