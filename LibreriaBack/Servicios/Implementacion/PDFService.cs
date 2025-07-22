using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LibreriaBack.Servicios.Implementacion
{
    public class PDFService : IPDFService
    {
        IClienteService cs;
        IReservaService rs;
        ILibroService ls;
        IPedidoService ps;

        public PDFService()
        {
            cs = new ClienteService();
            rs = new ReservaService();
            ls = new LibroService();
            ps = new PedidoService();
        }

        public Dictionary<bool, string> ImprimirPDF(string rutaArchivo)
        {
            Dictionary<bool, string> resultado = new Dictionary<bool, string>();
            try
            {
                string edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                string acrobatPath = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";

                if (System.IO.File.Exists(acrobatPath))
                {
                    try
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = acrobatPath;
                        process.StartInfo.Arguments = $"/t \"{rutaArchivo}\"";
                        process.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                    finally
                    {

                    }

                }
                else
                {
                    if (System.IO.File.Exists(edgePath))
                    {
                        Process edgeProcess = new Process();
                        edgeProcess.StartInfo.FileName = edgePath;
                        edgeProcess.StartInfo.Arguments = $"\"{rutaArchivo}\"";
                        edgeProcess.Start();
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = rutaArchivo,
                            Verb = "print",
                            CreateNoWindow = true,
                            UseShellExecute = true
                        };
                        Process.Start(startInfo);
                    }
                }

                resultado.Add(true, "Impresión exitosa");
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Add(true, ex.Message);
                return resultado;
            }
        }

        public Dictionary<bool, string> ImprimirPDFs(List<string> rutasArchivos)
        {
            Dictionary<bool, string> resultado = new Dictionary<bool, string>();

            try
            {
                string edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                string acrobatPath = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";

                foreach (string rutaArchivo in rutasArchivos)
                {
                    if (System.IO.File.Exists(acrobatPath))
                    {
                        try
                        {
                            Process process = new Process();
                            process.StartInfo.FileName = acrobatPath;
                            process.StartInfo.Arguments = $"/t \"{rutaArchivo}\"";
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            throw;
                        }
                        finally
                        {
                        }

                    }
                    else
                    {
                        if (System.IO.File.Exists(edgePath))
                        {
                            Process edgeProcess = new Process();
                            edgeProcess.StartInfo.FileName = edgePath;
                            edgeProcess.StartInfo.Arguments = $"\"{rutaArchivo}\"";
                            edgeProcess.Start();
                        }
                        else
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                FileName = rutaArchivo,
                                Verb = "print",
                                CreateNoWindow = true,
                                UseShellExecute = true
                            };
                            Process.Start(startInfo);
                        }
                    }
                }

                resultado.Add(true, "Archivos PDF abiertos exitosamente.");
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Add(false, ex.Message);
                return resultado;
            }
        }


        public DocumentoDTO CrearPDFReserva(List<Reserva> reservas)
        {
            string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;

            string directorioPDF = Path.Combine(directorioPrograma, "PDFReservas");

            if (!Directory.Exists(directorioPDF))
            {
                Directory.CreateDirectory(directorioPDF);
            }

            string archivoNombre = "Reservas_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".pdf";

            string rutaArchivo = Path.Combine(directorioPDF, archivoNombre);

            PdfWriter writer = new PdfWriter(rutaArchivo);
            PdfDocument pdf = new PdfDocument(writer);
            iText.Layout.Document document = new iText.Layout.Document(pdf);

            PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontItalic = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

            Color bordoColor = new DeviceRgb(128, 0, 0);

            Color headerBackgroundColor = new DeviceRgb(200, 0, 0);

            Color white = new DeviceRgb(255, 255, 255);

            Color black = new DeviceRgb(0, 0, 0);

            document.Add(new Paragraph("Detalle de Reservas")
                .SetFont(fontBold)
                .SetFontSize(20)
                .SetFontColor(bordoColor)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            document.Add(new Paragraph("\n"));

            Reserva primeraReserva = reservas.FirstOrDefault();
            if (primeraReserva != null)
            {
                Cliente cliente = cs.TraerClienteXId(primeraReserva.IdCliente.Value);
                FormasPago formaPago = cs.TraerFormasPago().FirstOrDefault(fp => fp.Id == primeraReserva.IdFormaPago.Value);

                document.Add(new Paragraph($"Cliente:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {cliente.Id} - {cliente.Nombre} - {(!string.IsNullOrEmpty(cliente.Telefono) ? "Teléfono: " + cliente.Telefono : "")}\n").SetFont(fontNormal));


                document.Add(new Paragraph("Fecha Reserva:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {primeraReserva.Fchreserva?.ToString("dd/MM/yyyy HH:mm:ss")}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Forma de Pago:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {formaPago.FormaPago}\n").SetFont(fontNormal));

                decimal seniaTotal = Math.Round(reservas.Sum(r => r.Senia).Value, 2);
                decimal reservaTotal = Math.Round(reservas.Sum(r => r.Saldo + r.Senia).Value, 2);
                decimal saldoAPagar = Math.Round(reservaTotal - seniaTotal, 2);
                document.Add(new Paragraph("Seña total:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {seniaTotal}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Total reserva:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {reservaTotal}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Saldo a Pagar:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {saldoAPagar}\n").SetFont(fontNormal));
            }

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 4, 2, 2, 2 }))
                .SetWidth(UnitValue.CreatePercentValue(100));

            table.AddCell(new Cell().Add(new Paragraph("Id Reserva").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Libro").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Seña").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Saldo").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            foreach (var reserva in reservas)
            {
                Libro libro = ls.TraerLibroXId(reserva.IdLibro.Value);
                decimal precio = reserva.Senia.Value + reserva.Saldo.Value;
                table.AddCell(new Cell().Add(new Paragraph(reserva.Id.ToString()).SetFont(fontNormal)));
                table.AddCell(new Cell().Add(new Paragraph($"Cód: {libro.Codigo} - {libro.Titulo}").SetFont(fontNormal)));
                table.AddCell(new Cell().Add(new Paragraph($"${precio:F2}").SetFont(fontNormal)));
                table.AddCell(new Cell().Add(new Paragraph($"${reserva.Senia:F2}").SetFont(fontNormal)));
                table.AddCell(new Cell().Add(new Paragraph($"${reserva.Saldo:F2}").SetFont(fontNormal)));
            }

            document.Add(table);
            document.Close();

            Console.WriteLine($"PDF creado exitosamente en: {rutaArchivo}");
            DocumentoDTO documento = new DocumentoDTO
            {
                Path = archivoNombre,
                Documento = document
            };
            return documento;
        }

        public List<DocumentoDTO> CreadPDFsPedidos(List<SavePedidoDTO> pedidos)
        {
            string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;

            string directorioPDF = Path.Combine(directorioPrograma, "PDFPedidos");

            if (!Directory.Exists(directorioPDF))
            {
                Directory.CreateDirectory(directorioPDF);
            }

            List<DocumentoDTO> documentosGenerados = new List<DocumentoDTO>();

            var pedidosPorProveedor = pedidos
                .GroupBy(p => p.IdProveedor)
                .ToList();

            foreach (var grupoProveedor in pedidosPorProveedor)
            {
                var pedidosProveedor = grupoProveedor.ToList();

                Proveedore proveedor = ps.TraerProveedores().FirstOrDefault(p => p.Id == grupoProveedor.Key);
                string nombreProveedor = proveedor != null ? proveedor.Proveedor : "Proveedor desconocido";

                string archivoNombre = $"{nombreProveedor}_Pedidos_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.pdf";
                string rutaArchivo = Path.Combine(directorioPDF, archivoNombre);

                PdfWriter writer = new PdfWriter(rutaArchivo);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                Color bordoColor = new DeviceRgb(128, 0, 0);

                document.Add(new Paragraph($"Pedido a Proveedor: {nombreProveedor}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18)
                    .SetFont(fontBold));

                string fechaPedido = pedidosProveedor[0]?.Fchpedido?.ToString("dd/MM/yyyy") ?? DateTime.Now.ToString("dd/MM/yyyy");
                document.Add(new Paragraph($"Fecha del Pedido: {fechaPedido}")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(12)
                    .SetMarginTop(20));

                Table tabla = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 1 }))
                    .UseAllAvailableWidth();

                tabla.AddHeaderCell("Item");
                tabla.AddHeaderCell("Libro");
                tabla.AddHeaderCell("Cantidad");
                int contador = 1;
                foreach (SavePedidoDTO pedido in pedidosProveedor)
                {
                    Libro libro = ls.TraerLibroXId(pedido.IdLibro.Value);
                    string detalleLibro = libro != null ? $"Cód: {libro.Codigo} - {libro.Titulo}" : "Libro desconocido";
                    string nombreProveedorPdf = proveedor != null ? proveedor.Proveedor : "Proveedor desconocido";

                    tabla.AddCell(contador.ToString());
                    tabla.AddCell(detalleLibro);
                    tabla.AddCell(pedido.Cantidad?.ToString() ?? "1");
                    contador++;
                }
                int total = pedidosProveedor.Sum(p => p.Cantidad.Value);
                document.Add(new Paragraph($"Total de Libros: {total}")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(30)
                    .SetMarginTop(20));


                document.Add(tabla);

                document.Close();

                documentosGenerados.Add(new DocumentoDTO
                {
                    Path = archivoNombre,
                    Documento = document
                });

                Console.WriteLine($"PDF creado exitosamente para el proveedor {nombreProveedor} en: {rutaArchivo}");
            }

            return documentosGenerados;
        }


        public DocumentoDTO ReimprimirReservas(List<Reserva> reservas)
        {
            string directorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
            Reserva primeraReserva = reservas.FirstOrDefault();

            string directorioPDF = Path.Combine(directorioPrograma, "PDFReimpresionReservas");

            if (!Directory.Exists(directorioPDF))
            {
                Directory.CreateDirectory(directorioPDF);
            }

            Cliente cliente = null;
            string archivoNombre = "";
            if (primeraReserva != null)
            {
                cliente = cs.TraerClienteXId(primeraReserva.IdCliente.Value);
                archivoNombre = "ReimpresionReservas_CLiente-" + cliente.Codigo + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".pdf";
            }
            else
            {
                archivoNombre = "ReimpresionReservas_" +DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".pdf";
            }

            string rutaArchivo = Path.Combine(directorioPDF, archivoNombre);

            PdfWriter writer = new PdfWriter(rutaArchivo);
            PdfDocument pdf = new PdfDocument(writer);
            iText.Layout.Document document = new iText.Layout.Document(pdf);

            PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontItalic = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

            Color bordoColor = new DeviceRgb(128, 0, 0);

            Color headerBackgroundColor = new DeviceRgb(200, 0, 0);

            Color white = new DeviceRgb(255, 255, 255);

            Color black = new DeviceRgb(0, 0, 0);

            document.Add(new Paragraph("Detalle de Reservas")
                .SetFont(fontBold)
                .SetFontSize(20)
                .SetFontColor(bordoColor)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            document.Add(new Paragraph("\n"));

            if (primeraReserva != null)
            {
                if (cliente == null)
                {
                    cliente = cs.TraerClienteXId(primeraReserva.IdCliente.Value);
                }
                
                FormasPago formaPago = cs.TraerFormasPago().FirstOrDefault(fp => fp.Id == primeraReserva.IdFormaPago.Value);

                document.Add(new Paragraph($"Cliente:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {cliente.Id} - {cliente.Nombre} - {(!string.IsNullOrEmpty(cliente.Telefono) ? "Teléfono: " + cliente.Telefono : "")}\n").SetFont(fontNormal));


                document.Add(new Paragraph("Fecha Reserva:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {primeraReserva.Fchreserva?.ToString("dd/MM/yyyy HH:mm:ss")}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Forma de Pago:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {formaPago.FormaPago}\n").SetFont(fontNormal));

                decimal seniaTotal = Math.Round(reservas.Sum(r => r.Senia).Value, 2);
                decimal reservaTotal = Math.Round(reservas.Sum(r => r.Saldo + r.Senia).Value, 2);
                decimal saldoAPagar = Math.Round(reservaTotal - seniaTotal, 2);
                document.Add(new Paragraph("Seña total:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {seniaTotal}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Total reserva:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {reservaTotal}\n").SetFont(fontNormal));

                document.Add(new Paragraph("Saldo a Pagar:")
                    .SetFont(fontBold)
                    .SetFontColor(black)
                    .Add($" {saldoAPagar}\n").SetFont(fontNormal));
            }

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 4, 2, 2, 2 }))
                .SetWidth(UnitValue.CreatePercentValue(100));

            table.AddCell(new Cell().Add(new Paragraph("Id Reserva").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Libro").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Seña").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("Saldo").SetFont(fontBold).SetFontColor(black))
                .SetBackgroundColor(white).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            foreach (var reserva in reservas)
            {
                LibroTodosLosPreciosDTO libro = ls.TraerLibroConPrecioXFechaYOReserva(reserva.Fchreserva.Value, reserva.Id, reserva.IdLibro.Value);
                decimal precio = reserva.Senia.Value + reserva.Saldo.Value;
                if (reservas == null || reservas.Count == 0 || reserva == null)
                {
                    if (reserva == null)
                    {
                        throw new ArgumentNullException(nameof(reserva), "La reserva es nula o está vacía.");
                    }
                    else if (reservas.Count == 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(reservas), "La lista de reservas está vacía.");
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(reservas), "La reserva es nula.");
                    }
                    
                }

                try
                {
                    table.AddCell(new Cell().Add(new Paragraph(reserva.Id.ToString()).SetFont(fontNormal)));
                    table.AddCell(new Cell().Add(new Paragraph($"Cód: {libro.Codigo} - {libro.Titulo}").SetFont(fontNormal)));
                    table.AddCell(new Cell().Add(new Paragraph($"${precio:F2}").SetFont(fontNormal)));
                    table.AddCell(new Cell().Add(new Paragraph($"${reserva.Senia:F2}").SetFont(fontNormal)));
                    table.AddCell(new Cell().Add(new Paragraph($"${reserva.Saldo:F2}").SetFont(fontNormal)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            document.Add(table);

            document.Close();

            Console.WriteLine($"PDF creado exitosamente en: {rutaArchivo}");
            DocumentoDTO documento = new DocumentoDTO
            {
                Path = archivoNombre,
                Documento = document
            };
            return documento;
        }


    }
}
