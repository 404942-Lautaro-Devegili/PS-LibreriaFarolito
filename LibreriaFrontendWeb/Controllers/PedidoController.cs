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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/Pedido/TraerProveedores
        [HttpGet("TraerProveedores")]
        public async Task<ActionResult<List<Proveedore>>> TraerProveedores()
        {
            try
            {
                var proveedores = await Task.Run(() => _pedidoService.TraerProveedores());
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener proveedores", error = ex.Message });
            }
        }

        // GET: api/Pedido/TraerLibrosParaPedir
        [HttpGet("TraerLibrosParaPedir")]
        public async Task<ActionResult<List<GetLibrosParaPedirDTO>>> TraerLibrosParaPedir(
            string titulo = null,
            int? idSerie = null,
            int? idAutor = null,
            int? idEditorial = null,
            int? idMateria = null,
            int? codigo = null)
        {
            try
            {
                Seriee serie = idSerie.HasValue ? new Seriee { Id = idSerie.Value } : null;
                Autore autor = idAutor.HasValue ? new Autore { Id = idAutor.Value } : null;
                Editoriales editorial = idEditorial.HasValue ? new Editoriales { Id = idEditorial.Value } : null;
                Materiaa materia = idMateria.HasValue ? new Materiaa { Id = idMateria.Value } : null;

                var libros = await Task.Run(() => _pedidoService.TraerLibrosXTituloAutorEditorialYOCodigo(
                    titulo, serie, autor, editorial, materia, codigo ?? -1));

                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar libros para pedir", error = ex.Message });
            }
        }

        // POST: api/Pedido/GuardarPedido
        [HttpPost("GuardarPedido")]
        public async Task<ActionResult<List<int>>> GuardarPedido([FromBody] SavePedidoDTO pedido)
        {
            try
            {
                var resultado = await Task.Run(() => _pedidoService.GuardarPedido(pedido));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar pedido", error = ex.Message });
            }
        }

        // POST: api/Pedido/GuardarPedidos
        [HttpPost("GuardarPedidos")]
        public async Task<ActionResult<Dictionary<int, int>>> GuardarPedidos([FromBody] List<SavePedidoDTO> pedidos)
        {
            try
            {
                var resultado = await Task.Run(() => _pedidoService.GuardarPedidos(pedidos));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar pedidos", error = ex.Message });
            }
        }
    }
}
