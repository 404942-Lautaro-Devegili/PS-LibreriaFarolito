namespace LibreriaBack.Datos.Entidades.Logistica
{
    public class Localidade
    {
        public int? Id { get; set; }

        public string? Localidad { get; set; }

        public int? IdProvincia { get; set; }

        public Localidade()
        {
            Id = -1;
            Localidad = "";
        }
    }
}
