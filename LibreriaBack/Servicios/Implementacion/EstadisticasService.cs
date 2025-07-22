using LibreriaBack.Datos.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibreriaBack.Datos.Entidades.DTOs.EstadisticasDTO;

namespace LibreriaBack.Servicios.Implementacion
{
    public class EstadisticasService : IEstadisticasService
    {
        private readonly EstadisticasDao _estadisticasDao;

        public EstadisticasService()
        {
            _estadisticasDao = new EstadisticasDao();
        }

        public EstadisticasResponseDTO ObtenerEstadisticas(EstadisticasParametrosDTO parametros)
        {
            try
            {
                EstadisticasResponseDTO response = new EstadisticasResponseDTO();

                if (string.IsNullOrEmpty(parametros.Tipo))
                {
                    throw new ArgumentException("El tipo de estadística es requerido");
                }

                if (parametros.Tipo.ToLower() == "pedidos")
                {
                    var estadisticasGenerales = _estadisticasDao.GetEstadisticasGeneralesPedidos(
                        parametros.DistribuidorId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );

                    response.TotalRegistros = estadisticasGenerales.TotalPedidos;
                    response.MontoTotal = estadisticasGenerales.MontoTotal;
                    response.Promedio = estadisticasGenerales.Promedio;
                    response.UltimoMes = estadisticasGenerales.UltimoMes;

                    response.DetallesPedidos = _estadisticasDao.GetDetallePedidos(
                        parametros.DistribuidorId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );

                    response.Tendencias = _estadisticasDao.GetTendenciasPedidos(
                        parametros.DistribuidorId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );
                }
                else if (parametros.Tipo.ToLower() == "reservas")
                {
                    var estadisticasGenerales = _estadisticasDao.GetEstadisticasGeneralesReservas(
                        parametros.MetodoPagoId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );

                    response.TotalRegistros = estadisticasGenerales.TotalReservas;
                    response.MontoTotal = estadisticasGenerales.MontoTotal;
                    response.Promedio = estadisticasGenerales.Promedio;
                    response.UltimoMes = estadisticasGenerales.UltimoMes;

                    response.DetallesReservas = _estadisticasDao.GetDetalleReservas(
                        parametros.MetodoPagoId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );

                    response.Tendencias = _estadisticasDao.GetTendenciasReservas(
                        parametros.MetodoPagoId,
                        parametros.FechaInicio,
                        parametros.FechaFin
                    );
                }
                else
                {
                    throw new ArgumentException("Tipo de estadística no válido. Debe ser 'pedidos' o 'reservas'");
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerEstadisticas: {ex.Message}");
                throw new Exception($"Error al obtener estadísticas: {ex.Message}", ex);
            }
        }
    }
}
