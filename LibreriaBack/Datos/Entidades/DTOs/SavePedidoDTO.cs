namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class SavePedidoDTO
    {
        public int Id { get; set; }

        public int? IdLibro { get; set; }

        public int? IdProveedor { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? Fchpedido { get; set; }
        public string? IdsReservas { get; set; }
    }
}
