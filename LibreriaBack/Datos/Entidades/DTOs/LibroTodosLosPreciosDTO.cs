using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class LibroTodosLosPreciosDTO
    {
        public int Id { get; set; }

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

        public DateTime FechaMod { get; set; }

        public bool? CargadoUltimaVezPorExcel { get; set; }

        public decimal? PrecioFinal { get; set; }
    }
}
