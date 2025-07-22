using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibreriaBack.Datos.Entidades.DTOs.EstadisticasDTO;

namespace LibreriaBack.Servicios
{
    public interface IEstadisticasService
    {
        EstadisticasResponseDTO ObtenerEstadisticas(EstadisticasParametrosDTO parametros);
    }
}
