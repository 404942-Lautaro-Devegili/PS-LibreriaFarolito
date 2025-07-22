using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // POST: api/Reserva/GuardarReservas
        [HttpPost("GuardarReservas")]
        public async Task<ActionResult<Dictionary<int, int>>> GuardarReservas([FromBody] List<Reserva> reservas)
        {
            try
            {
                var resultado = await Task.Run(() => _reservaService.GuardarReservas(reservas));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar reservas", error = ex.Message });
            }
        }

        // GET: api/Reserva/TraerReservasXId
        [HttpPost("TraerReservasXId")]
        public async Task<ActionResult<List<Reserva>>> TraerReservasXId([FromBody] List<int> idsReservas)
        {
            try
            {
                var reservas = await Task.Run(() => _reservaService.TraerReservasXId(idsReservas));
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener reservas", error = ex.Message });
            }
        }

        // GET: api/Reserva/TraerReservasXEstado
        [HttpGet("TraerReservasXEstado")]
        public async Task<ActionResult<List<ReservaParaCheckearDTO>>> TraerReservasXEstado(
            string titulo = null,
            int? idSerie = null,
            int? idAutor = null,
            int? idEditorial = null,
            int? idMateria = null,
            int? codigo = null,
            int? idCliente = null,
            int? idReserva = null,
            int sinAsignar = 1,
            int pedida = 1,
            int asignada = 1,
            int entregada = 0)
        {
            try
            {
                Seriee serie = idSerie.HasValue ? new Seriee { Id = idSerie.Value } : null;
                Autore autor = idAutor.HasValue ? new Autore { Id = idAutor.Value } : null;
                Editoriales editorial = idEditorial.HasValue ? new Editoriales { Id = idEditorial.Value } : null;
                Materiaa materia = idMateria.HasValue ? new Materiaa { Id = idMateria.Value } : null;
                Cliente cliente = idCliente.HasValue ? new Cliente { Id = idCliente.Value } : null;

                var reservas = await Task.Run(() => _reservaService.TraerReservasXTituloAutorEditorialXCodigoXClienteXEstadosYOReserva(
                    titulo,
                    serie,
                    autor,
                    editorial,
                    materia,
                    codigo ?? 0,
                    cliente,
                    idReserva ?? 0,
                    sinAsignar,
                    pedida,
                    asignada,
                    entregada));

                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener reservas", error = ex.Message });
            }
        }

        // POST: api/Reserva/ProcesoAnularReservas
        [HttpPost("ProcesoAnularReservas")]
        public async Task<ActionResult<Dictionary<int, int>>> ProcesoAnularReservas([FromBody] List<int> idsReservas)
        {
            try
            {
                var resultado = await Task.Run(() => _reservaService.ProcesoAnularReservas(idsReservas));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al anular reservas", error = ex.Message });
            }
        }

        // POST: api/Reserva/AsignarReservas
        [HttpPost("AsignarReservas")]
        public async Task<ActionResult<Dictionary<int, int>>> AsignarReservas([FromBody] List<int> idsReservas)
        {
            try
            {
                var resultado = await Task.Run(() => _reservaService.AsignarReservas(idsReservas));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar reservas", error = ex.Message });
            }
        }

        // POST: api/Reserva/EntregarReservas
        [HttpPost("EntregarReservas")]
        public async Task<ActionResult<Dictionary<int, int>>> EntregarReservas([FromBody] List<int> idsReservas)
        {
            try
            {
                var resultado = await Task.Run(() => _reservaService.EntregarReservas(idsReservas));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al entregar reservas", error = ex.Message });
            }
        }

        // GET: api/Reserva/TraerMontosReservas
        [HttpPost("TraerMontosReservas")]
        public async Task<ActionResult<List<MontosReservaAEntregarDTO>>> TraerMontosReservas([FromBody] List<int> idsReservas, int formaPago)
        {
            try
            {
                var montos = await Task.Run(() => _reservaService.TraerMontosReservas(idsReservas, formaPago));
                return Ok(montos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener montos", error = ex.Message });
            }
        }

        // GET: api/Reserva/TraerLibrosReservadosParaAsignar
        [HttpGet("TraerLibrosReservadosParaAsignar")]
        public async Task<ActionResult<List<LibroReservadoParaAsignarDTO>>> TraerLibrosReservadosParaAsignar(
            string titulo = null,
            int? idSerie = null,
            int? idAutor = null,
            int? idEditorial = null,
            int? idMateria = null,
            int? codigo = null,
            int? idCliente = null,
            int? idReserva = null)
        {
            try
            {
                Seriee serie = idSerie.HasValue ? new Seriee { Id = idSerie.Value } : null;
                Autore autor = idAutor.HasValue ? new Autore { Id = idAutor.Value } : null;
                Editoriales editorial = idEditorial.HasValue ? new Editoriales { Id = idEditorial.Value } : null;
                Materiaa materia = idMateria.HasValue ? new Materiaa { Id = idMateria.Value } : null;
                Cliente cliente = idCliente.HasValue ? new Cliente { Id = idCliente.Value } : null;

                var libros = await Task.Run(() => _reservaService.TraerLibrosReservadosXTituloAutorEditorialXCodigoXClienteYOReserva(
                    titulo,
                    serie,
                    autor,
                    editorial,
                    materia,
                    codigo ?? 0,
                    cliente,
                    idReserva ?? 0));

                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener libros reservados", error = ex.Message });
            }
        }

        // GET: api/Reserva/TraerLibrosAsignadosParaEntregar
        [HttpGet("TraerLibrosAsignadosParaEntregar")]
        public async Task<ActionResult<List<LibroAsignadoParaEntregarDTO>>> TraerLibrosAsignadosParaEntregar(
            string titulo = null,
            int? idSerie = null,
            int? idAutor = null,
            int? idEditorial = null,
            int? idMateria = null,
            int? codigo = null,
            int? idCliente = null,
            int? idReserva = null)
        {
            try
            {
                Seriee serie = idSerie.HasValue ? new Seriee { Id = idSerie.Value } : null;
                Autore autor = idAutor.HasValue ? new Autore { Id = idAutor.Value } : null;
                Editoriales editorial = idEditorial.HasValue ? new Editoriales { Id = idEditorial.Value } : null;
                Materiaa materia = idMateria.HasValue ? new Materiaa { Id = idMateria.Value } : null;
                Cliente cliente = idCliente.HasValue ? new Cliente { Id = idCliente.Value } : null;

                var libros = await Task.Run(() => _reservaService.TraerLibrosAsignadosXTituloAutorEditorialXCodigoXClienteYOReserva(
                    titulo,
                    serie,
                    autor,
                    editorial,
                    materia,
                    codigo ?? 0,
                    cliente,
                    idReserva ?? 0));

                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener libros asignados", error = ex.Message });
            }
        }

        // POST: api/Reserva/RegistrarPago
        [HttpPost("RegistrarPago")]
        public async Task<ActionResult<List<int>>> RegistrarPago([FromBody] RegistrarPagoDTO pagoData)
        {
            try
            {
                var resultado = await Task.Run(() => _reservaService.RegistrarPago(pagoData));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar pago", error = ex.Message });
            }
        }

        // POST: api/Reserva/VerificarPagoReservas
        [HttpPost("VerificarPagoReservas")]
        public async Task<ActionResult<object>> VerificarPagoReservas([FromBody] List<int> reservasIds)
        {
            try
            {
                var pago = await Task.Run(() => _reservaService.VerificarPagoReservas(reservasIds));

                if (pago != null)
                {
                    return Ok(new { pagado = true, pago = pago });
                }

                return Ok(new { pagado = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al verificar pago", error = ex.Message });
            }
        }
    }
}
