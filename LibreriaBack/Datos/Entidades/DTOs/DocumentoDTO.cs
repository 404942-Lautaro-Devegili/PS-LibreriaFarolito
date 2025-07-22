using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaBack.Datos.Entidades.DTOs
{
    public class DocumentoDTO
    {
        public string? Path { get; set; }
        public iText.Layout.Document? Documento { get; set; }
    }
}
