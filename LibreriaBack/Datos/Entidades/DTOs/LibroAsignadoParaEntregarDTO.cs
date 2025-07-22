namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class LibroAsignadoParaEntregarDTO
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public string? NomCliente { get; set; }
        public int IdLibro { get; set; }
        public decimal? Senia { get; set; }
        public decimal? Saldo { get; set; }

        public int? Codigo { get; set; }

        public string? Isbn { get; set; }

        public string? Ean13 { get; set; }

        public string? Titulo { get; set; }

        public int? IdAutor { get; set; }

        public string? Autor { get; set; }

        public int? IdSerie { get; set; }

        public string? Serie { get; set; }

        public int? IdEditorial { get; set; }

        public string? Editorial { get; set; }

        public int? IdMateria { get; set; }

        public string? Materia { get; set; }

        public decimal? PrecioLista { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Contado { get; set; }

        public int? Existencia { get; set; }
        public bool? Novedad { get; set; }
        public bool? Disponible { get; set; }
        public string? Telefono { get; set; }
    }
}
