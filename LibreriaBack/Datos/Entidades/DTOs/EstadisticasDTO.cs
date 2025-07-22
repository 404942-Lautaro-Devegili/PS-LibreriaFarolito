using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class EstadisticasDTO
    {
        public class EstadisticasParametrosDTO
        {
            public string Tipo { get; set; }
            public int? DistribuidorId { get; set; }
            public int? MetodoPagoId { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }
        public class EstadisticasResponseDTO
        {
            public int TotalRegistros { get; set; }
            public decimal MontoTotal { get; set; }
            public decimal Promedio { get; set; }
            public int UltimoMes { get; set; }
            public List<DetallePedidoEstadisticaDTO> DetallesPedidos { get; set; }
            public List<DetalleReservaEstadisticaDTO> DetallesReservas { get; set; }
            public List<TendenciaEstadisticaDTO> Tendencias { get; set; }

            public EstadisticasResponseDTO()
            {
                DetallesPedidos = new List<DetallePedidoEstadisticaDTO>();
                DetallesReservas = new List<DetalleReservaEstadisticaDTO>();
                Tendencias = new List<TendenciaEstadisticaDTO>();
            }
        }
        public class DetallePedidoEstadisticaDTO
        {
            public int Id { get; set; }
            public string Distribuidor { get; set; }
            public DateTime Fecha { get; set; }
            public int TotalLibros { get; set; }
            public decimal Monto { get; set; }
            public string Estado { get; set; }
            public string EstadoColor { get; set; }
        }
        public class DetalleReservaEstadisticaDTO
        {
            public int Id { get; set; }
            public string Cliente { get; set; }
            public string MetodoPago { get; set; }
            public DateTime Fecha { get; set; }
            public int TotalLibros { get; set; }
            public decimal Monto { get; set; }
            public string Estado { get; set; }
            public string EstadoColor { get; set; }
        }
        public class TendenciaEstadisticaDTO
        {
            public DateTime Fecha { get; set; }
            public int Cantidad { get; set; }
            public decimal Monto { get; set; }
        }
        public class EstadisticasPedidosDTO
        {
            public int TotalPedidos { get; set; }
            public decimal MontoTotal { get; set; }
            public decimal Promedio { get; set; }
            public int UltimoMes { get; set; }
        }
        public class EstadisticasReservasDTO
        {
            public int TotalReservas { get; set; }
            public decimal MontoTotal { get; set; }
            public decimal Promedio { get; set; }
            public int UltimoMes { get; set; }
        }
    }
}
