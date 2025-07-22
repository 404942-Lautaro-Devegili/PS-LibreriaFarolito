using ExcelDataReader;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using LibreriaBack.Datos.Implementacion;
using System.Data;

namespace LibreriaBack.Servicios.Implementacion
{
    public class LibroService : ILibroService
    {
        LibroDao ld;
        private int autores;
        private int editoriales;
        private int materias;
        private int series;
        private bool checkearAuxiliares;
        Dictionary<string, int> AllAutores;
        Dictionary<string, int> AllEditoriales;
        Dictionary<string, int> AllMaterias;
        Dictionary<string, int> AllSeries;
        public LibroService()
        {
            ld = new LibroDao();
            autores = 0;
            editoriales = 0;
            materias = 0;
            series = 0;
            AllAutores = new Dictionary<string, int>(StringComparer.Ordinal);
            AllEditoriales = new Dictionary<string, int>(StringComparer.Ordinal);
            AllMaterias = new Dictionary<string, int>(StringComparer.Ordinal);
            AllSeries = new Dictionary<string, int>(StringComparer.Ordinal);
            checkearAuxiliares = true;
        }

        public Libro TraerLibroXCodigo(int codigo)
        {
            return ld.GetLibroXCodigo(codigo);
        }

        public Libro TraerLibroXId(int id)
        {
            return ld.GetLibroXId(id);
        }

        public List<Autore> TraerAutores()
        {
            return ld.GetAutores();
        }

        public List<Editoriales> TraerEditoriales()
        {
            return ld.GetEditoriales();
        }

        public List<Materiaa> TraerMaterias()
        {
            return ld.GetMaterias();
        }

        public List<Seriee> TraerSeries()
        {
            return ld.GetSeries();
        }

        public bool EstaEnBD(string autorOMateriaOEditorial, string opcion)
        {
            bool enBD = ld.InBD(autorOMateriaOEditorial, opcion);
            return enBD;
        }

        public Dictionary<int, bool> Guardar(string autorOMateriaOEditorial, string opcion)
        {
            Dictionary<int, bool> seGuardo = ld.Save(autorOMateriaOEditorial, opcion);
            return seGuardo;
        }

        public int TraerIDXNombre(string autorOMateriaOEditorial, string opcion)
        {
            int id = ld.GetIDXNombre(autorOMateriaOEditorial, opcion);
            return id;
        }

        public bool CargarVariablesGlobales()
        {
            try
            {
                AllAutores = ld.GetAllAutores().ToDictionary(a => a.Autor, a => a.Id, StringComparer.OrdinalIgnoreCase);
                AllEditoriales = ld.GetAllEditoriales().ToDictionary(e => e.Editorial, e => e.Id, StringComparer.OrdinalIgnoreCase);
                AllMaterias = ld.GetAllMaterias().ToDictionary(m => m.Materia, m => m.Id, StringComparer.OrdinalIgnoreCase);
                AllSeries = ld.GetAllSeries().ToDictionary(s => s.Serie, s => s.Id, StringComparer.OrdinalIgnoreCase);
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar variables globales: {ex.Message}");
                return false;
            }
        }
        public int GuardarOModificarLibro(Libro libro, int opcion)
        {
            //Opcion 1: Guardar. Opcion 2: Modificar
            int guardadoOmodificadoOConError = 0;
            if (checkearAuxiliares)
            {
                libro = CheckearAutorSerieMateriaEditorial(libro, "N");
            }
            if (opcion == 1)
            {
                guardadoOmodificadoOConError = ld.SaveLibro(libro);
            }
            else
            {
                guardadoOmodificadoOConError = ld.UpdateLibro(libro);
            }
            return guardadoOmodificadoOConError; // 1: Guardado o modificado. -1: Error. 0: Sin cambios.
        }

        public List<int> TraerLibrosDesdeExcelViejo()
        {
            string rutaArchivo = "C:\\FoxPro\\prueba.xlsx";
            List<int> librosAgregadosOModificados = ProcesarExcel(rutaArchivo);
            return librosAgregadosOModificados;

        }

