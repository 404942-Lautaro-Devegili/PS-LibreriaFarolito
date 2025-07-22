using System.Data;
using System.Data.SqlClient;
namespace LibreriaBack.Datos
{
    public class HelperDao
    {
        private static HelperDao i;
        private SqlConnection conexion;
        public SqlCommand comando;
        private SqlDataAdapter adapter;
        public bool HuboErrores;
        private int ErrorCodigo;
        private string ErrorMensaje;
        public int afectadas;
        public List<int> listaResultados;
        public Dictionary<int, int> mapaResultados;


        public HelperDao()
        {
            conexion = new SqlConnection(Properties.Resources.dbCasa);
            comando = new SqlCommand();
            comando.Connection = conexion;
            adapter = new SqlDataAdapter(comando);
            HuboErrores = false;
            ErrorCodigo = 0;
            ErrorMensaje = "";
            afectadas = 0;
            listaResultados = new List<int>();
            mapaResultados = new Dictionary<int, int>();
        }

        public static HelperDao OI()
        {
            if (i == null)
            {
                i = new HelperDao();
            }
            return i;
        }

        public void ErrorDef(SqlException excSql)
        {
            HuboErrores = true;
            ErrorCodigo = excSql.Number;
            ErrorMensaje = excSql.Message;
        }

        public void LimpiarErrores()
        {
            HuboErrores = false;
            ErrorCodigo = 0;
            ErrorMensaje = "";
        }

        public void ClearResultados()
        {
            listaResultados.Clear();
        }

        public void AddResultados(string nomParametro)
        {
            listaResultados.Add(ResultadoSP());
            if (listaResultados[0] == 1) //Si es 1 significa que se modificó/guardó 1 fila/se logueó exitosamente, si es -1 hubo error y si es 0 no se modificó nada/las credenciales estaban incorrectas.
            {
                listaResultados.Add(int.Parse(ValorParametro(nomParametro).ToString()));
            }
            else
            {
                listaResultados.Add(-1);
            }
        }

        public virtual void AddResultadosDiccionario(string nomParametro)
        {
            int resultado = ResultadoSP();
            int valor = Convert.ToInt32(ValorParametro(nomParametro));
            if (resultado == 1)//Si es 1 significa que se modificó/guardó 1 fila, si es -1 hubo error y si es 0 no se modificó nada.
            {
                mapaResultados.Add(valor, resultado);
            }
            else if (mapaResultados.Keys.Contains(-1) == false)
            {
                mapaResultados.Add(-1, resultado);

            }
        }

        public void Conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
        }

