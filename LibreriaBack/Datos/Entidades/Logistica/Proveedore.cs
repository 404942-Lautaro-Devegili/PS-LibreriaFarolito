namespace LibreriaBack.Datos.Entidades.Logistica
{
    public class Proveedore
    {
        public int Id { get; set; }
        public string? Proveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
        public int? IdLocalidad { get; set; }
        public int? CodPostal { get; set; }
        public string? NroIva { get; set; }
    }
}
