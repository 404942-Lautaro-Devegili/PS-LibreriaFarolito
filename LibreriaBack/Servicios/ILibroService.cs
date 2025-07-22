using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;

namespace LibreriaBack.Servicios
{
    public interface ILibroService
    {
        event Action<int, int, int> ProgresoActualizado;
        Libro TraerLibroXCodigo(int codigo);
        Libro TraerLibroXId(int id);
        List<Autore> TraerAutores();
        List<Editoriales> TraerEditoriales();
        List<Materiaa> TraerMaterias();
        List<Seriee> TraerSeries();
        List<int> TraerLibrosDesdeExcelViejo();
        List<int> TraerLibrosDesdeExcel(string rutaArchivo);
        List<int> ProcesarExcel(string rutaArchivo);
        List<int> ProcesarExcelLeas(string rutaArchivo);
        bool EstaEnBD(string autorOMateriaOEditorial, string opcion);
        Dictionary<int, bool> Guardar(string autorOMateriaOEditorial, string opcion);
        int TraerIDXNombre(string autorOMateriaOEditorial, string opcion);
        bool CargarVariablesGlobales();
        int GuardarOModificarLibro(Libro libro, int opcion);
        List<Object> TraerCoincidentes(string autorOMateriaOEditorial, string opcion);
        List<Libro> TraerLibrosXTituloAutorEditorialYOCodigo(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo);
        bool Iguales(Libro libro1, Libro libro2);
        int TraerNextCodigo();
        LibroTodosLosPreciosDTO TraerLibroConPrecioXFechaYOReserva(DateTime fecha, int reserva, int codigo);
        List<Proveedore> TraerProveedoresCompletos();
        int AgregarProveedor(Proveedore proveedor);
        int ModificarProveedor(Proveedore proveedor);
    }
}
