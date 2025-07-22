using LibreriaBack.Datos.Entidades.Docs;
using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using System.Data;
using System.Data.SqlClient;

namespace LibreriaBack.Datos.Implementacion
{
    public class ReservaDao : HelperDao
    {

        public List<int> SaveReserva(Reserva r)
        {
            Parametro("PIDRESERVA", "N", null, "O");
            Parametro("PIDLIBRO", "N", r.IdLibro);
            Parametro("PIDCLIENTE", "N", r.IdCliente);
            Parametro("PIDPROVEEDOR", "N", r.IdProveedor);
            Parametro("PSEÑA", "N", r.Senia);
            Parametro("PSALDO", "N", r.Saldo);
            Parametro("PFCHRESERVA", "F", r.Fchreserva);
            Parametro("PIDFORMAPAGO", "N", r.IdFormaPago);
            Parametro("PLIBROPEDIDO", "B", r.LibroPedido);
            Parametro("PENTREGADA", "B", r.Entregada);
            Parametro("PACTIVA", "B", r.Activa);
            ComandoSP("SP_GUARDARRESERVA");
            AddResultados("PIDRESERVA");

            LimpiarParametros();
            return listaResultados;
        }

        public Dictionary<int, int> SaveReservas(List<Reserva> rs)
        {
            ConectarTransaccion("SP_GUARDARRESERVA");
            try
            {
                foreach (Reserva r in rs)
                {
                    Parametro("PIDRESERVA", "N", null, "O");
                    Parametro("PIDLIBRO", "N", r.IdLibro);
                    Parametro("PIDCLIENTE", "N", r.IdCliente);
                    Parametro("PIDPROVEEDOR", "N", r.IdProveedor);
                    Parametro("PSEÑA", "N", r.Senia);
                    Parametro("PSALDO", "N", r.Saldo);
                    Parametro("PFCHRESERVA", "F", r.Fchreserva);
                    Parametro("PIDFORMAPAGO", "N", r.IdFormaPago);
                    Parametro("PLIBROPEDIDO", "B", r.LibroPedido);
                    Parametro("PENTREGADA", "B", r.Entregada);
                    Parametro("PASIGNADA", "B", r.Asignada);
                    Parametro("PACTIVA", "B", r.Activa);
                    ComandoSPTransaccion();
                    AddResultadosDiccionario("PIDRESERVA");
                    LimpiarParametros();
                }
                Commit();

            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
                Rollback();
            }
            finally
            {
                Desconectar();
            }
            return mapaResultados;
        }
        public List<LibroReservadoParaAsignarDTO> GetLibrosReservadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva)
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
            if (cliente == null)
            {
                cliente = new Cliente();
                cliente.Id = -1;
            }
            Parametro("TITULO", "S", titulo);
            Parametro("ID_SERIE", "N", serie.Id);
            Parametro("ID_AUTOR", "N", autor.Id);
            Parametro("ID_EDITORIAL", "N", editorial.Id);
            Parametro("ID_MATERIA", "N", materia.Id);
            Parametro("CODIGO", "N", codigo);
            Parametro("ID_CLIENTE", "N", cliente.Id);
            Parametro("RESERVA", "N", reserva);
            DataTable t = ObtenerDt("SP_GETLIBROSDERESERVASPARAASIGNAR");
            LimpiarParametros();
            List<LibroReservadoParaAsignarDTO> lp = new List<LibroReservadoParaAsignarDTO>();
            foreach (DataRow r in t.Rows)
            {
                LibroReservadoParaAsignarDTO p = new LibroReservadoParaAsignarDTO();
                p.IdReserva = Convert.ToInt32(r["id_reserva"]);
                p.IdCliente = Convert.ToInt32(r["id_cliente"]);
                p.NomCliente = r["nom_cliente"].ToString();
                p.IdLibro = Convert.ToInt32(r["id_libro"]);
                p.Codigo = Convert.ToInt32(r["codigo"]);
                p.Isbn = r["isbn"].ToString();
                p.Ean13 = r["ean13"].ToString();
                p.Titulo = r["titulo"].ToString();
                p.IdAutor = Convert.ToInt32(r["id_autor"]);
                p.Autor = r["autor"].ToString();
                p.IdSerie = Convert.ToInt32(r["id_serie"]);
                p.Serie = r["serie"].ToString();
                p.IdEditorial = Convert.ToInt32(r["id_editorial"]);
                p.Editorial = r["editorial"].ToString();
                p.IdMateria = Convert.ToInt32(r["id_materia"]);
                p.Materia = r["materia"].ToString();
                p.PrecioLista = Math.Round(Convert.ToDecimal(r["precioLista"]), 2);
                p.Precio = Math.Round(Convert.ToDecimal(r["precio"]), 2);
                p.Contado = Math.Round(Convert.ToDecimal(r["contado"]), 2);
                p.Existencia = Convert.ToInt32(r["existencia"]);
                p.Novedad = Convert.ToBoolean(r["novedad"]);
                lp.Add(p);
            }
            return lp;
        }

        public Dictionary<int, int> AsignarReservas(List<int> lr)
        {
            ConectarTransaccion("SP_ASIGNARRESERVA");
            try
            {
                foreach (int r in lr)
                {
                    Parametro("PIDRESERVATRAER", "N", null, "O");
                    Parametro("PIDRESERVA", "N", r);
                    ComandoSPTransaccion();
                    AddResultadosDiccionario("PIDRESERVATRAER");
                    LimpiarParametros();
                }
                Commit();

            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
                Rollback();
            }
            finally
            {
                Desconectar();
            }
            return mapaResultados;
        }

        public List<LibroAsignadoParaEntregarDTO> GetLibrosAsignadosXTituloAutorEditorialXCodigoXClienteYOReserva(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo, Cliente cliente, int reserva)
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
            if (cliente == null)
            {
                cliente = new Cliente();
                cliente.Id = -1;
            }
            Parametro("TITULO", "S", titulo);
            Parametro("ID_SERIE", "N", serie.Id);
            Parametro("ID_AUTOR", "N", autor.Id);
            Parametro("ID_EDITORIAL", "N", editorial.Id);
            Parametro("ID_MATERIA", "N", materia.Id);
            Parametro("CODIGO", "N", codigo);
            Parametro("ID_CLIENTE", "N", cliente.Id);
            Parametro("RESERVA", "N", reserva);
            DataTable t = ObtenerDt("SP_GETLIBROSDERESERVASPARAENTREGAR");
            LimpiarParametros();
            List<LibroAsignadoParaEntregarDTO> lp = new List<LibroAsignadoParaEntregarDTO>();
            foreach (DataRow r in t.Rows)
            {
                LibroAsignadoParaEntregarDTO p = new LibroAsignadoParaEntregarDTO();
                p.IdReserva = Convert.ToInt32(r["id_reserva"]);
                p.IdCliente = Convert.ToInt32(r["id_cliente"]);
                p.NomCliente = r["nom_cliente"].ToString();
                p.IdLibro = Convert.ToInt32(r["id_libro"]);
                p.Senia = Math.Round(Convert.ToDecimal(r["senia"]), 2);
                p.Saldo = Math.Round(Convert.ToDecimal(r["saldo"]), 2);
                p.Codigo = Convert.ToInt32(r["codigo"]);
                p.Isbn = r["isbn"].ToString();
                p.Ean13 = r["ean13"].ToString();
                p.Titulo = r["titulo"].ToString();
                p.IdAutor = Convert.ToInt32(r["id_autor"]);
                p.Autor = r["autor"].ToString();
                p.IdSerie = Convert.ToInt32(r["id_serie"]);
                p.Serie = r["serie"].ToString();
                p.IdEditorial = Convert.ToInt32(r["id_editorial"]);
                p.Editorial = r["editorial"].ToString();
                p.IdMateria = Convert.ToInt32(r["id_materia"]);
                p.Materia = r["materia"].ToString();
                p.PrecioLista = Math.Round(Convert.ToDecimal(r["precioLista"]), 2);
                p.Precio = Math.Round(Convert.ToDecimal(r["precio"]), 2);
                p.Contado = Math.Round(Convert.ToDecimal(r["contado"]), 2);
                p.Existencia = Convert.ToInt32(r["existencia"]);
                p.Novedad = Convert.ToBoolean(r["novedad"]);
                p.Telefono = r["telefono"].ToString();
                lp.Add(p);
            }
            return lp;
        }

        public Dictionary<int, int> EntregarReservas(List<int> lr)
        {
            ConectarTransaccion("SP_ENTREGARRESERVA");
            try
            {
                foreach (int r in lr)
                {
                    Parametro("PIDRESERVATRAER", "N", null, "O");
                    Parametro("PIDRESERVA", "N", r);
                    ComandoSPTransaccion();
                    AddResultadosDiccionario("PIDRESERVATRAER");
                    LimpiarParametros();
                }
                Commit();

            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
                Rollback();
            }
            finally
            {
                Desconectar();
            }
            return mapaResultados;
        }
        public List<MontosReservaAEntregarDTO> GetMontosReservas(List<int> lr, int formaPago)
        {
            List<MontosReservaAEntregarDTO> lm = new List<MontosReservaAEntregarDTO>();
            foreach (int re in lr)
            {
                Parametro("PIDRESERVA", "N", re);
                Parametro("PFORMAPAGO", "N", formaPago);
                DataTable t = ObtenerDt("SP_GETMONTOSXRESERVA");
                LimpiarParametros();
                foreach (DataRow r in t.Rows)
                {
                    MontosReservaAEntregarDTO p = new MontosReservaAEntregarDTO();
                    p.IdReserva = Convert.ToInt32(r["id"]);
                    p.IdLibro = Convert.ToInt32(r["id_libro"]);
                    p.Senia = Math.Round(Convert.ToDecimal(r["senia"]), 2);
                    p.Saldo = Math.Round(Convert.ToDecimal(r["saldo"]), 2);
                    p.PrecioLibro = Math.Round(Convert.ToDecimal(r["precioLibro"]), 2);
                    p.ContadoLibro = Math.Round(Convert.ToDecimal(r["contadoLibro"]), 2);
                    p.formaPago = Convert.ToInt32(r["formaPago"]);
                    lm.Add(p);
                }
            }

            return lm;
        }

        public List<ReservaParaCheckearDTO> GetReservasXTituloAutorEditorialXCodigoXClienteXEstadosYOReserva(string titulo, Seriee serie, Autore autor,
                                                                                                                   Editoriales editorial, Materiaa materia,
                                                                                                                   int codigo, Cliente cliente, int reserva,
                                                                                                                   int sinAsignar, int pedida, int asignada,
                                                                                                                   int entregada)
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
            if (cliente == null)
            {
                cliente = new Cliente();
                cliente.Id = -1;
            }
            Parametro("TITULO", "S", titulo);
            Parametro("ID_SERIE", "N", serie.Id);
            Parametro("ID_AUTOR", "N", autor.Id);
            Parametro("ID_EDITORIAL", "N", editorial.Id);
            Parametro("ID_MATERIA", "N", materia.Id);
            Parametro("CODIGO", "N", codigo);
            Parametro("ID_CLIENTE", "N", cliente.Id);
            Parametro("RESERVA", "N", reserva);
            Parametro("PSINASIGNAR", "N", sinAsignar);
            Parametro("PPEDIDA", "N", pedida);
            Parametro("PASIGNADA", "N", asignada);
            Parametro("PENTREGADA", "N", entregada);
            DataTable t = ObtenerDt("SP_GETLIBROSDERESERVASPARACHECKEAR");
            LimpiarParametros();
            List<ReservaParaCheckearDTO> lp = new List<ReservaParaCheckearDTO>();
            foreach (DataRow r in t.Rows)
            {
                ReservaParaCheckearDTO p = new ReservaParaCheckearDTO();
                p.IdReserva = Convert.ToInt32(r["id_reserva"]);
                p.IdCliente = Convert.ToInt32(r["id_cliente"]);
                p.NomCliente = r["nom_cliente"].ToString();
                p.IdLibro = Convert.ToInt32(r["id_libro"]);
                p.Senia = Math.Round(Convert.ToDecimal(r["senia"]), 2);
                p.Saldo = Math.Round(Convert.ToDecimal(r["saldo"]), 2);
                p.Codigo = Convert.ToInt32(r["codigo"]);
                p.Isbn = r["isbn"].ToString();
                p.Ean13 = r["ean13"].ToString();
                p.Titulo = r["titulo"].ToString();
                p.IdAutor = Convert.ToInt32(r["id_autor"]);
                p.Autor = r["autor"].ToString();
                p.IdSerie = Convert.ToInt32(r["id_serie"]);
                p.Serie = r["serie"].ToString();
                p.IdEditorial = Convert.ToInt32(r["id_editorial"]);
                p.Editorial = r["editorial"].ToString();
                p.IdMateria = Convert.ToInt32(r["id_materia"]);
                p.Materia = r["materia"].ToString();
                p.PrecioLista = Math.Round(Convert.ToDecimal(r["precioLista"]), 2);
                p.Precio = Math.Round(Convert.ToDecimal(r["precio"]), 2);
                p.Contado = Math.Round(Convert.ToDecimal(r["contado"]), 2);
                p.Existencia = Convert.ToInt32(r["existencia"]);
                p.Novedad = Convert.ToBoolean(r["novedad"]);
                p.SinAsignar = Convert.ToBoolean(r["asignada"]);
                p.Pedida = Convert.ToBoolean(r["pedida"]);
                p.Asignada = Convert.ToBoolean(r["asignada"]);
                p.Entregada = Convert.ToBoolean(r["entregada"]);
                lp.Add(p);
            }
            return lp;
        }

        public Dictionary<int, int> ProcesoAnularReservas(List<int> lr)
        {
            ConectarTransaccion("SP_ANULARRESERVA");
            try
            {
                foreach (int r in lr)
                {
                    Parametro("PIDRESERVATRAER", "N", null, "O");
                    Parametro("PIDRESERVA", "N", r);
                    ComandoSPTransaccion();
                    AddResultadosDiccionario("PIDRESERVATRAER");
                    LimpiarParametros();
                }
                Commit();

            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
                Rollback();
            }
            finally
            {
                Desconectar();
            }
            return mapaResultados;
        }

        public List<Reserva> GetReservasXId(List<int> idsReservas)
        {
            List<Reserva> lr = new List<Reserva>();
            foreach (int idReserva in idsReservas)
            {
                Parametro("PIDRESERVA", "N", idReserva);
                DataTable t = ObtenerDt("SP_GETRESERVAXID");
                LimpiarParametros();
                foreach (DataRow r in t.Rows)
                {
                    int prov = r["id_proveedor"] == DBNull.Value ? -1 : Convert.ToInt32(r["id_proveedor"]);
                    Reserva re = new Reserva();
                    re.Id = Convert.ToInt32(r["id"]);
                    re.IdLibro = Convert.ToInt32(r["id_libro"]);
                    re.IdCliente = Convert.ToInt32(r["id_cliente"]);
                    re.IdProveedor = prov;
                    re.Senia = Math.Round(Convert.ToDecimal(r["senia"]), 2);
                    re.Saldo = Math.Round(Convert.ToDecimal(r["saldo"]), 2);
                    re.Fchreserva = Convert.ToDateTime(r["fchreserva"]);
                    //re.Fchentrega = Convert.ToDateTime(r["fchentrega"]);
                    re.IdFormaPago = Convert.ToInt32(r["id_forma_pago"]);
                    re.LibroPedido = Convert.ToBoolean(r["libroPedido"]);
                    re.Asignada = Convert.ToBoolean(r["asignada"]);
                    re.Entregada = Convert.ToBoolean(r["entregada"]);
                    re.Activa = Convert.ToBoolean(r["activa"]);
                    lr.Add(re);
                }
            }
            return lr;
        }
        public List<int> RegistrarPago(RegistrarPagoDTO pagoData)
        {
            string reservasIdsString = string.Join(",", pagoData.ReservasIds);

            Parametro("PIDPAGO", "N", null, "O");
            Parametro("PMONTO", "N", pagoData.Amount);
            Parametro("PMETODO_PAGO", "S", pagoData.Method);
            Parametro("PESTADO_PAGO", "S", pagoData.Status);
            Parametro("PPREFERENCE_ID", "S", pagoData.PreferenceId);
            Parametro("PPAYMENT_ID", "S", pagoData.PaymentId);
            Parametro("PEXTERNAL_REFERENCE", "S", pagoData.ExternalReference);
            Parametro("PRESERVAS_IDS", "S", reservasIdsString);

            ComandoSP("SP_REGISTRARPAGO");
            AddResultados("PIDPAGO");
            LimpiarParametros();

            return listaResultados;
        }

        public Pago? VerificarPagoReservas(List<int> reservasIds)
        {
            string reservasIdsString = string.Join(",", reservasIds);

            Parametro("PRESERVAS_IDS", "S", reservasIdsString);
            DataTable t = ObtenerDt("SP_VERIFICARPAGORESERVAS");
            LimpiarParametros();

            if (t.Rows.Count > 0)
            {
                DataRow r = t.Rows[0];
                return new Pago
                {
                    Id = Convert.ToInt32(r["id"]),
                    Monto = Convert.ToDecimal(r["monto"]),
                    MetodoPago = r["metodo_pago"].ToString() ?? "",
                    EstadoPago = r["estado_pago"].ToString() ?? "",
                    FechaPago = Convert.ToDateTime(r["fecha_pago"]),
                    PreferenceId = r["preference_id"]?.ToString(),
                    PaymentId = r["payment_id"]?.ToString(),
                    ExternalReference = r["external_reference"]?.ToString(),
                    ReservasIds = r["reservas_ids"].ToString() ?? ""
                };
            }

            return null;
        }
    }
}
