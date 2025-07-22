using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class LibroControlReservaNuevaDTO
    {
        public int IdLibro { get; set; }
        public int Cantidad { get; set; }
        public int CodigoLibro { get; set; }
        public string Titulo { get; set; }
        public decimal Precio { get; set; }
        public decimal Contado { get; set; }
        public decimal SubtotalContado { get; set; }
        public decimal SubtotalCredito { get; set; }

    }
}
