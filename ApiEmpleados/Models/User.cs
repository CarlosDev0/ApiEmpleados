﻿namespace ApiEmpleados.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public virtual ICollection<Rol>? Roles { get; set; }
        public Empleado? Empleado { get; set; } 
    }
}
