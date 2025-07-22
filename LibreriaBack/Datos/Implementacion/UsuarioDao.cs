using LibreriaBack.Datos.Entidades.Autorización;
using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Implementacion
{
    public class UsuarioDao : HelperDao
    {
        public LoginDTO Login(string usuario, string password)
        {
            try
            {
                LoginDTO ld = new LoginDTO();
                Parametro("PUSUARIO", "S", usuario);
                Parametro("PCONTRASENIA", "S", password);
                Parametro("PLOGUEADO", "N", null, "O");
                Parametro("PIDUSER", "N", null, "O");
                ComandoSP("SP_LOGIN");
                ld.Resultado = ValorParametro("PLOGUEADO") != DBNull.Value ? Convert.ToInt32(ValorParametro("PLOGUEADO")) : -1;
                object pidUserValue = ValorParametro("PIDUSER");
                ld.IdUsuario = pidUserValue != DBNull.Value ? Convert.ToInt32(pidUserValue) : 0;
                LimpiarParametros();
                return ld;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el login", ex);
            }
        }

        public Usuario GetUser(int idUsuario)
        {
            try
            {
                Usuario u = new Usuario();
                Parametro("PIDUSUARIO", "N", idUsuario);
                DataTable t = ObtenerDt("SP_GETUSER");
                LimpiarParametros();
                foreach (DataRow r in t.Rows)
                {
                    u.Id = Convert.ToInt32(r["id"]);
                    u.Nombre = r["nombre"]?.ToString() ?? string.Empty;
                    u.Apellido = r["apellido"]?.ToString() ?? string.Empty;
                    u.User = r["usuario"]?.ToString() ?? string.Empty;
                    u.Password = r["pass"]?.ToString() ?? string.Empty;
                    u.Activo = r["activo"] != DBNull.Value ? Convert.ToBoolean(r["activo"]) : false;
                    u.FechaMod = r["fechaMod"] != DBNull.Value ? Convert.ToDateTime(r["fechaMod"]) : DateTime.Now;
                    u.IdRol = r["idRol"] != DBNull.Value ? Convert.ToInt32(r["idRol"]) : 0;

                    u.Rol = new Rol();
                    u.Rol.Id = r["idRol"] != DBNull.Value ? Convert.ToInt32(r["idRol"]) : 0;
                    u.Rol.Nombre = r["rol"]?.ToString() ?? string.Empty;
                    u.Rol.Descripcion = r["descripcion"]?.ToString() ?? string.Empty;
                    u.Rol.Estado = r["estado"] != DBNull.Value ? Convert.ToBoolean(r["estado"]) : false;
                }
                return u;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al devolver el usuario", ex);
            }
        }
        public List<MontosReservaAEntregarDTO> GetMontosReservas(List<int> lr, int formaPago)
        {
            List<MontosReservaAEntregarDTO> lm = new List<MontosReservaAEntregarDTO>();
            foreach (int re in lr)
            {
                Parametro("PIDRESERVA", "N", re);
                Parametro("PFORMAPAGO", "N", formaPago);
                DataTable t = ObtenerDt("SP_GETMONTOSXRESERVA");
                LimpiarParametros();
                foreach (DataRow r in t.Rows)
                {
                    MontosReservaAEntregarDTO p = new MontosReservaAEntregarDTO();
                    p.IdReserva = Convert.ToInt32(r["id"]);
                    p.IdLibro = Convert.ToInt32(r["id_libro"]);
                    p.Senia = Math.Round(Convert.ToDecimal(r["senia"]), 2);
                    p.Saldo = Math.Round(Convert.ToDecimal(r["saldo"]), 2);
                    p.PrecioLibro = Math.Round(Convert.ToDecimal(r["precioLibro"]), 2);
                    p.ContadoLibro = Math.Round(Convert.ToDecimal(r["contadoLibro"]), 2);
                    p.formaPago = Convert.ToInt32(r["formaPago"]);
                    lm.Add(p);
                }
            }

            return lm;
        }

        public List<int> SaveReserva(Reserva r)
        {
            Parametro("PIDRESERVA", "N", null, "O");
            Parametro("PIDLIBRO", "N", r.IdLibro);
            Parametro("PIDCLIENTE", "N", r.IdCliente);
            Parametro("PIDPROVEEDOR", "N", r.IdProveedor);
            Parametro("PSEÑA", "N", r.Senia);
            Parametro("PSALDO", "N", r.Saldo);
            Parametro("PFCHRESERVA", "F", r.Fchreserva);
            Parametro("PIDFORMAPAGO", "N", r.IdFormaPago);
            Parametro("PLIBROPEDIDO", "B", r.LibroPedido);
            Parametro("PENTREGADA", "B", r.Entregada);
            Parametro("PACTIVA", "B", r.Activa);
            ComandoSP("SP_GUARDARRESERVA");
            AddResultados("PIDRESERVA");

            LimpiarParametros();
            return listaResultados;
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                DataTable t = ObtenerDt("SP_GETUSUARIOS");
                foreach (DataRow r in t.Rows)
                {
                    Usuario u = new Usuario();
                    u.Id = Convert.ToInt32(r["id"]);
                    u.Nombre = r["nombre"].ToString();
                    u.Apellido = r["apellido"].ToString();
                    u.User = r["usuario"].ToString();
                    u.Password = r["pass"].ToString();
                    u.Activo = Convert.ToBoolean(r["activo"]);
                    u.FechaMod = Convert.ToDateTime(r["fechaMod"]);
                    u.IdRol = Convert.ToInt32(r["idRol"]);

                    u.Rol = new Rol();
                    u.Rol.Id = Convert.ToInt32(r["idRol"]);
                    u.Rol.Nombre = r["rol"].ToString();
                    u.Rol.Descripcion = r["descripcion"].ToString();
                    u.Rol.Estado = Convert.ToBoolean(r["estado"]);

                    usuarios.Add(u);
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios", ex);
            }
        }

        public List<int> SaveUsuario(Usuario usuario)
        {
            try
            {
                Parametro("PIDUSUARIO", "N", null, "O");
                Parametro("PNOMBRE", "S", usuario.Nombre);
                Parametro("PAPELLIDO", "S", usuario.Apellido);
                Parametro("PUSUARIO", "S", usuario.User);
                Parametro("PPASS", "S", usuario.Password);
                Parametro("PACTIVO", "B", usuario.Activo);
                Parametro("PIDROL", "N", usuario.IdRol);

                ComandoSP("SP_GUARDARUSUARIO");
                AddResultados("PIDUSUARIO");

                LimpiarParametros();
                return listaResultados;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar usuario", ex);
            }
        }

        public int ModificarUsuario(Usuario usuario)
        {
            try
            {
                Parametro("PID", "N", usuario.Id);
                Parametro("PNOMBRE", "S", usuario.Nombre);
                Parametro("PAPELLIDO", "S", usuario.Apellido);
                Parametro("PUSUARIO", "S", usuario.User);
                Parametro("PPASS", "S", usuario.Password);
                Parametro("PACTIVO", "B", usuario.Activo);
                Parametro("PIDROL", "N", usuario.IdRol);

                ComandoSP("SP_MODIFICARUSUARIO");
                LimpiarParametros();

                return afectadas > 0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario", ex);
            }
        }

        public List<Rol> GetRoles()
        {
            List<Rol> roles = new List<Rol>();
            try
            {
                DataTable t = ObtenerDt("SP_GETROLES");
                foreach (DataRow r in t.Rows)
                {
                    Rol rol = new Rol();
                    rol.Id = Convert.ToInt32(r["id"]);
                    rol.Nombre = r["nombre"].ToString();
                    rol.Descripcion = r["descripcion"].ToString();
                    rol.Estado = Convert.ToBoolean(r["estado"]);
                    roles.Add(rol);
                }
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener roles", ex);
            }
        }
    }
}
