using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.Autorización
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaMod { get; set; }
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }
    }
}
