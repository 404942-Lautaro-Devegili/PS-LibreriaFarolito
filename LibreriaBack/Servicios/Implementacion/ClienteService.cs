using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Datos.Implementacion;

namespace LibreriaBack.Servicios.Implementacion
{
    public class ClienteService : IClienteService
    {
        ClienteDao cd;

        public ClienteService()
        {
            cd = new ClienteDao();
        }
        public List<int> GuardarCliente(Cliente c)
        {
            return cd.SaveCliente(c);
        }

        public List<Cliente> TraerClientes()
        {
            return cd.GetClientes();
        }
        public List<int> GuardarOModificarCliente(Cliente cliente)
        {
            //Opcion 1: Guardar. Opcion 2: Modificar
            int opcion = CheckearCliente(cliente);
            List<int> ints = new List<int>();
            if (opcion == 1)
            {
                ints = GuardarCliente(cliente);
                ints.Add(TraerIdClienteXCodigo(int.Parse(cliente.Codigo.ToString())));
            }
            else if (opcion == 0)
            {
                ints.Add(cd.ModificarCLiente(cliente));
                ints.Add(int.Parse(cliente.Codigo.ToString()));
                ints.Add(TraerIdClienteXCodigo(int.Parse(cliente.Codigo.ToString())));
            }
            else if (opcion == 2)
            {
                ints.Add(0);
                ints.Add(int.Parse(cliente.Codigo.ToString()));
                ints.Add(TraerIdClienteXCodigo(int.Parse(cliente.Codigo.ToString())));
            }
            return ints; // [0] = 1: Guardado o modificado. [0] = -1: Error. [0] = 0: Sin cambios. [1]: Codigo guardado. [1] = -1: Error.
        }

        public Cliente EstaEnBD(string codigo)
        {
            return cd.GetClienteXCodigo(int.Parse(codigo));
        }

        public int CheckearCliente(Cliente cliente)
        {
            string codigo = cliente.Codigo.ToString();
            if (!string.IsNullOrEmpty(codigo) && codigo != "-9")
            {
                Cliente cliEnBase = EstaEnBD(codigo);
                if (cliEnBase != null)
                {
                    cliente.Id = cliEnBase.Id;
                    if (!cliente.Equals(cliEnBase))
                    {
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -9;
            }
        }

        public int TraerNextCodigo()
        {
            return cd.GetNextCodigo();
        }

        public int TraerIdClienteXCodigo(int codigo)
        {
            return cd.GetIdClientePorCodigo(codigo);
        }

        public List<Cliente> TraerClientesXNombre(string nombre)
        {
            return cd.GetClientesXNombreOCodigo(nombre);
        }

        public Cliente TraerClienteXId(int id)
        {
            return cd.GetClienteXId(id);
        }

        public Cliente TraerClienteXCodigo(int codigo)
        {
            return cd.GetClienteXCodigo(codigo);
        }

        public Cliente TraerClienteXTelefono(string telefono)
        {
            return cd.GetClienteXTelefono(telefono);
        }

        public List<Localidade> TraerLocalidades()
        {
            return cd.GetLocalidades();
        }

        public List<FormasPago> TraerFormasPago()
        {
            return cd.GetFormasPago();
        }

        public List<FormasPago> TraerFormasPagoConPorcentaje()
        {
            return cd.GetFormasPagoConPorcentaje();
        }

        public int ActualizarPorcentajesFormasPago(List<FormaPagoActualizacionDTO> metodosPago)
        {
            try
            {
                foreach (var metodo in metodosPago)
                {
                    var resultado = cd.ActualizarFormaPagoCompleto(metodo.Id, metodo.PorcentajeRecargo, metodo.SeniaMinima);
                    if (resultado != 1)
                    {
                        return -1; // Si alguno falla, devolvemos error
                    }
                }
                return 1; // Todo salió bien
            }
            catch
            {
                return -1;
            }
        }
    }
}
