using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Logistica;

namespace LibreriaBack.Servicios
{
    public interface IClienteService
    {
        List<int> GuardarCliente(Cliente c);
        List<int> GuardarOModificarCliente(Cliente cliente);
        List<Cliente> TraerClientes();
        int CheckearCliente(Cliente cliente);
        Cliente EstaEnBD(string codigo);
        int TraerNextCodigo();
        List<Cliente> TraerClientesXNombre(string nombre);
        Cliente TraerClienteXId(int id);
        Cliente TraerClienteXCodigo(int codigo);
        Cliente TraerClienteXTelefono(string telefono);
        List<Localidade> TraerLocalidades();
        List<FormasPago> TraerFormasPago();
        List<FormasPago> TraerFormasPagoConPorcentaje();
        int ActualizarPorcentajesFormasPago(List<FormaPagoActualizacionDTO> metodosPago);
    }
}
