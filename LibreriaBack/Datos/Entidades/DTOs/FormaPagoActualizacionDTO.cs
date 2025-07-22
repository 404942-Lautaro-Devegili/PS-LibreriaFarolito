using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class FormaPagoActualizacionDTO
    {
        public int Id { get; set; }
        public decimal PorcentajeRecargo { get; set; }
        public decimal SeniaMinima { get; set; }
    }
}
