using LibreriaBack.Datos.Entidades.Autorización;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioDao ud;
        public UsuarioService()
        {
            ud = new UsuarioDao();
        }

        public Usuario Login(string usuario, string password)
        {
            LoginDTO res = ud.Login(usuario, password);
            if (res != null)
            {
                switch (res.Resultado)
                {
                    case 1:
                        return ud.GetUser(res.IdUsuario);
                    case 0:
                        throw new Exception("Usuario o contraseña incorrectos.");
                    case -1:
                        throw new Exception("El usuario no está activo.");
                    case -2:
                        throw new Exception("El usuario no existe.");
                    default:
                        throw new Exception("Hubo un error al hacer el login. Por favor, espere unos minutos e intente nuevamente.");
                }
            } 
            else {
                throw new Exception("Hubo un error al hacer el login. Por favor, espere unos minutos e intente nuevamente.");
            }
        }

        public List<Usuario> TraerUsuarios()
        {
            return ud.GetUsuarios();
        }

        public List<int> GuardarUsuario(Usuario usuario)
        {
            return ud.SaveUsuario(usuario);
        }

        public int ModificarUsuario(Usuario usuario)
        {
            return ud.ModificarUsuario(usuario);
        }

        public List<Rol> TraerRoles()
        {
            return ud.GetRoles();
        }
    }
}
