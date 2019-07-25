using jganchozo.salaespera.entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jganchozo.salaespera.data
{
    public class BaseDatos
    {
        public SqlConnection conexion = null;

        private string cadena = "";
        public readonly SqlDataAdapter loDataAdapter = new SqlDataAdapter();
        DataSet loDataSetResultado = new DataSet();

        public BaseDatos()
        {
            cadena = ConfigurationManager.ConnectionStrings["DB_SALAESPERA"].ConnectionString;
            conexion = new SqlConnection(cadena);
        }

        public void Conectar()
        {
            if (conexion.State == ConnectionState.Closed || conexion == null)
            {
                conexion.Open();
            }
        }
        public void Desconectar()
        {
            if (conexion.State == ConnectionState.Open || conexion != null)
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Método para actualizar datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="proceducimiento"></param>
        /// <returns></returns>
        public string ActualizarDatos(SqlParameter[] parametrosProcedure, string nombreProcedure)
        {
            SqlDataReader reader;
            string mensaje = "";
            try
            {
                SqlCommand cmdConsulta = this.oSQLGeneraComando(nombreProcedure.Trim(), parametrosProcedure);
                reader = cmdConsulta.ExecuteReader();
                while (reader.Read())
                {
                    mensaje = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //registroLogs_Common.GuardarLog(ex.Message);
                return "";

            }
            finally
            {
                this.Desconectar();
            }
            return mensaje;
        }

        /// <summary>
        /// Método para generar comando sql
        /// </summary>
        /// <param name="procedimiento"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        protected SqlCommand oSQLGeneraComando(string psProcedimiento, SqlParameter[] poSQLParametros)
        {
            SqlCommand oSQLComando = new SqlCommand(psProcedimiento, this.conexion);
            try
            {

                oSQLComando.Parameters.AddRange(poSQLParametros);
                oSQLComando.CommandType = CommandType.StoredProcedure;
                Conectar();

            }
            catch (Exception ex)
            {
                //registroLogs_Common.GuardarLog(ex.Message);
            }
            return oSQLComando;
        }


        /// <summary>
        /// Método para insertar datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="procedimiento"></param>
        /// <returns></returns>
        public List<PersonaColaEntity> InsertarDatos(SqlParameter[] parametrosProcedure, string nombreProcedure)
        {
            List<PersonaColaEntity> lista = new List<PersonaColaEntity>();
            SqlDataReader reader;
            try
            {
                SqlCommand comando = this.oSQLGeneraComando(nombreProcedure, parametrosProcedure);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    int numeroAtencion = 0;
                    PersonaColaEntity listaPersona = new PersonaColaEntity();
                    numeroAtencion = reader.GetInt32(0);
                    listaPersona.NumeroCola = reader.GetInt32(1);
                    listaPersona.NumeroAtencion = numeroAtencion.ToString().PadLeft(4, '0');

                    lista.Add(listaPersona);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
                //registroLogs_Common.GuardarLog(ex.Message);
            }
            finally
            {
                this.Desconectar();
            }
            return lista;
        }

        /// <summary>
        /// Método para ejecutar procedimiento 
        /// </summary>
        /// <param name="procedimiento"></param>
        /// <param name="parametros"></param>
        /// <returns> retorna un dataset</returns>
        public DataSet dsEjecutarProcedimiento(string psProcedimiento, SqlParameter[] poSQLParametros)
        {
            loDataSetResultado = new DataSet();
            try
            {

                SqlCommand oSQLComando = this.oSQLGeneraComando(psProcedimiento, poSQLParametros);
                loDataAdapter.SelectCommand = oSQLComando;
                loDataAdapter.SelectCommand.Connection = this.conexion;
                loDataAdapter.SelectCommand.CommandTimeout = 18000;
                loDataAdapter.Fill(loDataSetResultado);

            }
            catch (SystemException ex)
            {
                throw new Exception();
            }
            finally
            {
                Desconectar();
            }
            return loDataSetResultado;
        }
    }
}
