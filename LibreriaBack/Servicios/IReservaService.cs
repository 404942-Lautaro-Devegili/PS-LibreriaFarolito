using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;

namespace LibreriaBack.Servicios
{
    public interface IReservaService
    {
        List<int> GuardarReserva(Reserva r);
        Dictionary<int, int> GuardarReservas(List<Reserva> lr);
        List<LibroReservadoParaAsignarDTO> TraerLibrosReservadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva);
        List<LibroAsignadoParaEntregarDTO> TraerLibrosAsignadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva);
        Dictionary<int, int> AsignarReservas(List<int> lr);
        Dictionary<int, int> EntregarReservas(List<int> lr);
        List<MontosReservaAEntregarDTO> TraerMontosReservas(List<int> lr, int formaPago);
        public List<ReservaParaCheckearDTO> TraerReservasXTituloAutorEditorialXCodigoXClienteXEstadosYOReserva(string titulo, Seriee serie, Autore autor,
                                                                                                                   Editoriales editorial, Materiaa materia,
                                                                                                                   int codigo, Cliente cliente, int reserva,
                                                                                                                   int sinAsignar, int pedida, int asignada,
                                                                                                                   int entregada);
        Dictionary<int, int> ProcesoAnularReservas(List<int> lr);
        List<Reserva> TraerReservasXId(List<int> idsReservas);
        List<int> RegistrarPago(RegistrarPagoDTO pagoData);
        Pago? VerificarPagoReservas(List<int> reservasIds);
    }
}
