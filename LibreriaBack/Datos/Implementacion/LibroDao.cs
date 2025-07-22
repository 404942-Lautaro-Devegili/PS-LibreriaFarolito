using LibreriaBack.Datos.Entidades.Libros;
using System.Data;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Logistica;

namespace LibreriaBack.Datos.Implementacion
{
    // Diccionario (este lo hice yo para acordarme cómo usar mis funciones de bajo nivel mientras creaba todo):
    // Cuando quiero traer una datatable: OI().ObtenerDt("SP_NOMBRESP"), por ejemplo OI().ObtenerDt("SP_GETLIBROXCODIGO")
    // Cuando quiero traer un valor: OI().ValorParametro("NOMBREPARAMETRO"), por ejemplo OI().ValorParametro("ID")
    // Cuando quiero ejecutar un comando: OI().ComandoSP("SP_NOMBRESP"), por ejemplo OI().ComandoSP("SP_GUARDARLIBRO")
    // Cuando quiero ejecutar un comando y obtener la cantidad de filas afectadas: Afectadas(), por ejemplo luego de guardar un libro, Afectadas().
    // Si devuelve 1, se guardó correctamente, si devuelve 0, no se guardó, si devuelve -1, hubo un error.
    // Cuando quiero saber si hubo errores: HuboError(), por ejemplo HuboError(). Devuelve true si hubo errores, false si no hubo errores.

    public class LibroDao : HelperDao
    {
        private int afectadas;
        public HelperDao OI()
        {
            return HelperDao.OI();
        }

        public int Afectadas()
        {
            afectadas = OI().Afectadas();
            return afectadas;
        }

        public bool HuboError()
        {
            bool e = OI().HuboErrores;
            OI().LimpiarErrores();
            return e;
        }

        public Libro GetLibroXCodigo(int codigo)
        {
            OI().Parametro("CODIGO", "N", codigo);
            DataTable t = OI().ObtenerDt("SP_GETLIBROXCODIGO");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return null;
            }
            Libro l = new Libro();
            l.Id = Convert.ToInt32(t.Rows[0]["id"]);
            l.Codigo = Convert.ToInt32(t.Rows[0]["codigo"]);
            l.Isbn = t.Rows[0]["isbn"].ToString();
            l.Ean13 = t.Rows[0]["ean13"].ToString();
            l.Titulo = t.Rows[0]["titulo"].ToString();
            l.IdAutor = Convert.ToInt32(t.Rows[0]["id_autor"]);
            l.Autor = t.Rows[0]["autor"].ToString();
            l.IdSerie = Convert.ToInt32(t.Rows[0]["id_serie"]);
            l.Serie = t.Rows[0]["serie"].ToString();
            l.IdEditorial = Convert.ToInt32(t.Rows[0]["id_editorial"]);
            l.Editorial = t.Rows[0]["editorial"].ToString();
            l.IdMateria = Convert.ToInt32(t.Rows[0]["id_materia"]);
            l.Materia = t.Rows[0]["materia"].ToString();
            l.PrecioLista = Math.Round(Convert.ToDecimal(t.Rows[0]["precioLista"]), 2);
            l.Precio = Math.Round(Convert.ToDecimal(t.Rows[0]["precio"]), 2);
            l.Contado = Math.Round(Convert.ToDecimal(t.Rows[0]["contado"]), 2);
            l.Existencia = Convert.ToInt32(t.Rows[0]["existencia"]);
            l.Novedad = Convert.ToBoolean(t.Rows[0]["novedad"]);
            l.Disponible = t.Rows[0]["disponible"] != DBNull.Value ? Convert.ToBoolean(t.Rows[0]["disponible"]) : false;
            return l;
        }

