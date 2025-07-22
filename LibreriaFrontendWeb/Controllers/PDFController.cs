using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IPDFService _pdfService;

        public PDFController(IPDFService pdfService)
        {
            _pdfService = pdfService;
        }

        // POST: api/PDF/CrearPDFReserva
        [HttpPost("CrearPDFReserva")]
        public async Task<ActionResult<DocumentoDTO>> CrearPDFReserva([FromBody] List<Reserva> reservas)
        {
            try
            {
                var documento = await Task.Run(() => _pdfService.CrearPDFReserva(reservas));
                return Ok(documento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear PDF", error = ex.Message });
            }
        }

        // POST: api/PDF/ImprimirPDF
        [HttpPost("ImprimirPDF")]
        public async Task<ActionResult<Dictionary<bool, string>>> ImprimirPDF([FromBody] string rutaArchivo)
        {
            try
            {
                var resultado = await Task.Run(() => _pdfService.ImprimirPDF(rutaArchivo));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al imprimir PDF", error = ex.Message });
            }
        }

        // POST: api/PDF/CrearPDFsPedidos
        [HttpPost("CrearPDFsPedidos")]
        public async Task<ActionResult<List<DocumentoDTO>>> CrearPDFsPedidos([FromBody] List<SavePedidoDTO> pedidos)
        {
            try
            {
                var documentos = await Task.Run(() => _pdfService.CreadPDFsPedidos(pedidos));
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear PDFs de pedidos", error = ex.Message });
            }
        }

        // POST: api/PDF/ImprimirPDFs
        [HttpPost("ImprimirPDFs")]
        public async Task<ActionResult<Dictionary<bool, string>>> ImprimirPDFs([FromBody] List<string> rutasArchivos)
        {
            try
            {
                var resultado = await Task.Run(() => _pdfService.ImprimirPDFs(rutasArchivos));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al imprimir PDFs", error = ex.Message });
            }
        }

        // POST: api/PDF/ReimprimirReservas
        [HttpPost("ReimprimirReservas")]
        public async Task<ActionResult<DocumentoDTO>> ReimprimirReservas([FromBody] List<Reserva> reservas)
        {
            try
            {
                var documento = await Task.Run(() => _pdfService.ReimprimirReservas(reservas));
                return Ok(documento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear PDF de reimpresión", error = ex.Message });
            }
        }

        [HttpGet("DescargarPDF/{nombreArchivo}")]
        public IActionResult DescargarPDF(string nombreArchivo)
        {
            try
            {
                string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
                string directorioPDF = Path.Combine(directorioPrograma, "PDFReservas");
                string rutaCompleta = Path.Combine(directorioPDF, nombreArchivo);

                if (!System.IO.File.Exists(rutaCompleta))
                {
                    return NotFound("PDF no encontrado");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaCompleta);

                return File(fileBytes, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al descargar PDF", error = ex.Message });
            }
        }

        [HttpGet("DescargarPDFPedido/{nombreArchivo}")]
        public IActionResult DescargarPDFPedido(string nombreArchivo)
        {
            try
            {
                string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
                string directorioPDF = Path.Combine(directorioPrograma, "PDFPedidos");
                string rutaCompleta = Path.Combine(directorioPDF, nombreArchivo);

                if (!System.IO.File.Exists(rutaCompleta))
                {
                    return NotFound("PDF no encontrado");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaCompleta);

                return File(fileBytes, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al descargar PDF", error = ex.Message });
            }
        }

        [HttpGet("DescargarPDFReimpresion/{nombreArchivo}")]
        public IActionResult DescargarPDFReimpresion(string nombreArchivo)
        {
            try
            {
                string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
                string directorioPDF = Path.Combine(directorioPrograma, "PDFReimpresionReservas");
                string rutaCompleta = Path.Combine(directorioPDF, nombreArchivo);

                if (!System.IO.File.Exists(rutaCompleta))
                {
                    return NotFound("PDF no encontrado");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaCompleta);

                return File(fileBytes, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al descargar PDF", error = ex.Message });
            }
        }
    }
}
