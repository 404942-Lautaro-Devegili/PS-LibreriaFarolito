using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Cliente/TraerClientes
        [HttpGet("TraerClientes")]
        public async Task<ActionResult<List<Cliente>>> TraerClientes()
        {
            try
            {
                var clientes = await Task.Run(() => _clienteService.TraerClientes());
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener clientes", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerClienteXId/5
        [HttpGet("TraerClienteXId/{id}")]
        public async Task<ActionResult<Cliente>> TraerClienteXId(int id)
        {
            try
            {
                var cliente = await Task.Run(() => _clienteService.TraerClienteXId(id));
                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener cliente", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerClienteXCodigo/123
        [HttpGet("TraerClienteXCodigo/{codigo}")]
        public async Task<ActionResult<Cliente>> TraerClienteXCodigo(int codigo)
        {
            try
            {
                var cliente = await Task.Run(() => _clienteService.TraerClienteXCodigo(codigo));
                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener cliente", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerClientesXNombre?nombre=Juan
        [HttpGet("TraerClientesXNombre")]
        public async Task<ActionResult<List<Cliente>>> TraerClientesXNombre(string nombre)
        {
            try
            {
                var clientes = await Task.Run(() => _clienteService.TraerClientesXNombre(nombre));
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar clientes", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerNextCodigo
        [HttpGet("TraerNextCodigo")]
        public async Task<ActionResult<int>> TraerNextCodigo()
        {
            try
            {
                var nextCodigo = await Task.Run(() => _clienteService.TraerNextCodigo());
                return Ok(nextCodigo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener próximo código", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerLocalidades
        [HttpGet("TraerLocalidades")]
        public async Task<ActionResult<List<Localidade>>> TraerLocalidades()
        {
            try
            {
                var localidades = await Task.Run(() => _clienteService.TraerLocalidades());
                return Ok(localidades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener localidades", error = ex.Message });
            }
        }

        // GET: api/Cliente/TraerFormasPago
        [HttpGet("TraerFormasPago")]
        public async Task<ActionResult<List<FormasPago>>> TraerFormasPago()
        {
            try
            {
                var formasPago = await Task.Run(() => _clienteService.TraerFormasPagoConPorcentaje());
                return Ok(formasPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener formas de pago", error = ex.Message });
            }
        }

        // PUT: api/Cliente/ActualizarPorcentajesFormasPago
        [HttpPut("ActualizarPorcentajesFormasPago")]
        public async Task<ActionResult<int>> ActualizarPorcentajesFormasPago([FromBody] List<FormaPagoActualizacionDTO> metodosPago)
        {
            try
            {
                foreach (var metodo in metodosPago)
                {
                    if (metodo.PorcentajeRecargo < 0 || metodo.PorcentajeRecargo > 100)
                    {
                        return BadRequest("El porcentaje debe estar entre 0 y 100");
                    }
                }

                var resultado = await Task.Run(() => _clienteService.ActualizarPorcentajesFormasPago(metodosPago));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar porcentajes", error = ex.Message });
            }
        }

        // POST: api/Cliente/GuardarOModificarCliente
        [HttpPost("GuardarOModificarCliente")]
        public async Task<ActionResult<List<int>>> GuardarOModificarCliente([FromBody] Cliente cliente)
        {
            try
            {
                var resultado = await Task.Run(() => _clienteService.GuardarOModificarCliente(cliente));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar cliente", error = ex.Message });
            }
        }
    }
}
