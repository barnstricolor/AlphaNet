using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class Bd
    {
        private OracleConnection conn;
        private static Bd instance;

        private Bd()
        {
            string strConexao = "DATA SOURCE=XE;PASSWORD=PI;USER ID=PI";
            this.conn = new OracleConnection();
            this.conn.ConnectionString = strConexao;
            try
            {
                this.conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static Bd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bd();
                }
                return instance;
            }
        }
 
        public OracleConnection obterConexao()
        {
            return this.conn;
        }
        
        public OracleDataReader obterQuery(string strSelect)
        {
            OracleCommand cmd = new OracleCommand(strSelect, this.conn);

            OracleDataReader dr = cmd.ExecuteReader();
            cmd.Dispose();
            return dr;
        }

        public static string aspas(string str)
        {
            return (char)39 + str + (char)39;
        }

        public void executaComando(string sql)
        {
            try
            {
                OracleConnection cnn = this.obterConexao();

                OracleCommand cmd = cnn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                throw e;
            }

        }
        
        public int obterSequencia(string sequence)
        {

            OracleCommand cmd = new OracleCommand("Select " + sequence + ".nextval from dual", obterConexao());

            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string valor = dr[0].ToString();
            dr.Close();
            return Int32.Parse(valor);
        }

        public OracleDataReader executeQuery(string str)
        {
            OracleDataReader dr = this.obterQuery(str);
            return dr;
        }

    }
}
