using LibreriaBack.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static LibreriaBack.Datos.Entidades.DTOs.EstadisticasDTO;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly IEstadisticasService _estadisticasService;

        public EstadisticasController(IEstadisticasService estadisticasService)
        {
            _estadisticasService = estadisticasService;
        }

        // POST: api/Estadisticas/ObtenerEstadisticas
        [HttpPost("ObtenerEstadisticas")]
        public async Task<ActionResult<EstadisticasResponseDTO>> ObtenerEstadisticas([FromBody] EstadisticasParametrosDTO parametros)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Usuario no autenticado" });
                }

                if (parametros == null)
                {
                    return BadRequest(new { message = "Los parámetros son requeridos" });
                }

                if (string.IsNullOrEmpty(parametros.Tipo))
                {
                    return BadRequest(new { message = "El tipo de estadística es requerido" });
                }

                if (parametros.Tipo.ToLower() != "pedidos" && parametros.Tipo.ToLower() != "reservas")
                {
                    return BadRequest(new { message = "El tipo debe ser 'pedidos' o 'reservas'" });
                }

                var estadisticas = await Task.Run(() => _estadisticasService.ObtenerEstadisticas(parametros));

                return Ok(estadisticas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EstadisticasController.ObtenerEstadisticas: {ex.Message}");
                return StatusCode(500, new
                {
                    message = "Error interno del servidor al obtener estadísticas",
                    error = ex.Message
                });
            }
        }

        // GET: api/Estadisticas/TraerDistribuidores
        [HttpGet("TraerDistribuidores")]
        public async Task<ActionResult<List<object>>> TraerDistribuidores()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Usuario no autenticado" });
                }

                var pedidoService = HttpContext.RequestServices.GetService<IPedidoService>();
                var distribuidores = await Task.Run(() => pedidoService.TraerProveedores());

                var distribuidoresSimplificados = distribuidores.Select(d => new
                {
                    id = d.Id,
                    nombre = d.Proveedor
                }).ToList();

                return Ok(distribuidoresSimplificados);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EstadisticasController.TraerDistribuidores: {ex.Message}");
                return StatusCode(500, new
                {
                    message = "Error al obtener distribuidores",
                    error = ex.Message
                });
            }
        }

        // GET: api/Estadisticas/TraerMetodosPago
        [HttpGet("TraerMetodosPago")]
        public async Task<ActionResult<List<object>>> TraerMetodosPago()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Usuario no autenticado" });
                }

                var clienteService = HttpContext.RequestServices.GetService<IClienteService>();
                var metodosPago = await Task.Run(() => clienteService.TraerFormasPago());

                var metodosPagoSimplificados = metodosPago.Select(m => new
                {
                    id = m.Id,
                    nombre = m.FormaPago
                }).ToList();

                return Ok(metodosPagoSimplificados);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EstadisticasController.TraerMetodosPago: {ex.Message}");
                return StatusCode(500, new
                {
                    message = "Error al obtener métodos de pago",
                    error = ex.Message
                });
            }
        }
    }
}