        public List<int> TraerLibrosDesdeExcel(string rutaArchivo)
        {
            List<int> librosAgregadosOModificados = ProcesarExcelLeas(rutaArchivo);
            return librosAgregadosOModificados;
        }

        public event Action<int, int, int> ProgresoActualizado;

        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*////////////////////////////////////////NUEVO////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/

        public List<int> ProcesarExcelLeas(string rutaArchivo)
        {
            int librosAgregados = 0;
            int librosModificados = 0;
            int librosQueDieronError = 0;
            int librosSinCambios = 0;
            int total = 0;
            AllAutores.Clear();
            AllEditoriales.Clear();
            AllMaterias.Clear();
            AllSeries.Clear();

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            CargarVariablesGlobales();

            using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var dataTable = dataSet.Tables[0];

                    int blockSize = 1000;
                    int totalFilas = dataTable.Rows.Count;
                    checkearAuxiliares = false;
                    for (int start = 1; start < totalFilas; start += blockSize)
                    {
                        var rows = dataTable.AsEnumerable().Skip(start).Take(blockSize).ToList();
                        List<Libro> librosBloque = rows.Select(CrearLibroDesdeFila).ToList();

                        var codigosBloque = librosBloque.Select(l => l.Codigo.Value).ToList();
                        var librosExistentes = ld.GetLibrosXBloqueCodigo(codigosBloque);

                        foreach (Libro libro in librosBloque)
                        {
                            total++;
                            Libro l = CheckearAutorSerieMateriaEditorial(libro, "E");
                            libro.IdAutor = l.IdAutor;
                            libro.IdEditorial = l.IdEditorial;
                            libro.IdMateria = l.IdMateria;
                            libro.IdSerie = l.IdSerie;
                            libro.Contado = Math.Round(libro.Contado.Value, 2);
                            libro.Precio = Math.Round(libro.Precio.Value, 2);
                            libro.PrecioLista = Math.Round(libro.PrecioLista.Value, 2);


                            if (librosExistentes.TryGetValue(libro.Codigo.Value, out var libroExistente))
                            {
                                libroExistente.Contado = Math.Round(libroExistente.Contado.Value, 2);
                                libroExistente.Precio = Math.Round(libroExistente.Precio.Value, 2);
                                libroExistente.PrecioLista = Math.Round(libroExistente.PrecioLista.Value, 2);
                                libro.Id = libroExistente.Id;
                                libroExistente.CargadoUltimaVezPorExcel = libro.CargadoUltimaVezPorExcel;

                                if (Iguales(libroExistente, libro))
                                {
                                    librosSinCambios++;
                                }
                                else
                                {
                                    int afectadas = GuardarOModificarLibro(libro, 2);
                                    if (afectadas == 1)
                                        librosModificados++;
                                    else if (afectadas == -1)
                                        librosQueDieronError++;
                                }
                            }
                            else
                            {
                                int afectadas = GuardarOModificarLibro(libro, 1);
                                if (afectadas == 1)
                                    librosAgregados++;
                                else if (afectadas == -1)
                                    librosQueDieronError++;
                            }
                        }
                        int procesados = librosAgregados + librosModificados + librosQueDieronError;
                        ProgresoActualizado?.Invoke((procesados * 100) / totalFilas, (procesados), (totalFilas - procesados));

                    }
                }
            }
            stopwatch.Stop();
            int elapsed = (int)stopwatch.ElapsedMilliseconds;


