using LibreriaBack.Datos.Entidades.Autorización;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Servicios
{
    public interface IUsuarioService
    {
        Usuario Login(string usuario, string password);
        List<Usuario> TraerUsuarios();
        List<int> GuardarUsuario(Usuario usuario);
        int ModificarUsuario(Usuario usuario);
        List<Rol> TraerRoles();
    }
}
