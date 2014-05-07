using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using Oracle.ManagedDataAccess.Client;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleCidadeRepositorio : CidadeRepositorio
    {
        private static Bd persistencia = Bd.Instance;
        protected string tabela() { return "CIDADE"; }
        protected string colunaId() { return "ID"; }
        protected string colunaIdExterno() { return "CIDADE_ID"; }
        protected string[] colunas() { return new string[] { colunaId(), colunaIdExterno(), "NOM_CIDADE", "NUM_CEP" }; }

        protected Bd obterPersistencia()
        {
            return persistencia;
        }
        protected OracleConnection obterConexao()
        {
            return this.obterPersistencia().obterConexao();
        }

        public CidadeId proximaIdentidade()
        {
            return new CidadeId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Cidade cidade)
        {
            if (cidade._id.Equals(0))
            {
                insert(cidade);
            }
            else
            {
                update(cidade);
            }

        }
        private void insert(Cidade cidade)
        {
            cidade._id = obterSequencia();

            insertCommand(cidade);

        }
        private void update(Cidade cidade)
        {
            updateCommand(cidade, cidade._id);

        }
        protected void updateCommand(Cidade dominio, int id)
        {
            executarCommand(montarUpdateById(id), dominio);
        }
        protected string montarUpdateById(int id)
        {
            string[] strAux = new string[colunas().Length];

            for (int i = 0; i < colunas().Length; i++)
            {
                strAux[i] = colunas()[i] + " = :" + colunas()[i].ToLower();
            }

            string str = "Update " + tabela() + " Set " + string.Join(",", strAux);
            str += " Where " + colunaId() + " = " + id;

            return str;
        }

        private int obterSequencia()
        {
            return obterPersistencia().obterSequencia("SQ_CIDADE");
        }
        protected void insertCommand(Cidade cidade)
        {
            executarCommand(montarInsert(), cidade);
        }
        protected string montarInsert()
        {
            string[] strAux = new string[colunas().Length];

            for (int i = 0; i < colunas().Length; i++)
            {
                strAux[i] = " :" + colunas()[i].ToLower();
            }

            string str = "Insert Into " + tabela() + "(" + string.Join(",", colunas()) + ")";
            str += " Values (" + string.Join(",", strAux) + ")";

            return str;
        }
        private Dictionary<string, object> criarDictionary()
        {
            return new Dictionary<string, object>();
        }

        protected void executarCommand(string cmdText, Cidade dominio)
        {
            Dictionary<string, object> d = criarDictionary();
            valuesMap(d, dominio);
            montarCommand(cmdText, d).ExecuteNonQuery();
        }
        protected OracleCommand montarCommand(string cmdText, Dictionary<string, object> d)
        {
            OracleCommand cmd = new OracleCommand(cmdText, obterConexao());
            foreach (var value in d)
                cmd.Parameters.Add(":" + value.Key, value.Value);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = cmdText;
            return cmd;
        }

        public Cidade obterPeloId(CidadeId aviaoId)
        {
            OracleDataReader dr = executeQueryByIdExterno(aviaoId.Id);

            if (!dr.HasRows) return null;

            dr.Read();

            return mapRow(dr);
        }
        protected Cidade mapRow(OracleDataReader dr)
        {
            Cidade cidade = new Cidade(
                new CidadeId(dr["CIDADE_ID"].ToString()),
                    dr["NOM_CIDADE"].ToString(),
                    dr["NUM_CEP"].ToString());
            cidade._id = int.Parse(dr["ID"].ToString());
            return cidade; 
        }

        public void limpar()
        {
        }
        public List<Cidade> todasCidades() {
            
            OracleDataReader dr = executeQuery(montarSelect());

            List<Cidade> lista = new List<Cidade>();

            while (dr.Read())
            {
                Cidade cidade = new Cidade(
                    new CidadeId(dr["CIDADE_ID"].ToString()),
                        dr["NOM_CIDADE"].ToString(),
                        dr["NUM_CEP"].ToString());
                cidade._id = int.Parse(dr["ID"].ToString());

                lista.Add(cidade);
            }
            dr.Close();
            return lista;
        }

        public void excluir(CidadeId cidadeId) {
            string sql;

            OracleConnection cnn = obterConexao();

            sql = "Delete " + tabela() + " Where " + colunaIdExterno() + " = :" + colunaIdExterno().ToLower();

            OracleCommand cmd = new OracleCommand(sql, cnn);

            cmd.Parameters.Add(":" + colunaIdExterno(), cidadeId.Id);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();

        }
        protected OracleDataReader executeQuery(string str)
        {
            OracleDataReader dr = this.obterPersistencia().obterQuery(str);
            return dr;
        }
        protected string montarSelect(string where)
        {
            string str = montarSelect();

            if (!string.IsNullOrEmpty(where))
                str += " Where " + where;
            return str;
        }
        protected string montarSelect()
        {
            return "Select * from " + tabela();
        }
        protected OracleDataReader executeQueryByIdExterno(string id)
        {
            return executeQuery(montarSelectWhereIdExterno(id));
        }
        protected string montarSelectWhereIdExterno(string id)
        {
            return montarSelect(colunaIdExterno() + " = " + Bd.aspas(id));
        }
        protected void valuesMap(Dictionary<string, object> d, Cidade dominio)
        {
            Cidade cidade = dominio;
            d.Add("ID", cidade._id);
            d.Add("CIDADE_ID", cidade.cidadeId().Id);
            d.Add("NOM_CIDADE", cidade.nome());
            d.Add("NUM_CEP", cidade.cep());

        }
        public object[] extrairValores(Cidade dominio)
        {
            Cidade cidade = dominio;
            return new object[] { cidade.cidadeId(), cidade.nome(), cidade.cep()};
        }
        public string montarWhereByFiltroString(string filtro)
        {
            return "NOM_CIDADE LIKE '%" + filtro + "%'";
        }

    }
}
