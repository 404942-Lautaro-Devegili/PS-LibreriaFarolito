using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Datos.Implementacion;

namespace LibreriaBack.Servicios.Implementacion
{
    public class ReservaService : IReservaService
    {
        ReservaDao rd;
        public ReservaService()
        {
            rd = new ReservaDao();
        }

        public List<int> GuardarReserva(Reserva r)
        {
            return rd.SaveReserva(r);
        }

        public Dictionary<int, int> GuardarReservas(List<Reserva> lr)
        {
            return rd.SaveReservas(lr);
        }

        public List<LibroReservadoParaAsignarDTO> TraerLibrosReservadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva)
        {
            List<LibroReservadoParaAsignarDTO> librosReservadosParaAsignar = rd.GetLibrosReservadosXTituloAutorEditorialXCodigoXClienteYOReserva(titulo, serie, autor, editorial, materia, codigo, cliente, reserva);
            return librosReservadosParaAsignar;
        }

        public List<LibroAsignadoParaEntregarDTO> TraerLibrosAsignadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva)
        {
            List<LibroAsignadoParaEntregarDTO> librosReservadosParaAsignar = rd.GetLibrosAsignadosXTituloAutorEditorialXCodigoXClienteYOReserva(titulo, serie, autor, editorial, materia, codigo, cliente, reserva);
            return librosReservadosParaAsignar;
        }

        public Dictionary<int, int> AsignarReservas(List<int> lr)
        {
            return rd.AsignarReservas(lr);
        }

        public Dictionary<int, int> EntregarReservas(List<int> lr)
        {
            return rd.EntregarReservas(lr);
        }

        public List<MontosReservaAEntregarDTO> TraerMontosReservas(List<int> lr, int formaPago)
        {
            return rd.GetMontosReservas(lr, formaPago);
        }

        public List<ReservaParaCheckearDTO> TraerReservasXTituloAutorEditorialXCodigoXClienteXEstadosYOReserva(string titulo, Seriee serie, Autore autor,
                                                                                                                   Editoriales editorial, Materiaa materia,
                                                                                                                   int codigo, Cliente cliente, int reserva,
                                                                                                                   int sinAsignar, int pedida, int asignada,
                                                                                                                   int entregada)
        {
            return rd.GetReservasXTituloAutorEditorialXCodigoXClienteXEstadosYOReserva(titulo, serie, autor,
                                                                                     editorial, materia, codigo,
                                                                                     cliente, reserva, sinAsignar,
                                                                                     pedida, asignada, entregada);
        }

        public Dictionary<int, int> ProcesoAnularReservas(List<int> lr)
        {
            return rd.ProcesoAnularReservas(lr);
        }

        public List<Reserva> TraerReservasXId(List<int> idsReservas)
        {
            return rd.GetReservasXId(idsReservas);
        }
        public List<int> RegistrarPago(RegistrarPagoDTO pagoData)
        {
            return rd.RegistrarPago(pagoData);
        }

        public Pago? VerificarPagoReservas(List<int> reservasIds)
        {
            return rd.VerificarPagoReservas(reservasIds);
        }
    }
}
