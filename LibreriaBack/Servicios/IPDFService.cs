using iText.Layout;
using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Servicios
{
    public interface IPDFService
    {
        Dictionary<bool, string> ImprimirPDF(string rutaArchivo);
        Dictionary<bool, string> ImprimirPDFs(List<string> rutasArchivos);
        DocumentoDTO CrearPDFReserva(List<Reserva> reservas);
        List<DocumentoDTO> CreadPDFsPedidos(List<SavePedidoDTO> pedidos);
        DocumentoDTO ReimprimirReservas(List<Reserva> reservas);
    }
}
