namespace LibreriaBack.Datos.Entidades.Docs
{
    public class Reserva
    {
        public int Id { get; set; }

        public int? IdLibro { get; set; }

        public int? IdCliente { get; set; }

        public int? IdProveedor { get; set; }

        public decimal? Senia { get; set; }

        public decimal? Saldo { get; set; }

        public DateTime? Fchreserva { get; set; }

        public DateTime? Fchentrega { get; set; }
        public int? IdFormaPago { get; set; }

        public bool? LibroPedido { get; set; }

        public bool? Asignada { get; set; }

        public bool? Entregada { get; set; }

        public bool? Activa { get; set; }
    }
}
