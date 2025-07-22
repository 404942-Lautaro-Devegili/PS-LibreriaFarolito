namespace LibreriaBack.Datos.Entidades.Logistica
{
    public class Cliente
    {
        public int Id { get; set; }

        public int? Codigo { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public int? IdLocalidad { get; set; }

        public string? Localidad { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Cliente))
                return false;

            Cliente other = (Cliente)obj;

            return Id == other.Id &&
                   Codigo == other.Codigo &&
                   Nombre == other.Nombre &&
                   Direccion == other.Direccion &&
                   IdLocalidad == other.IdLocalidad &&
                   Localidad == other.Localidad &&
                   Telefono == other.Telefono &&
                   Email == other.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Codigo, Nombre, Direccion, IdLocalidad, Localidad, Telefono, Email);
        }
    }
}