            List<int> librosAgregadosOModificados = new List<int> { librosAgregados, librosModificados, librosQueDieronError, librosSinCambios, total, autores, editoriales, materias, series, elapsed };
            autores = 0;
            editoriales = 0;
            materias = 0;
            series = 0;
            return librosAgregadosOModificados;
        }

        private Libro CrearLibroDesdeFila(DataRow row)
        {
            return new Libro
            {
                Codigo = Convert.ToInt32(row[1]),
                Isbn = !string.IsNullOrWhiteSpace(row[2]?.ToString()) ? row[2].ToString().Trim() : "-",
                Ean13 = !string.IsNullOrWhiteSpace(row[3]?.ToString()) ? row[3].ToString().Trim() : "-",
                Titulo = !string.IsNullOrWhiteSpace(row[4]?.ToString()) ? row[4].ToString().Trim() : "-",
                Precio = Math.Round(Convert.ToDecimal(row[9]) * 1.15M, 2),
                PrecioLista = Math.Round(Convert.ToDecimal(row[9]), 2),
                Contado = Math.Round(Convert.ToDecimal(row[9]), 2),
                Autor = !string.IsNullOrWhiteSpace(row[5]?.ToString()) ? row[5].ToString().Trim() : "No tiene",
                Editorial = !string.IsNullOrWhiteSpace(row[7]?.ToString()) ? row[7].ToString().Trim() : "No tiene",
                Materia = !string.IsNullOrWhiteSpace(row[8]?.ToString()) ? row[8].ToString().Trim() : "No tiene",
                Serie = !string.IsNullOrWhiteSpace(row[6]?.ToString()) ? row[6].ToString().Trim() : "No tiene",
                Existencia = 0,
                Novedad = !string.IsNullOrWhiteSpace(row[0]?.ToString()) ? true : false,
                Disponible = true,
                CargadoUltimaVezPorExcel = true
            };
        }


        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/
        /*/////////////////////////////////////////////////////////////////////////////////////////*/

        public List<int> ProcesarExcelLeasViejo(string rutaArchivo)
        {
            int librosAgregados = 0;
            int librosModificados = 0;
            int librosQueDieronError = 0;
            int librosSinCambios = 0;
            int total = 0;
            using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var dataTable = dataSet.Tables[0];
                    var libros = new List<Libro>();
                    int afectadas = 0;
                    int totalFilas = dataTable.Rows.Count - 1;

                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var row = dataTable.Rows[i];
                        var libro = new Libro
                        {
                            Codigo = Convert.ToInt32(row[1]),
                            Isbn = !string.IsNullOrEmpty(row[2].ToString()) ? row[2].ToString() : "-",
                            Ean13 = !string.IsNullOrEmpty(row[3].ToString()) ? row[3].ToString() : "-",
                            Titulo = !string.IsNullOrEmpty(row[4].ToString()) ? row[4].ToString() : "-",
                            //Serie = !string.IsNullOrEmpty(row[6].ToString()) ? row[6].ToString() : "-",
                            PrecioLista = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9]), 2) : 0,
                            Precio = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9])*1.1M, 2) : 0,
                            Contado = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9]), 2) : 0,
                            //Existencia = !string.IsNullOrEmpty(row[10].ToString()) ? Convert.ToInt32(row[10]) : 0,
                            Existencia = 0,
                            Novedad = !string.IsNullOrEmpty(row[0].ToString()) ? true : false,
                            Disponible = true,
                            CargadoUltimaVezPorExcel = true
                        };
                        total++;

                        libro.PrecioLista = decimal.Parse(libro.PrecioLista.Value.ToString("0.00"));
                        libro.Precio = decimal.Parse(libro.Precio.Value.ToString("0.00"));
                        libro.Contado = decimal.Parse(libro.Contado.Value.ToString("0.00"));

                        libro.Autor = row[5].ToString();
                        libro.Editorial = row[7].ToString();
                        libro.Materia = row[8].ToString();
                        libro.Serie = row[6].ToString();

                        libro = CheckearAutorSerieMateriaEditorial(libro, "E");

                        int idLibro = ld.GetIdLibroXCodigo(libro.Codigo.Value);
                        if (idLibro > 0)
                        {
                            Libro l = ld.GetLibroXCodigo(libro.Codigo.Value);
                            libro.Id = l.Id;
                            if (l != null && Iguales(l, libro))
                            {
                                librosSinCambios++;
                            }
                            else
                            {
                                afectadas = GuardarOModificarLibro(libro, 2);
                                if (afectadas == 1)
                                {
                                    librosModificados++;
                                }
                                else if (afectadas == -1)
                                {
                                    librosQueDieronError++;
                                }
                                else
                                {
                                    librosSinCambios++;
                                }
                            }
                        }
                        else
                        {
                            // Libro para agregar
                            libro.Id = 0;
                            afectadas = GuardarOModificarLibro(libro, 1);
                            if (afectadas == 1)
                            {
                                librosAgregados++;
                            }
                            else if (afectadas == -1)
                            {
                                librosQueDieronError++;
                            }
                            else
                            {
                                librosSinCambios++;
                            }
                        }

                        int progreso = (i * 100) / totalFilas;
                        int librosProcesados = i;
                        int librosRestantes = totalFilas - i;
                        ProgresoActualizado?.Invoke(progreso, librosProcesados, librosRestantes);
                    }
                }
            }
            List<int> librosAgregadosOModificados = new List<int> { librosAgregados, librosModificados, librosQueDieronError, librosSinCambios, total, autores, editoriales, materias, series };
            autores = 0;
            editoriales = 0;
            materias = 0;
            series = 0;
            return librosAgregadosOModificados;
        }


        public List<int> ProcesarExcel(string rutaArchivo)
        {
            int librosAgregados = 0;
            int librosModificados = 0;
            int librosQueDieronError = 0;
            int librosSinCambios = 0;
            int total = 0;
            using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var dataTable = dataSet.Tables[0];
                    var libros = new List<Libro>();
                    int afectadas = 0;

                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var row = dataTable.Rows[i];
                        var libro = new Libro
                        {
                            Codigo = Convert.ToInt32(row[1]),
                            Isbn = !string.IsNullOrEmpty(row[2].ToString()) ? row[2].ToString() : "-",
                            Ean13 = !string.IsNullOrEmpty(row[3].ToString()) ? row[3].ToString() : "-",
                            Titulo = !string.IsNullOrEmpty(row[4].ToString()) ? row[4].ToString() : "-",
                            //Serie = !string.IsNullOrEmpty(row[6].ToString()) ? row[6].ToString() : "-",
                            PrecioLista = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9]), 2) : 0,
                            Precio = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9])*1.15M, 2) : 0,
                            Contado = !string.IsNullOrEmpty((row[9]).ToString()) ? Math.Round(Convert.ToDecimal(row[9]), 2) : 0,
                            Existencia = !string.IsNullOrEmpty(row[10].ToString()) ? Convert.ToInt32(row[10]) : 0,
                            Novedad = !string.IsNullOrEmpty(row[0].ToString()) ? true : false,
                            Disponible = true
                        };
                        total++;

                        libro.PrecioLista = decimal.Parse(libro.PrecioLista.Value.ToString("0.00"));
                        libro.Precio = decimal.Parse(libro.Precio.Value.ToString("0.00"));
                        libro.Contado = decimal.Parse(libro.Contado.Value.ToString("0.00"));

                        libro.Autor = row[5].ToString();
                        libro.Editorial = row[7].ToString();
                        libro.Materia = row[8].ToString();
                        libro.Serie = row[6].ToString();

                        libro = CheckearAutorSerieMateriaEditorial(libro, "E");

                        int idLibro = ld.GetIdLibroXCodigo(libro.Codigo.Value);
                        if (idLibro > 0)
                        {
                            Libro l = ld.GetLibroXCodigo(libro.Codigo.Value);
                            libro.Id = l.Id;
                            if (l != null && Iguales(l, libro))
                            {
                                librosSinCambios++;
                            }
                            else
                            {
                                afectadas = GuardarOModificarLibro(libro, 2);
                                if (afectadas == 1)
                                {
                                    librosModificados++;
                                }
                                else if (afectadas == -1)
                                {
                                    librosQueDieronError++;
                                }
                                else
                                {
                                    librosSinCambios++;
                                }
                            }
                        }
                        else
                        {
                            // Libro para agregar
                            libro.Id = 0;
                            afectadas = GuardarOModificarLibro(libro, 1);
                            if (afectadas == 1)
                            {
                                librosAgregados++;
                            }
                            else if (afectadas == -1)
                            {
                                librosQueDieronError++;
                            }
                            else
                            {
                                librosSinCambios++;
                            }
                        }
                    }
                }
            }
            List<int> librosAgregadosOModificados = new List<int> { librosAgregados, librosModificados, librosQueDieronError, librosSinCambios, total, autores, editoriales, materias, series };
            autores = 0;
            editoriales = 0;
            materias = 0;
            series = 0;
            return librosAgregadosOModificados;
        }

        public bool Iguales(Libro l1, Libro l2)
        {
            bool ret = false;
            bool ret2 = false;
            if (l1.Id == l2.Id && l1.Codigo == l2.Codigo && l1.Isbn == l2.Isbn &&
                l1.Ean13 == l2.Ean13 && l1.Titulo == l2.Titulo && l1.IdAutor == l2.IdAutor &&
                l1.Autor.ToLower() == l2.Autor.ToLower() && l1.IdSerie == l2.IdSerie && l1.Serie.ToLower() == l2.Serie.ToLower() &&
                l1.IdEditorial == l2.IdEditorial && l1.Editorial == l2.Editorial && l1.IdMateria == l2.IdMateria &&
                l1.Materia.ToLower() == l2.Materia.ToLower() && l1.PrecioLista == l2.PrecioLista && l1.Precio == l2.Precio &&
                l1.Contado == l2.Contado && l1.Novedad == l2.Novedad)
            {
                ret = true;
            } else
            {
                ret = false;
            }
            if (Equals(l1, l2))
            {
                ret2 = true;
            }
            return ret;
        }

        public List<object> TraerCoincidentes(string autorOMateriaOEditorial, string opcion)
        {
            return ld.GetCoincidentes(autorOMateriaOEditorial, opcion);
        }

        public Libro CheckearAutorSerieMateriaEditorial(Libro libro, string opcion)
        { // Opcion E: Excel. Opcion N: Nuevo, escrito en el txtbox.
            // Procesar Autor
            string autor = libro.Autor;
            if (!string.IsNullOrEmpty(autor) && autor != "-")
            {
                if (AllAutores.TryGetValue(autor, out int autorId))
                {
                    // Si el autor ya está en el diccionario, asigna el ID directamente.
                    libro.IdAutor = autorId;
                }
                else
                {
                    Dictionary<int, bool> d = Guardar(autor, "A");
                    if (d.Values.First() == true)
                    {
                        int id = d.Keys.First();
                        libro.IdAutor = id;
                        AllAutores[autor] = id;
                        autores++;
                    }
                    else
                    {
                        int id = TraerIDXNombre(autor, "A");
                        libro.IdAutor = id != -1 ? id : 1;

                        // Si el ID es válido, agrega al diccionario.
                        if (id != -1)
                        {
                            if (!AllAutores.ContainsKey(autor))
                            {
                                autores++;
                            }
                            AllAutores[autor] = id;
                        }
                    }
                }
            } else
            {
                if ((opcion == "N" && (libro.IdAutor <= 1 || libro.IdAutor == null)) || opcion == "E") //Si no es un Autor puesto a mano y no se 
                                                                                                       //seleccionó uno, o si es Excel (que sería si la 
                {                                                                                      //columna está vacía en el excel), se le pone no tiene.
                    libro.IdAutor = 1;
                }
            }


            // Procesar Editorial
            string editorial = libro.Editorial;
            if (!string.IsNullOrEmpty(editorial) && editorial != "-")
            {
                if (AllEditoriales.TryGetValue(editorial, out int editorialId))
                {
                    // Si el autor ya está en el diccionario, asigna el ID directamente.
                    libro.IdEditorial = editorialId;
                }
                else
                {
                    Dictionary<int, bool> d = Guardar(editorial, "E");
                    if (d.Values.First() == true)
                    {
                        int id = d.Keys.First();
                        libro.IdEditorial = id;
                        AllEditoriales[editorial] = id;
                        editoriales++;
                    }
                    else
                    {
                        int id = TraerIDXNombre(editorial, "E");
                        libro.IdEditorial = id != -1 ? id : 1;

                        // Si el ID es válido, agrega al diccionario.
                        if (id != -1)
                        {
                            if (!AllEditoriales.ContainsKey(editorial))
                            {
                                editoriales++;
                            }
                            AllEditoriales[editorial] = id;
                        }
                    }
                }
            } else
            {
                if ((opcion == "N" && (libro.IdEditorial <= 1 || libro.IdEditorial == null)) || opcion == "E") //Si no es una Editorial puesta a mano y no se 
                                                                                                               //seleccionó una, o si es Excel (que sería si la 
                                                                                                               //columna está vacía en el excel), se le pone no tiene.
                {
                    libro.IdEditorial = 1;
                }
            }

            // Procesar Materia
            string materia = libro.Materia;
            if (!string.IsNullOrEmpty(materia) && materia != "-")
            {
                if (AllMaterias.TryGetValue(materia, out int materiaId))
                {
                    // Si la materia ya está en el diccionario, asigna el ID directamente.
                    libro.IdMateria = materiaId;
                }
                else
                {
                    Dictionary<int, bool> d = Guardar(materia, "M");
                    if (d.Values.First() == true)
                    {
                        int id = d.Keys.First();
                        libro.IdMateria = id;
                        AllMaterias[materia] = id;
                        materias++;
                    }
                    else
                    {
                        int id = TraerIDXNombre(materia, "M");
                        libro.IdMateria = id != -1 ? id : 1;

                        // Si el ID es válido, agrega al diccionario.
                        if (id != -1)
                        {
                            if (!AllMaterias.ContainsKey(materia))
                            {
                                materias++;
                            }
                            AllMaterias[materia] = id;
                        }
                    }
                }
            } else
            {
                if ((opcion == "N" && (libro.IdMateria <= 1 || libro.IdMateria == null)) || opcion == "E") //Si no es una Materia puesta a mano y no se 
                {                                                                                          //seleccionó una, o si es Excel (que sería si la 
                    libro.IdMateria = 1;                                                                   //columna está vacía en el excel), se le pone no tiene.
                }
            }

            // Procesar Serie

            string serie = libro.Serie;
            if (!string.IsNullOrEmpty(serie) && serie != "-")
            {
                if (AllSeries.TryGetValue(serie, out int serieId))
                {
                    // Si la Serie ya está en el diccionario, asigna el ID directamente.
                    libro.IdSerie = serieId;
                }
                else
                {
                    Dictionary<int, bool> d = Guardar(serie, "S");
                    if (d.Values.First() == true)
                    {
                        int id = d.Keys.First();
                        libro.IdSerie = id;
                        AllSeries[serie] = id;
                        series++;
                    }
                    else
                    {
                        int id = TraerIDXNombre(serie, "S");
                        libro.IdSerie = id != -1 ? id : 1;

                        // Si el ID es válido, agrega al diccionario.
                        if (id != -1)
                        {
                            if (!AllSeries.ContainsKey(serie))
                            {
                                series++;
                            }
                            AllSeries[serie] = id;
                        }
                    }
                }
            } else
            {
                if ((opcion == "N" && (libro.IdSerie <= 1 || libro.IdSerie == null)) || opcion == "E") //Si no es una Serie puesta a mano y no se 
                {                                                                                      //seleccionó una, o si es Excel (que sería si la 
                    libro.IdSerie = 1;                                                                 //columna está vacía en el excel), se le pone no tiene.
                }
            }
            return libro;
        }

        public List<Libro> TraerLibrosXTituloAutorEditorialYOCodigo(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo)
        {
            List<Libro> libros = ld.GetLibrosXTituloAutorEditorialYOCodigo(titulo, serie, autor, editorial, materia, codigo);
            return libros;
        }

        public int TraerNextCodigo()
        {
            return ld.GetUltimoCodigoLibro();
        }

        public LibroTodosLosPreciosDTO TraerLibroConPrecioXFechaYOReserva(DateTime fecha, int reserva, int codigo)
        {
            return ld.GetLibroConPrecioXFechaYOReserva(fecha, reserva, codigo);
        }

        public List<Proveedore> TraerProveedoresCompletos()
        {
            return ld.GetProveedoresCompletos();
        }

        public int AgregarProveedor(Proveedore proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
                return -1;

            return ld.AgregarProveedor(proveedor);
        }

        public int ModificarProveedor(Proveedore proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.Nombre) || proveedor.Id <= 0)
                return -1;

            return ld.ModificarProveedor(proveedor);
        }
    }
}
