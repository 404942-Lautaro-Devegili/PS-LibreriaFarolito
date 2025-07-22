using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;

namespace LibreriaBack.Servicios
{
    public interface IPedidoService
    {
        List<Proveedore> TraerProveedores();
        List<int> GuardarPedido(SavePedidoDTO p);
        Dictionary<int, int> GuardarPedidos(List<SavePedidoDTO> lp);
        List<GetLibrosParaPedirDTO> TraerLibrosXTituloAutorEditorialYOCodigo(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo);
    }
}
