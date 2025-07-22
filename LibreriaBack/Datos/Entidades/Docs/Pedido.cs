namespace LibreriaBack.Datos.Entidades.Docs
{
    public class Pedido
    {
        public int Id { get; set; }

        public int? IdLibro { get; set; }

        public int? IdProveedor { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? Fchpedido { get; set; }
    }
}
