using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibreriaBack.Datos.Entidades.DTOs.EstadisticasDTO;

namespace LibreriaBack.Datos.Implementacion
{
    public class EstadisticasDao : HelperDao
    {
        public EstadisticasPedidosDTO GetEstadisticasGeneralesPedidos(int? distribuidorId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            EstadisticasPedidosDTO estadisticas = new EstadisticasPedidosDTO();

            try
            {
                Parametro("PDISTRIBUIDOR_ID", "N", distribuidorId.HasValue ? distribuidorId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_ESTADISTICAS_GENERALES_PEDIDOS");
                LimpiarParametros();

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    estadisticas.TotalPedidos = row["total_pedidos"] != DBNull.Value ? Convert.ToInt32(row["total_pedidos"]) : 0;
                    estadisticas.MontoTotal = row["monto_total"] != DBNull.Value ? Convert.ToDecimal(row["monto_total"]) : 0;
                    estadisticas.Promedio = row["promedio"] != DBNull.Value ? Convert.ToDecimal(row["promedio"]) : 0;
                    estadisticas.UltimoMes = row["ultimo_mes"] != DBNull.Value ? Convert.ToInt32(row["ultimo_mes"]) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEstadisticasGeneralesPedidos: {ex.Message}");
                throw;
            }

            return estadisticas;
        }

        public EstadisticasReservasDTO GetEstadisticasGeneralesReservas(int? metodoPagoId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            EstadisticasReservasDTO estadisticas = new EstadisticasReservasDTO();

            try
            {
                Parametro("PMETODO_PAGO_ID", "N", metodoPagoId.HasValue ? metodoPagoId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_ESTADISTICAS_GENERALES_RESERVAS");
                LimpiarParametros();

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    estadisticas.TotalReservas = row["total_reservas"] != DBNull.Value ? Convert.ToInt32(row["total_reservas"]) : 0;
                    estadisticas.MontoTotal = row["monto_total"] != DBNull.Value ? Convert.ToDecimal(row["monto_total"]) : 0;
                    estadisticas.Promedio = row["promedio"] != DBNull.Value ? Convert.ToDecimal(row["promedio"]) : 0;
                    estadisticas.UltimoMes = row["ultimo_mes"] != DBNull.Value ? Convert.ToInt32(row["ultimo_mes"]) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEstadisticasGeneralesReservas: {ex.Message}");
                throw;
            }

            return estadisticas;
        }

        public List<DetallePedidoEstadisticaDTO> GetDetallePedidos(int? distribuidorId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<DetallePedidoEstadisticaDTO> detalles = new List<DetallePedidoEstadisticaDTO>();

            try
            {
                Parametro("PDISTRIBUIDOR_ID", "N", distribuidorId.HasValue ? distribuidorId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_DETALLE_PEDIDOS_ESTADISTICAS");
                LimpiarParametros();

                foreach (DataRow row in dt.Rows)
                {
                    DetallePedidoEstadisticaDTO detalle = new DetallePedidoEstadisticaDTO();
                    detalle.Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0;
                    detalle.Distribuidor = row["distribuidor"]?.ToString() ?? "";
                    detalle.Fecha = row["fecha"] != DBNull.Value ? Convert.ToDateTime(row["fecha"]) : DateTime.MinValue;
                    detalle.TotalLibros = row["total_libros"] != DBNull.Value ? Convert.ToInt32(row["total_libros"]) : 0;
                    detalle.Monto = row["monto"] != DBNull.Value ? Convert.ToDecimal(row["monto"]) : 0;
                    detalle.Estado = row["estado"]?.ToString() ?? "";

                    detalle.EstadoColor = ObtenerColorEstadoPedido(detalle.Estado);

                    detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetDetallePedidos: {ex.Message}");
                throw;
            }

            return detalles;
        }

        public List<DetalleReservaEstadisticaDTO> GetDetalleReservas(int? metodoPagoId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<DetalleReservaEstadisticaDTO> detalles = new List<DetalleReservaEstadisticaDTO>();

            try
            {
                Parametro("PMETODO_PAGO_ID", "N", metodoPagoId.HasValue ? metodoPagoId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_DETALLE_RESERVAS_ESTADISTICAS");
                LimpiarParametros();

                foreach (DataRow row in dt.Rows)
                {
                    DetalleReservaEstadisticaDTO detalle = new DetalleReservaEstadisticaDTO();
                    detalle.Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0;
                    detalle.Cliente = row["cliente"]?.ToString() ?? "";
                    detalle.MetodoPago = row["metodo_pago"]?.ToString() ?? "";
                    detalle.Fecha = row["fecha"] != DBNull.Value ? Convert.ToDateTime(row["fecha"]) : DateTime.MinValue;
                    detalle.TotalLibros = 1;
                    detalle.Monto = row["monto"] != DBNull.Value ? Convert.ToDecimal(row["monto"]) : 0;
                    detalle.Estado = row["estado"]?.ToString() ?? "";

                    detalle.EstadoColor = ObtenerColorEstadoReserva(detalle.Estado);

                    detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetDetalleReservas: {ex.Message}");
                throw;
            }

            return detalles;
        }

        public List<TendenciaEstadisticaDTO> GetTendenciasPedidos(int? distribuidorId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<TendenciaEstadisticaDTO> tendencias = new List<TendenciaEstadisticaDTO>();

            try
            {
                Parametro("PDISTRIBUIDOR_ID", "N", distribuidorId.HasValue ? distribuidorId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_TENDENCIAS_PEDIDOS");
                LimpiarParametros();

                foreach (DataRow row in dt.Rows)
                {
                    TendenciaEstadisticaDTO tendencia = new TendenciaEstadisticaDTO();
                    tendencia.Fecha = row["fecha"] != DBNull.Value ? Convert.ToDateTime(row["fecha"]) : DateTime.MinValue;
                    tendencia.Cantidad = row["cantidad"] != DBNull.Value ? Convert.ToInt32(row["cantidad"]) : 0;
                    tendencia.Monto = row["monto"] != DBNull.Value ? Convert.ToDecimal(row["monto"]) : 0;

                    tendencias.Add(tendencia);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetTendenciasPedidos: {ex.Message}");
                throw;
            }

            return tendencias;
        }

        public List<TendenciaEstadisticaDTO> GetTendenciasReservas(int? metodoPagoId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<TendenciaEstadisticaDTO> tendencias = new List<TendenciaEstadisticaDTO>();

            try
            {
                Parametro("PMETODO_PAGO_ID", "N", metodoPagoId.HasValue ? metodoPagoId.Value : -1);
                Parametro("PFECHA_INICIO", "F", fechaInicio.HasValue ? fechaInicio.Value : (object)DBNull.Value);
                Parametro("PFECHA_FIN", "F", fechaFin.HasValue ? fechaFin.Value : (object)DBNull.Value);

                DataTable dt = ObtenerDt("SP_GET_TENDENCIAS_RESERVAS");
                LimpiarParametros();

                foreach (DataRow row in dt.Rows)
                {
                    TendenciaEstadisticaDTO tendencia = new TendenciaEstadisticaDTO();
                    tendencia.Fecha = row["fecha"] != DBNull.Value ? Convert.ToDateTime(row["fecha"]) : DateTime.MinValue;
                    tendencia.Cantidad = row["cantidad"] != DBNull.Value ? Convert.ToInt32(row["cantidad"]) : 0;
                    tendencia.Monto = row["monto"] != DBNull.Value ? Convert.ToDecimal(row["monto"]) : 0;

                    tendencias.Add(tendencia);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetTendenciasReservas: {ex.Message}");
                throw;
            }

            return tendencias;
        }

        private string ObtenerColorEstadoPedido(string estado)
        {
            return estado.ToLower() switch
            {
                "pendiente" => "warning",
                "procesando" => "info",
                "enviado" => "primary",
                "recibido" => "success",
                "cancelado" => "danger",
                _ => "secondary"
            };
        }

        private string ObtenerColorEstadoReserva(string estado)
        {
            return estado.ToLower() switch
            {
                "sin asignar" => "warning",
                "pedida" => "info",
                "asignada" => "primary",
                "entregada" => "success",
                "cancelada" => "danger",
                _ => "secondary"
            };
        }
    }
}
