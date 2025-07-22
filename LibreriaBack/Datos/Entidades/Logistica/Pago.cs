using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.Logistica
{
    public class Pago
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string EstadoPago { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public string? PreferenceId { get; set; }
        public string? PaymentId { get; set; }
        public string? ExternalReference { get; set; }
        public string ReservasIds { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
