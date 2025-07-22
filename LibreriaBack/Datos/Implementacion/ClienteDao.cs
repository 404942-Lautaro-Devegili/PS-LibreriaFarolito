using LibreriaBack.Datos.Entidades.Logistica;
using System.Data;

namespace LibreriaBack.Datos.Implementacion
{
    public class ClienteDao : HelperDao
    {
        public List<int> SaveCliente(Cliente c)
        {
            Parametro("PCODIGO", "N", null, "O");
            Parametro("PNOMBRE", "S", c.Nombre);
            Parametro("PTELEFONO", "S", c.Telefono);
            Parametro("PDIRECCION", "S", c.Direccion);
            Parametro("PIDLOCALIDAD", "N", c.IdLocalidad);
            Parametro("PEMAIL", "S", c.Email);
            ComandoSP("SP_GUARDARCLIENTE");
            AddResultados("PCODIGO");

            LimpiarParametros();
            return listaResultados;
        }

        public int ModificarCLiente(Cliente c)
        {
            Parametro("PID", "N", c.Id);
            Parametro("PNOMBRE", "S", c.Nombre);
            Parametro("PTELEFONO", "S", c.Telefono);
            Parametro("PDIRECCION", "S", c.Direccion);
            Parametro("PIDLOCALIDAD", "N", c.IdLocalidad);
            Parametro("PEMAIL", "S", c.Email);
            ComandoSP("SP_MODIFICARCLIENTE");
            LimpiarParametros();
            return ResultadoSP();
        }

