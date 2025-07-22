using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class RegistrarPagoDTO
    {
        public List<int> ReservasIds { get; set; } = new();
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? PreferenceId { get; set; }
        public string? PaymentId { get; set; }
        public string? ExternalReference { get; set; }
    }
}
