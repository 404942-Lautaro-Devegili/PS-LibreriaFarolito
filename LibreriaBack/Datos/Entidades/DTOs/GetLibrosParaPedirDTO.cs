namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class GetLibrosParaPedirDTO
    {
        public int IdLibro { get; set; }
        public int? Codigo { get; set; }
        public string? Titulo { get; set; }
        public int? IdAutor { get; set; }
        public string? Autor { get; set; }
        public int? IdSerie { get; set; }
        public string? Serie { get; set; }
        public int? IdEditorial { get; set; }
        public string? Editorial { get; set; }
        public int? IdMateria { get; set; }
        public string? Materia { get; set; }
        public int? Cantidad { get; set; }
        public string? IdsReservas { get; set; }
    }
}
