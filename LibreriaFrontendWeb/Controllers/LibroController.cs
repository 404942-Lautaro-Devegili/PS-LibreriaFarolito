using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Servicios;
using LibreriaBack.Servicios.Implementacion;
using LibreriaFrontendWeb.Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService ls;
        private readonly IHubContext<ProgressHub> _hubContext;

        public LibroController(ILibroService libroService, IHubContext<ProgressHub> hubContext)
        {
            this.ls = libroService;
            this._hubContext = hubContext;
        }

        // GET: api/Libro/TraerAutores
        [HttpGet("TraerAutores")]
        public async Task<ActionResult<List<Autore>>> TraerAutores()
        {
            try
            {
                var autores = await Task.Run(() => ls.TraerAutores());
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener autores", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerEditoriales
        [HttpGet("TraerEditoriales")]
        public async Task<ActionResult<List<Editoriales>>> TraerEditoriales()
        {
            try
            {
                var editoriales = await Task.Run(() => ls.TraerEditoriales());
                return Ok(editoriales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener editoriales", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerMaterias
        [HttpGet("TraerMaterias")]
        public async Task<ActionResult<List<Materiaa>>> TraerMaterias()
        {
            try
            {
                var materias = await Task.Run(() => ls.TraerMaterias());
                return Ok(materias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener materias", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerSeries
        [HttpGet("TraerSeries")]
        public async Task<ActionResult<List<Seriee>>> TraerSeries()
        {
            try
            {
                var series = await Task.Run(() => ls.TraerSeries());
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener series", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerLibroXCodigo/5
        [HttpGet("TraerLibroXCodigo/{codigo}")]
        public async Task<ActionResult<Libro>> TraerLibroXCodigo(int codigo)
        {
            try
            {
                var libro = await Task.Run(() => ls.TraerLibroXCodigo(codigo));
                if (libro == null)
                    return NotFound();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener libro", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerNextCodigo
        [HttpGet("TraerNextCodigo")]
        public async Task<ActionResult<int>> TraerNextCodigo()
        {
            try
            {
                var nextCodigo = await Task.Run(() => ls.TraerNextCodigo());
                return Ok(nextCodigo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener próximo código", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerCoincidentes?busqueda=texto&tipo=A
        [HttpGet("TraerCoincidentes")]
        public async Task<ActionResult<List<object>>> TraerCoincidentes(string busqueda, string tipo)
        {
            try
            {
               if (busqueda == "-")
                {
                    busqueda = "";
                }
                var coincidentes = await Task.Run(() => ls.TraerCoincidentes(busqueda, tipo));
                return Ok(coincidentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener coincidentes", error = ex.Message });
            }
        }

        // POST: api/Libro/GuardarLibro
        [HttpPost("GuardarLibro")]
        public async Task<ActionResult<int>> GuardarLibro([FromBody] Libro libro)
        {
            try
            {
                // Opción 1 = Alta
                ls.CargarVariablesGlobales();
                var resultado = await Task.Run(() => ls.GuardarOModificarLibro(libro, 1));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar libro", error = ex.Message });
            }
        }

        // PUT: api/Libro/ModificarLibro
        [HttpPut("ModificarLibro")]
        public async Task<ActionResult<int>> ModificarLibro([FromBody] Libro libro)
        {
            try
            {
                // Opción 2 = Modificar
                ls.CargarVariablesGlobales();
                var resultado = await Task.Run(() => ls.GuardarOModificarLibro(libro, 2));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al modificar libro", error = ex.Message });
            }
        }

        // POST: api/Libro/ImportExcel
        [HttpPost("ImportExcel")]
        public async Task<ActionResult<List<int>>> ImportExcel(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No se ha proporcionado ningún archivo");

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (extension != ".xls" && extension != ".xlsx")
                    return BadRequest("El archivo debe ser un Excel (.xls o .xlsx)");

                var tempPath = Path.GetTempFileName();
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                try
                {
                    void ProgressHandler(int progress, int processed, int remaining)
                    {
                        _hubContext.Clients.All.SendAsync("ReceiveProgress", progress, processed, remaining).Wait();
                    }

                    ls.ProgresoActualizado += ProgressHandler;

                    try
                    {
                        var resultado = await Task.Run(() => ls.TraerLibrosDesdeExcel(tempPath));
                        return Ok(resultado);
                    }
                    finally
                    {
                        ls.ProgresoActualizado -= ProgressHandler;
                    }
                }
                finally
                {
                    if (System.IO.File.Exists(tempPath))
                        System.IO.File.Delete(tempPath);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al procesar archivo Excel", error = ex.Message });
            }
        }

        // GET: api/Libro/BuscarLibros
        [HttpGet("BuscarLibros")]
        public async Task<ActionResult<List<Libro>>> BuscarLibros(
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

                var libros = await Task.Run(() => ls.TraerLibrosXTituloAutorEditorialYOCodigo(
                    titulo, serie, autor, editorial, materia, codigo ?? 0));

                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar libros", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerLibroConPrecios
        [HttpGet("TraerLibroConPrecios")]
        public async Task<ActionResult<LibroTodosLosPreciosDTO>> TraerLibroConPrecios(
            DateTime? fecha = null,
            int? reserva = null,
            int? codigo = null)
        {
            try
            {
                var libro = await Task.Run(() => ls.TraerLibroConPrecioXFechaYOReserva(
                    fecha ?? DateTime.Now,
                    reserva ?? 0,
                    codigo ?? 0));

                if (libro == null)
                    return NotFound();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener libro con precios", error = ex.Message });
            }
        }

        // POST: api/Libro/GuardarAutor
        [HttpPost("GuardarAutor")]
        public async Task<ActionResult<Dictionary<int, bool>>> GuardarAutor([FromBody] string autor)
        {
            try
            {
                var resultado = await Task.Run(() => ls.Guardar(autor, "A"));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar autor", error = ex.Message });
            }
        }

        // POST: api/Libro/GuardarEditorial
        [HttpPost("GuardarEditorial")]
        public async Task<ActionResult<Dictionary<int, bool>>> GuardarEditorial([FromBody] string editorial)
        {
            try
            {
                var resultado = await Task.Run(() => ls.Guardar(editorial, "E"));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar editorial", error = ex.Message });
            }
        }

        // POST: api/Libro/GuardarMateria
        [HttpPost("GuardarMateria")]
        public async Task<ActionResult<Dictionary<int, bool>>> GuardarMateria([FromBody] string materia)
        {
            try
            {
                var resultado = await Task.Run(() => ls.Guardar(materia, "M"));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar materia", error = ex.Message });
            }
        }

        // POST: api/Libro/GuardarSerie
        [HttpPost("GuardarSerie")]
        public async Task<ActionResult<Dictionary<int, bool>>> GuardarSerie([FromBody] string serie)
        {
            try
            {
                var resultado = await Task.Run(() => ls.Guardar(serie, "S"));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar serie", error = ex.Message });
            }
        }

        // GET: api/Libro/EstaEnBD
        [HttpGet("EstaEnBD")]
        public async Task<ActionResult<bool>> EstaEnBD(string valor, string tipo)
        {
            try
            {
                var existe = await Task.Run(() => ls.EstaEnBD(valor, tipo));
                return Ok(existe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al verificar existencia", error = ex.Message });
            }
        }

        // GET: api/Libro/TraerProveedores
        [HttpGet("TraerProveedores")]
        public async Task<ActionResult<List<Proveedore>>> TraerProveedores()
        {
            try
            {
                var proveedores = await Task.Run(() => ls.TraerProveedoresCompletos());
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener proveedores", error = ex.Message });
            }
        }

        // POST: api/Libro/AgregarProveedor
        [HttpPost("AgregarProveedor")]
        public async Task<ActionResult> AgregarProveedor([FromBody] Proveedore proveedor)
        {
            try
            {
                var resultado = await Task.Run(() => ls.AgregarProveedor(proveedor));
                if (resultado > 0)
                {
                    return Ok(new { message = "Proveedor agregado exitosamente", id = resultado });
                }
                else
                {
                    return BadRequest(new { message = "Error al agregar el proveedor" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // PUT: api/Libro/ModificarProveedor
        [HttpPut("ModificarProveedor")]
        public async Task<ActionResult> ModificarProveedor([FromBody] Proveedore proveedor)
        {
            try
            {
                var resultado = await Task.Run(() => ls.ModificarProveedor(proveedor));
                if (resultado > 0)
                {
                    return Ok(new { message = "Proveedor modificado exitosamente" });
                }
                else
                {
                    return BadRequest(new { message = "Error al modificar el proveedor" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