        public void ConectarTransaccion(string nombreSP)
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandText = nombreSP;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Transaction = conexion.BeginTransaction();
        }

        public void Desconectar()
        {
            conexion.Close();
        }
        public void Commit()
        {
            comando.Transaction.Commit();
        }

        public void Rollback()
        {
            comando.Transaction.Rollback();
        }

        public int ConsultarEscalar(string nombreSP, string paramOut)
        {
            Conectar();
            comando.Parameters.Clear();
            comando.CommandText = nombreSP;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = paramOut;
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;
            comando.Parameters.Add(parametro);
            comando.ExecuteNonQuery();
            Desconectar();
            comando.Parameters.Clear();
            return (int)parametro.Value;
        }

        public int ConsultarEscalar(string nombreSP, string paramOut, int IdFuncion)
        {
            Conectar();
            comando.Parameters.Clear();
            comando.CommandText = nombreSP;
            SqlParameter parametro = new SqlParameter();

            parametro.ParameterName = paramOut;
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;
            comando.Parameters.AddWithValue("@funcion", IdFuncion);
            comando.Parameters.Add(parametro);
            comando.ExecuteNonQuery();
            Desconectar();
            comando.Parameters.Clear();
            return (int)parametro.Value;
        }

        public DataTable CargarProductos()
        {
            Conectar();
            comando.CommandText = "SP_CONSULTAR_PRODUCTOS";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            return tabla;
        }

        public DataTable Consultar(string nombreSP)
        {
            Conectar();
            comando.Parameters.Clear();
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            comando.Parameters.Clear();
            return tabla;
        }

        public DataTable Consultar(string nombreSP, Parametro p)
        {
            Conectar();
            comando.Parameters.Clear();
            comando.CommandText = nombreSP;
            comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            comando.Parameters.Clear();
            return tabla;
        }

        public DataTable Consultar(string nombreSP, List<Parametro> parametros)
        {
            Conectar();
            comando.Parameters.Clear();
            comando.CommandText = nombreSP;
            foreach (Parametro p in parametros)
            {
                comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            }
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            comando.Parameters.Clear();
            return tabla;
        }

        public SqlParameter pSalida()
        {
            SqlParameter p = new SqlParameter();
            p.Direction = ParameterDirection.Output;
            return p;
        }

        public int EjecutarSQL(string strSql, List<Parametro> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                conexion.Open();
                t = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (Parametro param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) { t.Rollback(); }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();

            }
            comando.Parameters.Clear();

            return afectadas;
        }
        public bool BorrarFuncion(int nro)
        {
            string sp = "DELETEFunciones";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@codFuncion", nro));
            int afectadas = EjecutarSQL(sp, lst);
            comando.Parameters.Clear();
            return afectadas > 0;
        }

        public void Parametro(string nombre,
                              string sTipo,
                              object valor = null,
                              string sDireccion = null,
                              int tamano = 0)
        {
            SqlDbType tipo;

            switch (sTipo)
            {
                case "N":
                    tipo = SqlDbType.Int;
                    break;
                case "S":
                    tipo = SqlDbType.VarChar;
                    break;
                case "S2":
                    tipo = SqlDbType.VarChar;
                    break;
                case "L":
                    tipo = SqlDbType.Char;
                    break;
                case "F":
                    tipo = SqlDbType.DateTime;
                    break;
                case "B":
                    tipo = SqlDbType.Bit;
                    break;
                case "D":
                    tipo = SqlDbType.Decimal;
                    break;
                case "C":
                    tipo = SqlDbType.VarChar;
                    var paramCursor = new SqlParameter(nombre, tipo)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramCursor);
                    return;
                default:
                    tipo = SqlDbType.DateTime;
                    break;
            }

            var parametro = new SqlParameter(nombre, tipo);

            if (string.IsNullOrEmpty(sDireccion) || sDireccion != "O")
            {
                if (sTipo == "F" && valor is DateTime dateTimeValue && dateTimeValue == DateTime.MinValue)
                {
                    valor = DBNull.Value;
                }
                parametro.Value = valor ?? DBNull.Value;
            }

            parametro.Direction = sDireccion switch
            {
                "O" => ParameterDirection.Output,
                "IO" => ParameterDirection.InputOutput,
                _ => ParameterDirection.Input
            };

            if (sTipo == "S" && (tamano < 1 || tamano == 0))
            {
                parametro.Size = 40000;
            }
            else if (sTipo == "S" && tamano > 0)
            {
                parametro.Size = tamano;
            }

            if (sTipo == "S2")
            {
                parametro.Size = 2000;
            }

            comando.Parameters.Add(parametro);
        }

        public void ComandoSP(string nombreSP)
        {
            comando.CommandText = nombreSP;
            comando.CommandType = CommandType.StoredProcedure;

            try
            {
                Conectar();
                afectadas = comando.ExecuteNonQuery();
            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
            }
            finally
            {
                Desconectar();
            }
        }

        public void ComandoSPTransaccion()
        {
            try
            {
                afectadas = comando.ExecuteNonQuery();
            }
            catch (SqlException excSql)
            {
                ErrorDef(excSql);
            }

        }

        public object ValorParametro(string nombre)
        {
            return comando.Parameters[nombre].Value;
        }

        public void LimpiarParametros()
        {
            comando.Parameters.Clear();
        }

        public int Afectadas()
        {
            int rowsAfectadas = afectadas;
            afectadas = 0;
            return rowsAfectadas;
        }

        public DataTable ObtenerDt(string spNombre)
        {
            DataTable dt = new DataTable();

            try
            {
                Conectar();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = spNombre;

                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return dt;
        }

        public virtual int ResultadoSP()
        {
            if (afectadas == 1)
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

    }
}
