using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Puede ser "Admin", "User", etc.

        // Constructor por defecto
        public User() { }

        // Constructor para crear un usuario
        public User(string email, string passwordHash, string role = "User")
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
