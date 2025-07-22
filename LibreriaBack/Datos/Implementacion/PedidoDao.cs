using LibreriaBack.Datos.Entidades.DTOs;
using LibreriaBack.Datos.Entidades.Libros;
using LibreriaBack.Datos.Entidades.Logistica;
using System.Data;
using System.Data.SqlClient;

namespace LibreriaBack.Datos.Implementacion
{
    public class PedidoDao : HelperDao
    {

        public List<Proveedore> GetProveedores()
        {
            DataTable t = ObtenerDt("SP_GETPROVEEDORES");
            List<Proveedore> lp = new List<Proveedore>();
            foreach (DataRow r in t.Rows)
            {
                Proveedore p = new Proveedore();
                p.Id = Convert.ToInt32(r["id"]);
                p.Proveedor = r["proveedor"].ToString();
                p.Direccion = r["direccion"].ToString();
                p.IdLocalidad = Convert.ToInt32(r["id_localidad"]);
                p.CodPostal = Convert.ToInt32(r["cod_postal"]);
                p.Telefono = r["telefono"].ToString();
                p.NroIva = r["nro_iva"].ToString();
                lp.Add(p);
            }
            return lp;
        }

        public List<int> SavePedido(SavePedidoDTO p)
        {
            Parametro("PIDPEDIDO", "N", null, "O");
            Parametro("PIDLIBRO", "N", p.IdLibro);
            Parametro("PIDPROVEEDOR", "N", p.IdProveedor);
            Parametro("PCANTIDAD", "N", p.Cantidad);
            Parametro("PFCHPEDIDO", "F", p.Fchpedido);
            Parametro("IDSRESERVAS", "S", p.IdsReservas);
            ComandoSP("SP_GUARDARPEDIDODOS");
            AddResultados("PIDPEDIDO");

            LimpiarParametros();
            return listaResultados;
        }
        public Dictionary<int, int> SavePedidos(List<SavePedidoDTO> lp)
        {
            ConectarTransaccion("SP_GUARDARPEDIDO");
            try
            {
                foreach (SavePedidoDTO p in lp)
                {
                    Parametro("PIDPEDIDO", "N", null, "O");
                    Parametro("PIDLIBRO", "N", p.IdLibro);
                    Parametro("PIDPROVEEDOR", "N", p.IdProveedor);
                    Parametro("PCANTIDAD", "N", p.Cantidad);
                    Parametro("PFCHPEDIDO", "F", p.Fchpedido);
                    Parametro("IDSRESERVAS", "S", p.IdsReservas);
                    ComandoSPTransaccion();
                    AddResultadosDiccionario("PIDPEDIDO");
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
        override public int ResultadoSP()
        {
            if (afectadas >= 1)
            {
                afectadas = 0;
                LimpiarErrores();
                return 1;
            }
            else if (HuboErrores)
            {
                afectadas = 0;
                LimpiarErrores();
                return -1;
            }
            else
            {
                afectadas = 0;
                LimpiarErrores();
                return 0;
            }
        }
        override public void AddResultadosDiccionario(string nomParametro)
        {
            int resultado = ResultadoSP();
            int valor = Convert.ToInt32(ValorParametro(nomParametro));
            if (resultado >= 1)//Si es 1 significa que se modificó/guardó 1 fila, si es -1 hubo error y si es 0 no se modificó nada.
            {
                mapaResultados.Add(valor, resultado);
            }
            else if (mapaResultados.Keys.Contains(-1) == false)
            {
                mapaResultados.Add(-1, resultado);

            }
        }
        public List<GetLibrosParaPedirDTO> GetLibrosParaPedir(string titulo, Seriee serie, Autore autor, Editoriales editorial, Materiaa materia, int codigo)
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
            DataTable t = OI().ObtenerDt("SP_GETLIBROSPARAPEDIR");
            OI().LimpiarParametros();
            List<GetLibrosParaPedirDTO> lista = new List<GetLibrosParaPedirDTO>();
            try
            {
                foreach (DataRow dr in t.Rows)
                {
                    GetLibrosParaPedirDTO dto = new GetLibrosParaPedirDTO();
                    dto.IdLibro = Convert.ToInt32(dr["id_libro"]);
                    dto.Codigo = Convert.ToInt32(dr["codigo"]);
                    dto.Titulo = dr["titulo"].ToString();
                    dto.IdAutor = Convert.ToInt32(dr["id_autor"]);
                    dto.Autor = dr["autor"].ToString();
                    dto.IdSerie = Convert.ToInt32(dr["id_serie"]);
                    dto.Serie = dr["serie"].ToString();
                    dto.IdEditorial = Convert.ToInt32(dr["id_editorial"]);
                    dto.Editorial = dr["editorial"].ToString();
                    dto.IdMateria = Convert.ToInt32(dr["id_materia"]);
                    dto.Materia = dr["materia"].ToString();
                    dto.Cantidad = Convert.ToInt32(dr["cantidad"]);
                    dto.IdsReservas = dr["ids_reservas"].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                t.Dispose();
            }
            return lista;
        }
    }
}