        public Libro GetLibroXId(int id)
        {
            OI().Parametro("PIDLIBRO", "N", id);
            DataTable t = OI().ObtenerDt("SP_GETLIBROXID");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return null;
            }
            Libro l = new Libro();
            l.Id = Convert.ToInt32(t.Rows[0]["id"]);
            l.Codigo = Convert.ToInt32(t.Rows[0]["codigo"]);
            l.Isbn = t.Rows[0]["isbn"].ToString();
            l.Ean13 = t.Rows[0]["ean13"].ToString();
            l.Titulo = t.Rows[0]["titulo"].ToString();
            l.IdAutor = Convert.ToInt32(t.Rows[0]["id_autor"]);
            l.IdSerie = Convert.ToInt32(t.Rows[0]["id_serie"]);
            l.IdEditorial = Convert.ToInt32(t.Rows[0]["id_editorial"]);
            l.IdMateria = Convert.ToInt32(t.Rows[0]["id_materia"]);
            l.PrecioLista = Math.Round(Convert.ToDecimal(t.Rows[0]["precioLista"]), 2);
            l.Precio = Math.Round(Convert.ToDecimal(t.Rows[0]["precio"]), 2);
            l.Contado = Math.Round(Convert.ToDecimal(t.Rows[0]["contado"]), 2);
            l.Existencia = Convert.ToInt32(t.Rows[0]["existencia"]);
            l.Novedad = Convert.ToBoolean(t.Rows[0]["novedad"]);
            l.Disponible = Convert.ToBoolean(t.Rows[0]["disponible"]);
            return l;
        }

        public Dictionary<int, Libro> GetLibrosXBloqueCodigo(List<int> codigos)
        {
            var libros = new Dictionary<int, Libro>();
            Conectar();

            var table = new DataTable();
            table.Columns.Add("Codigo", typeof(int));

            foreach (var codigo in codigos)
                table.Rows.Add(codigo);

            var tableParam = comando.Parameters.AddWithValue("Codigos", table);
            tableParam.SqlDbType = SqlDbType.Structured;
            comando.CommandText = "SP_GETLIBROSXBLOQUECODIGO";
            comando.CommandType = CommandType.StoredProcedure;

            using (var reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    var libro = new Libro();
                    libro.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    libro.Codigo = reader.GetInt32(reader.GetOrdinal("codigo"));
                    libro.Isbn = reader.GetString(reader.GetOrdinal("isbn"));
                    libro.Ean13 = reader.GetString(reader.GetOrdinal("ean13"));
                    libro.Titulo = reader.GetString(reader.GetOrdinal("titulo"));
                    libro.IdAutor = reader.GetInt32(reader.GetOrdinal("id_autor"));
                    libro.Autor = reader.GetString(reader.GetOrdinal("autor"));
                    libro.IdSerie = reader.GetInt32(reader.GetOrdinal("id_serie"));
                    libro.Serie = reader.GetString(reader.GetOrdinal("serie"));
                    libro.IdEditorial = reader.GetInt32(reader.GetOrdinal("id_editorial"));
                    libro.Editorial = reader.GetString(reader.GetOrdinal("editorial"));
                    libro.IdMateria = reader.GetInt32(reader.GetOrdinal("id_materia"));
                    libro.Materia = reader.GetString(reader.GetOrdinal("materia"));
                    libro.PrecioLista = reader.GetDecimal(reader.GetOrdinal("precioLista"));
                    libro.Precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                    libro.Contado = reader.GetDecimal(reader.GetOrdinal("contado"));
                    libro.Existencia = reader.GetInt32(reader.GetOrdinal("existencia"));
                    libro.Novedad = reader.GetBoolean(reader.GetOrdinal("novedad"));
                    libro.Disponible = reader.GetBoolean(reader.GetOrdinal("disponible"));
                    libro.CargadoUltimaVezPorExcel = reader.GetBoolean(reader.GetOrdinal("cargadoUltimaVezPorExcel"));



                    libros[libro.Codigo.Value] = libro;
                }
            }
            LimpiarParametros();
            LimpiarErrores();
            Desconectar();
            return libros;

        }

        public List<Autore> GetAutores()
        {
            DataTable t = OI().Consultar("SP_GETAUTORES");
            List<Autore> la = new List<Autore>();
            foreach (DataRow r in t.Rows)
            {
                Autore a = new Autore();
                a.Id = Convert.ToInt32(r["id"]);
                a.Autor = r["autor"].ToString();
                la.Add(a);
            }
            return la;
        }

        public List<Editoriales> GetEditoriales()
        {
            DataTable t = OI().Consultar("SP_GETEDITORIALES");
            List<Editoriales> le = new List<Editoriales>();
            foreach (DataRow r in t.Rows)
            {
                Editoriales e = new Editoriales();
                e.Id = Convert.ToInt32(r["id"]);
                e.Editorial = r["editorial"].ToString();
                le.Add(e);
            }
            return le;
        }

        public List<Materiaa> GetMaterias()
        {
            DataTable t = OI().Consultar("SP_GETMATERIAS");
            List<Materiaa> lm = new List<Materiaa>();
            foreach (DataRow r in t.Rows)
            {
                Materiaa m = new Materiaa();
                m.Id = Convert.ToInt32(r["id"]);
                m.Materia = r["materia"].ToString();
                lm.Add(m);
            }
            return lm;
        }

        public List<Seriee> GetSeries()
        {
            DataTable t = OI().Consultar("SP_GETSERIES");
            List<Seriee> ls = new List<Seriee>();
            foreach (DataRow r in t.Rows)
            {
                Seriee s = new Seriee();
                s.Id = Convert.ToInt32(r["id"]);
                s.Serie = r["serie"].ToString();
                ls.Add(s);
            }
            return ls;
        }

        public bool InBD(string autorOMateriaOEditorial, string opcion)
        {
            OI().Parametro("AUTOROMATERIAOEDITORIAL", "S", autorOMateriaOEditorial);
            OI().Parametro("OPCION", "S", opcion);
            OI().Parametro("ENBD", "N", null, "O");
            OI().ComandoSP("SP_ESTAENBD");
            bool inBD = Convert.ToInt32(OI().ValorParametro("ENBD")) == 1 ? true : false;
            OI().LimpiarParametros();
            return inBD;
        }

        public Dictionary<int, bool> Save(string autorOMateriaOEditorial, string opcion)
        {
            OI().Parametro("PID", "N", null, "O");
            OI().Parametro("AUTOROMATERIAOEDITORIAL", "S", autorOMateriaOEditorial);
            OI().Parametro("OPCION", "S", opcion);
            OI().ComandoSP("SP_GUARDAR");
            Dictionary<int, bool> d = new Dictionary<int, bool>();
            if (Afectadas() == 0)
            {

                d.Add(-1, false);
                return d;
            }
            d.Add(Convert.ToInt32(OI().ValorParametro("PID")), true);
            OI().LimpiarParametros();
            return d;
        }

        public int GetIDXNombre(string autorOMateriaOEditorial, string opcion)
        {
            OI().Parametro("AUTOROMATERIAOEDITORIAL", "S", autorOMateriaOEditorial);
            OI().Parametro("OPCION", "S", opcion);
            OI().Parametro("ID", "N", null, "O");
            OI().ComandoSP("SP_GETIDXNOMBRE");
            object idParametro = OI().ValorParametro("ID");
            int id = idParametro != null && idParametro != DBNull.Value
                    ? Convert.ToInt32(idParametro)
                    : -1;
            OI().LimpiarParametros();
            return id;
        }

        public int SaveLibro(Libro libro)
        {
            SetParamsLibro(libro);
            OI().ComandoSP("SP_GUARDARLIBRO");
            OI().LimpiarParametros();
            return OI().ResultadoSP();
        }

        public int GetIdLibroXCodigo(int codigo)
        {
            OI().Parametro("CODIGO", "N", codigo);
            OI().Parametro("ID", "N", null, "O");
            OI().ComandoSP("SP_GETIDLIBROXCODIGO");
            int id = Convert.ToInt32(OI().ValorParametro("ID"));
            OI().LimpiarParametros();
            return id;
        }

        public int UpdateLibro(Libro libro)
        {
            SetParamsLibro(libro);
            OI().ComandoSP("SP_MODIFICARLIBRO");
            OI().LimpiarParametros();
            return OI().ResultadoSP();
        }

        public void SetParamsLibro(Libro libro)
        {
            OI().Parametro("ID", "N", libro.Id);
            OI().Parametro("CODIGO", "N", libro.Codigo);
            OI().Parametro("ISBN", "S", libro.Isbn);
            OI().Parametro("EAN13", "S", libro.Ean13);
            OI().Parametro("TITULO", "S", libro.Titulo);
            OI().Parametro("ID_AUTOR", "N", libro.IdAutor);
            OI().Parametro("ID_SERIE", "S", libro.IdSerie);
            OI().Parametro("ID_EDITORIAL", "N", libro.IdEditorial);
            OI().Parametro("ID_MATERIA", "N", libro.IdMateria);
            OI().Parametro("PRECIOLISTA", "D", libro.PrecioLista);
            OI().Parametro("PRECIO", "D", libro.Precio);
            OI().Parametro("CONTADO", "D", libro.Contado);
            OI().Parametro("EXISTENCIA", "N", libro.Existencia);
            OI().Parametro("NOVEDAD", "B", libro.Novedad);
            OI().Parametro("DISPONIBLE", "B", libro.Disponible);
            OI().Parametro("CARGADOULTVEZPOREXCEL", "B", libro.CargadoUltimaVezPorExcel);

        }

        public List<Object> GetCoincidentes(string autorOMateriaOEditorial, string opcion)
        {
            OI().Parametro("AUTOROMATERIAOEDITORIAL", "S", autorOMateriaOEditorial);
            OI().Parametro("OPCION", "S", opcion);
            DataTable t = OI().ObtenerDt("SP_GETCOINCIDENTES");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return new List<Object>();
            }
            if (opcion == "A")
            {
                List<Autore> la = new List<Autore>();
                foreach (DataRow r in t.Rows)
                {
                    Autore a = new Autore();
                    a.Id = Convert.ToInt32(r["id"]);
                    a.Autor = r["autor"].ToString();
                    la.Add(a);
                }
                return la.Cast<Object>().ToList();
            }
            else if (opcion == "E")
            {
                List<Editoriales> le = new List<Editoriales>();
                foreach (DataRow r in t.Rows)
                {
                    Editoriales e = new Editoriales();
                    e.Id = Convert.ToInt32(r["id"]);
                    e.Editorial = r["editorial"].ToString();
                    le.Add(e);
                }
                return le.Cast<Object>().ToList();
            }
            else if (opcion == "M")
            {
                List<Materiaa> lm = new List<Materiaa>();
                foreach (DataRow r in t.Rows)
                {
                    Materiaa m = new Materiaa();
                    m.Id = Convert.ToInt32(r["id"]);
                    m.Materia = r["materia"].ToString();
                    lm.Add(m);
                }
                return lm.Cast<Object>().ToList();
            }
            else
            {
                List<Seriee> ls = new List<Seriee>();
                foreach (DataRow r in t.Rows)
                {
                    Seriee s = new Seriee();
                    s.Id = Convert.ToInt32(r["id"]);
                    s.Serie = r["serie"].ToString();
                    ls.Add(s);
                }
                return ls.Cast<Object>().ToList();
            }
        }

        public List<Libro> GetLibrosXTituloAutorEditorialYOCodigo(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo)
        {
            if (titulo == null || titulo == "" || titulo == string.Empty)
            {
                titulo = "-";
            }
            if (autor == null)
            {
                autor = new Autore();
                autor.Id = -1;
            }
            if (editorial == null)
            {
                editorial = new Editoriales();
                editorial.Id = -1;
            }
            if (serie == null)
            {
                serie = new Seriee();
                serie.Id = -1;
            }
            if (materia == null)
            {
                materia = new Materiaa();
                materia.Id = -1;
            }
            OI().Parametro("TITULO", "S", titulo);
            OI().Parametro("ID_SERIE", "N", serie.Id);
            OI().Parametro("ID_AUTOR", "N", autor.Id);
            OI().Parametro("ID_EDITORIAL", "N", editorial.Id);
            OI().Parametro("ID_MATERIA", "N", materia.Id);
            OI().Parametro("CODIGO", "N", codigo);
            DataTable t = OI().ObtenerDt("SP_GETLIBROSXTITULOAUTOREDITORIALYOCODIGO");
            OI().LimpiarParametros();
            List<Libro> ll = new List<Libro>();
            foreach (DataRow r in t.Rows)
            {
                Libro l = new Libro();
                l.Id = Convert.ToInt32(r["id"]);
                l.Codigo = Convert.ToInt32(r["codigo"]);
                l.Isbn = r["isbn"].ToString();
                l.Ean13 = r["ean13"].ToString();
                l.Titulo = r["titulo"].ToString();
                l.IdAutor = Convert.ToInt32(r["id_autor"]);
                l.Autor = r["autor"].ToString();
                l.IdSerie = Convert.ToInt32(r["id_serie"]);
                l.Serie = r["serie"].ToString();
                l.IdEditorial = Convert.ToInt32(r["id_editorial"]);
                l.Editorial = r["editorial"].ToString();
                l.IdMateria = Convert.ToInt32(r["id_materia"]);
                l.Materia = r["materia"].ToString();
                l.PrecioLista = Math.Round(Convert.ToDecimal(r["precioLista"]), 2);
                l.Precio = Math.Round(Convert.ToDecimal(r["precio"]), 2);
                l.Contado = Math.Round(Convert.ToDecimal(r["contado"]), 2);
                l.Existencia = Convert.ToInt32(r["existencia"]);
                l.Novedad = Convert.ToBoolean(r["novedad"]);
                l.Disponible = Convert.ToBoolean(r["disponible"]);
                l.FechaMod = Convert.ToDateTime(r["fec_mod"]);
                l.CargadoUltimaVezPorExcel = Convert.ToBoolean(r["cargadoUltimaVezPorExcel"]);
                ll.Add(l);
            }
            return ll;
        }

        public List<Autore> GetAllAutores()
        {
            DataTable t = OI().ObtenerDt("SP_GETALLAUTORES");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return new List<Autore>();
            }
                List<Autore> la = new List<Autore>();
                foreach (DataRow r in t.Rows)
                {
                    Autore a = new Autore();
                    a.Id = Convert.ToInt32(r["id"]);
                    a.Autor = r["autor"].ToString();
                    la.Add(a);
                }
                return la;
        }

        public List<Editoriales> GetAllEditoriales()
        {
            DataTable t = OI().ObtenerDt("SP_GETALLEDITORIALES");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return new List<Editoriales>();
            }
            List<Editoriales> le = new List<Editoriales>();
            foreach (DataRow r in t.Rows)
            {
                Editoriales e = new Editoriales();
                e.Id = Convert.ToInt32(r["id"]);
                e.Editorial = r["editorial"].ToString();
                le.Add(e);
            }
            return le;
        }

        public List<Materiaa> GetAllMaterias()
        {
            DataTable t = OI().ObtenerDt("SP_GETALLMATERIAS");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return new List<Materiaa>();
            }

            List<Materiaa> lm = new List<Materiaa>();
            foreach (DataRow r in t.Rows)
            {
                Materiaa m = new Materiaa();
                m.Id = Convert.ToInt32(r["id"]);
                m.Materia = r["materia"].ToString();
                lm.Add(m);
            }
            return lm;
        }

        public List<Seriee> GetAllSeries()
        {
            DataTable t = OI().ObtenerDt("SP_GETALLSERIES");
            OI().LimpiarParametros();
            if (t.Rows.Count == 0)
            {
                return new List<Seriee>();
            }
            List<Seriee> ls = new List<Seriee>();
            foreach (DataRow r in t.Rows)
            {
                Seriee s = new Seriee();
                s.Id = Convert.ToInt32(r["id"]);
                s.Serie = r["serie"].ToString();
                ls.Add(s);
            }
            return ls;
        }

        public int GetUltimoCodigoLibro()
        {
            Parametro("PCODIGO", "N", null, "O");
            ComandoSP("SP_TRAERULTIMOCODIGOLIBRO");
            int codigo;
            if (HuboErrores)
            {
                codigo = -1;
            }
            else
            {
                codigo = int.Parse(ValorParametro("PCODIGO").ToString());
            }
            LimpiarErrores();
            LimpiarParametros();
            return codigo;
        }

        public LibroTodosLosPreciosDTO GetLibroConPrecioXFechaYOReserva(DateTime fecha, int reserva, int codigo)
        {
            Parametro("PIDLIBRO", "N", codigo);
            Parametro("PIDRESERVA", "N", reserva);
            Parametro("PFECHA", "F", fecha);
            DataTable t = ObtenerDt("SP_GETPRECIOLIBROXFECHAORESERVA");
            if (t.Rows.Count == 0)
            {
                return null;
            }
            LibroTodosLosPreciosDTO l = new LibroTodosLosPreciosDTO();
            l.Id = Convert.ToInt32(t.Rows[0]["id"]);
            l.Codigo = Convert.ToInt32(t.Rows[0]["codigo"]);
            l.Isbn = t.Rows[0]["isbn"].ToString();
            l.Ean13 = t.Rows[0]["ean13"].ToString();
            l.Titulo = t.Rows[0]["titulo"].ToString();
            l.IdAutor = Convert.ToInt32(t.Rows[0]["id_autor"]);
            l.IdSerie = Convert.ToInt32(t.Rows[0]["id_serie"]);
            l.IdEditorial = Convert.ToInt32(t.Rows[0]["id_editorial"]);
            l.IdMateria = Convert.ToInt32(t.Rows[0]["id_materia"]);
            l.PrecioLista = Math.Round(Convert.ToDecimal(t.Rows[0]["precioLista"]), 2);
            l.Precio = Math.Round(Convert.ToDecimal(t.Rows[0]["precio"]), 2);
            l.Contado = Math.Round(Convert.ToDecimal(t.Rows[0]["contado"]), 2);
            l.Existencia = Convert.ToInt32(t.Rows[0]["existencia"]);
            l.Novedad = Convert.ToBoolean(t.Rows[0]["novedad"]);
            l.Disponible = Convert.ToBoolean(t.Rows[0]["disponible"]);
            l.PrecioFinal = Math.Round(Convert.ToDecimal(t.Rows[0]["precioFinal"]), 2);
            LimpiarErrores();
            LimpiarParametros();
            return l;
        }

        public List<Proveedore> GetProveedoresCompletos()
        {
            DataTable t = ObtenerDt("SP_TRAERPROVEEDORES");
            List<Proveedore> lp = new List<Proveedore>();
            foreach (DataRow r in t.Rows)
            {
                Proveedore p = new Proveedore();
                p.Id = Convert.ToInt32(r["id"]);
                p.Nombre = r["nombre"].ToString();
                p.Contacto = r["contacto"]?.ToString();
                p.Telefono = r["telefono"]?.ToString();
                p.Email = r["email"]?.ToString();
                p.Direccion = r["direccion"]?.ToString();
                p.Observaciones = r["observaciones"]?.ToString();
                p.Activo = r["activo"] != DBNull.Value ? Convert.ToBoolean(r["activo"]) : true;
                lp.Add(p);
            }
            LimpiarErrores();
            LimpiarParametros();
            return lp;
        }

        public int AgregarProveedor(Proveedore proveedor)
        {
            Parametro("PNOMBRE", "S", proveedor.Nombre);
            Parametro("PCONTACTO", "S", proveedor.Contacto);
            Parametro("PTELEFONO", "S", proveedor.Telefono);
            Parametro("PEMAIL", "S", proveedor.Email);
            Parametro("PDIRECCION", "S", proveedor.Direccion);
            Parametro("POBSERVACIONES", "S", proveedor.Observaciones);
            Parametro("PACTIVO", "B", proveedor.Activo);
            ComandoSP("SP_AGREGARPROVEDOR");
            int resultado = ResultadoSP();
            LimpiarParametros();
            return resultado;
        }

        public int ModificarProveedor(Proveedore proveedor)
        {
            Parametro("PID", "N", proveedor.Id);
            Parametro("PNOMBRE", "S", proveedor.Nombre);
            Parametro("PCONTACTO", "S", proveedor.Contacto);
            Parametro("PTELEFONO", "S", proveedor.Telefono);
            Parametro("PEMAIL", "S", proveedor.Email);
            Parametro("PDIRECCION", "S", proveedor.Direccion);
            Parametro("POBSERVACIONES", "S", proveedor.Observaciones);
            Parametro("PACTIVO", "B", proveedor.Activo);
            ComandoSP("SP_MODIFICARPROVEDOR");
            int resultado = ResultadoSP();
            LimpiarParametros();
            return resultado;
        }
    }
}
