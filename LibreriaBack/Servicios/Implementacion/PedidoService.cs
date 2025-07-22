using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Datos.Implementacion;

namespace LibreriaBack.Servicios.Implementacion
{
    public class PedidoService : IPedidoService
    {
        PedidoDao pd;
        public PedidoService()
        {
            pd = new PedidoDao();
        }
        public List<Proveedore> TraerProveedores()
        {
            return pd.GetProveedores();
        }

        public List<int> GuardarPedido(SavePedidoDTO p)
        {
            var mimi = pd.SavePedido(p);
            return mimi;
        }
        public Dictionary<int, int> GuardarPedidos(List<SavePedidoDTO> lp)
        {
            return pd.SavePedidos(lp);
        }

        public List<GetLibrosParaPedirDTO> TraerLibrosXTituloAutorEditorialYOCodigo(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo)
        {
            return pd.GetLibrosParaPedir(titulo, serie, autor, editorial, materia, codigo);
        }
    }
}