        public List<Cliente> GetClientes()
        {
            DataTable Dt = ObtenerDt("SP_TRAERCLIENTES");
            List<Cliente> clientes = new List<Cliente>();
            if (HuboErrores)
            {
                clientes = null;
            }
            else
            {
                foreach (DataRow item in Dt.Rows)
                {
                    Cliente c = new Cliente();
                    c.Id = int.Parse(item["ID"].ToString());
                    c.Codigo = int.Parse(item["CODIGO"].ToString());
                    c.Nombre = item["NOMBRE"].ToString();
                    c.Telefono = item["TELEFONO"].ToString();
                    c.Direccion = item["DIRECCION"].ToString();
                    c.IdLocalidad = int.Parse(item["ID_LOCALIDAD"].ToString());
                    c.Localidad = item["LOCALIDAD"].ToString();
                    c.Email = item["EMAIL"].ToString();
                    clientes.Add(c);
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return clientes;
        }

        public List<Cliente> GetClientesXNombreOCodigo(string nombre)
        {
            Parametro("PNOMBRE", "S", nombre);
            DataTable Dt = ObtenerDt("SP_TRAERCLIENTESXNOMBRE");
            List<Cliente> clientes = new List<Cliente>();
            if (HuboErrores)
            {
                clientes = null;
            }
            else
            {
                foreach (DataRow item in Dt.Rows)
                {
                    Cliente c = new Cliente();
                    c.Id = int.Parse(item["ID"].ToString());
                    c.Codigo = int.Parse(item["CODIGO"].ToString());
                    c.Nombre = item["NOMBRE"].ToString();
                    c.Telefono = item["TELEFONO"].ToString();
                    c.Direccion = item["DIRECCION"].ToString();
                    c.IdLocalidad = int.Parse(item["ID_LOCALIDAD"].ToString());
                    c.Localidad = item["LOCALIDAD"].ToString();
                    c.Email = item["EMAIL"].ToString();
                    clientes.Add(c);
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return clientes;
        }

        public Cliente GetClienteXId(int id)
        {
            Parametro("PIDCLIENTE", "S", id);
            DataTable Dt = ObtenerDt("SP_TRAERCLIENTEXID");
            Cliente c = new Cliente();
            if (HuboErrores)
            {
                c = null;
            }
            else
            {
                if (Dt.Rows.Count > 0)
                {
                    c.Id = int.Parse(Dt.Rows[0]["ID"].ToString());
                    c.Codigo = int.Parse(Dt.Rows[0]["CODIGO"].ToString());
                    c.Nombre = Dt.Rows[0]["NOMBRE"].ToString();
                    c.Telefono = Dt.Rows[0]["TELEFONO"].ToString();
                    c.Direccion = Dt.Rows[0]["DIRECCION"].ToString();
                    c.IdLocalidad = int.Parse(Dt.Rows[0]["ID_LOCALIDAD"].ToString());
                    c.Localidad = Dt.Rows[0]["LOCALIDAD"].ToString();
                    c.Email = Dt.Rows[0]["EMAIL"].ToString();
                }
                else
                {
                    c = null;
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return c;
        }

        public Cliente GetClienteXCodigo(int codigo)
        {
            Parametro("PCODIGO", "S", codigo.ToString());
            DataTable Dt = ObtenerDt("SP_TRAERCLIENTEXCODIGO");
            Cliente c = new Cliente();
            if (HuboErrores)
            {
                c = null;
            }
            else
            {
                if (Dt.Rows.Count > 0)
                {
                    c.Id = int.Parse(Dt.Rows[0]["ID"].ToString());
                    c.Codigo = int.Parse(Dt.Rows[0]["CODIGO"].ToString());
                    c.Nombre = Dt.Rows[0]["NOMBRE"].ToString();
                    c.Telefono = Dt.Rows[0]["TELEFONO"].ToString();
                    c.Direccion = Dt.Rows[0]["DIRECCION"].ToString();
                    c.IdLocalidad = int.Parse(Dt.Rows[0]["ID_LOCALIDAD"].ToString());
                    c.Localidad = Dt.Rows[0]["LOCALIDAD"].ToString();
                    c.Email = Dt.Rows[0]["EMAIL"].ToString();
                }
                else
                {
                    c = null;
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return c;
        }

        public Cliente GetClienteXTelefono(string telefono)
        {
            Parametro("PTELEFONO", "S", telefono);
            DataTable Dt = ObtenerDt("SP_TRAERCLIENTEXTELEFONO");
            Cliente c = new Cliente();
            if (HuboErrores)
            {
                c = null;
            }
            else
            {
                if (Dt.Rows.Count > 0)
                {
                    c.Id = int.Parse(Dt.Rows[0]["ID"].ToString());
                    c.Codigo = int.Parse(Dt.Rows[0]["CODIGO"].ToString());
                    c.Nombre = Dt.Rows[0]["NOMBRE"].ToString();
                    c.Telefono = Dt.Rows[0]["TELEFONO"].ToString();
                    c.Direccion = Dt.Rows[0]["DIRECCION"].ToString();
                    c.IdLocalidad = int.Parse(Dt.Rows[0]["ID_LOCALIDAD"].ToString());
                    c.Localidad = Dt.Rows[0]["LOCALIDAD"].ToString();
                    c.Email = Dt.Rows[0]["EMAIL"].ToString();
                }
                else
                {
                    c = null;
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return c;
        }

        public int GetNextCodigo()
        {
            Parametro("PCODIGO", "N", null, "O");
            ComandoSP("SP_TRAERNEXTCODIGOCLIENTE");
            int codigo;
            if (HuboErrores)
            {
                codigo = -1;
            }
            else
            {
                codigo = int.Parse(ValorParametro("PCODIGO").ToString());
            }
            LimpiarErrores();
            LimpiarParametros();
            return codigo;
        }

        public int GetIdClientePorCodigo(int codigo)
        {
            Parametro("PID", "N", null, "O");
            Parametro("PCODIGO", "N", codigo);
            ComandoSP("SP_TRAERIDCLIENTEXCODIGO");
            int id;
            if (HuboErrores)
            {
                id = -1;
            }
            else
            {
                id = int.Parse(ValorParametro("PID").ToString());
            }
            LimpiarErrores();
            LimpiarParametros();
            return id;
        }

        public List<Localidade> GetLocalidades()
        {
            DataTable Dt = ObtenerDt("SP_TRAERLOCALIDADES");
            List<Localidade> localidades = new List<Localidade>();
            if (HuboErrores)
            {
                localidades = null;
            }
            else
            {
                foreach (DataRow item in Dt.Rows)
                {
                    Localidade l = new Localidade();
                    l.Id = int.Parse(item["ID"].ToString());
                    l.Localidad = item["LOCALIDAD"].ToString();
                    localidades.Add(l);
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return localidades;
        }

        public List<FormasPago> GetFormasPago()
        {
            DataTable Dt = ObtenerDt("SP_TRAERFORMASPAGO");
            List<FormasPago> formasPago = new List<FormasPago>();
            if (HuboErrores)
            {
                formasPago = null;
            }
            else
            {
                foreach (DataRow item in Dt.Rows)
                {
                    FormasPago f = new FormasPago();
                    f.Id = int.Parse(item["ID"].ToString());
                    f.FormaPago = item["FORMAPAGO"].ToString();
                    formasPago.Add(f);
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return formasPago;
        }

        public List<FormasPago> GetFormasPagoConPorcentaje()
        {
            DataTable Dt = ObtenerDt("SP_TRAERFORMASPAGO_CON_PORCENTAJE");
            List<FormasPago> formasPago = new List<FormasPago>();
            if (HuboErrores)
            {
                formasPago = null;
            }
            else
            {
                foreach (DataRow item in Dt.Rows)
                {
                    FormasPago f = new FormasPago();
                    f.Id = int.Parse(item["ID"].ToString());
                    f.FormaPago = item["FORMAPAGO"].ToString();
                    f.PorcentajeRecargo = decimal.Parse(item["porcentajeRecargo"].ToString());
                    f.SeniaMinima = item["seniaMinima"] != DBNull.Value ? decimal.Parse(item["seniaMinima"].ToString()) : 0;
                    formasPago.Add(f);
                }
            }
            LimpiarErrores();
            LimpiarParametros();
            return formasPago;
        }

        public int ActualizarPorcentajeFormaPago(int idFormaPago, decimal porcentajeRecargo)
        {
            Parametro("PID_FORMA_PAGO", "N", idFormaPago);
            Parametro("PPORCENTAJE_RECARGO", "N", porcentajeRecargo);
            ComandoSP("SP_ACTUALIZAR_PORCENTAJE_FORMA_PAGO");
            int resultado = ResultadoSP();
            LimpiarParametros();
            return resultado;
        }

        public int ActualizarFormaPagoCompleto(int idFormaPago, decimal porcentajeRecargo, decimal seniaMinima)
        {
            Parametro("PID_FORMA_PAGO", "N", idFormaPago);
            Parametro("PPORCENTAJE_RECARGO", "N", porcentajeRecargo);
            Parametro("PSENIA_MINIMA", "N", seniaMinima);
            ComandoSP("SP_ACTUALIZAR_FORMA_PAGO_COMPLETO");
            int resultado = ResultadoSP();
            LimpiarParametros();
            return resultado;
        }
    }
}
