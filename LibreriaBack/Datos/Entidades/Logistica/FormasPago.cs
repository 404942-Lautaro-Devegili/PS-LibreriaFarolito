namespace LibreriaBack.Datos.Entidades.Logistica
{
    public class FormasPago
    {
        public int Id { get; set; }
        public string? FormaPago { get; set; }
        public decimal? PorcentajeRecargo { get; set; }
        public decimal? SeniaMinima { get; set; }

        public FormasPago()
        {
            Id = -1;
            FormaPago = "";
            PorcentajeRecargo = 0;
            SeniaMinima = 0;
        }
    }
}
