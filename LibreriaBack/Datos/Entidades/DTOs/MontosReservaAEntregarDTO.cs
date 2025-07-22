namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class MontosReservaAEntregarDTO
    {
        public int IdReserva { get; set; }
        public int IdLibro { get; set; }
        public decimal? Senia { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? PrecioLibro { get; set; }
        public decimal? ContadoLibro { get; set; }
        public int? formaPago { get; set; }
    }
}
