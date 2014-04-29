using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using Oracle.ManagedDataAccess.Client;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleAviaoRepositorio : AviaoRepositorio
    {
        private static Bd persistencia = Bd.Instance;
        protected string tabela() { return "AVIAO"; }
        protected string colunaId() { return "ID"; }
        protected string colunaIdExterno() { return "AVIAO_ID"; }
        protected string[] colunas() { return new string[] { colunaId(), colunaIdExterno(), "NOM_MODELO", "QTD_ASSENTO" }; }

        protected Bd obterPersistencia()
        {
            return persistencia;
        }
        protected OracleConnection obterConexao()
        {
            return this.obterPersistencia().obterConexao();
        }

        public AviaoId proximaIdentidade()
        {
            return new AviaoId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Aviao aviao)
        {
            if (aviao._id.Equals(0))
            {
                insert(aviao);
            }
            else
            {
                update(aviao);
            }

        }
        private void insert(Aviao aviao)
        {
            aviao._id = obterSequencia();

            insertCommand(aviao);

        }
        private void update(Aviao aviao)
        {
            updateCommand(aviao, aviao._id);

        }
        protected void updateCommand(Aviao dominio, int id)
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
            return obterPersistencia().obterSequencia("SQ_AVIAO");
        }
        protected void insertCommand(Aviao aviao)
        {
            executarCommand(montarInsert(), aviao);
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

        protected void executarCommand(string cmdText, Aviao dominio)
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

        public Aviao obterPeloId(AviaoId aviaoId)
        {
            OracleDataReader dr = executeQueryByIdExterno(aviaoId.Id);

            if (!dr.HasRows) return null;

            dr.Read();

            return mapRow(dr);
        }
        protected Aviao mapRow(OracleDataReader dr)
        {
            Aviao aviao = new Aviao(
                new AviaoId(dr["AVIAO_ID"].ToString()),
                    dr["NOM_MODELO"].ToString(),
                    int.Parse(dr["QTD_ASSENTO"].ToString()));
            aviao._id = int.Parse(dr["ID"].ToString());
            return aviao; 
        }

        public void limpar()
        {
        }
        public List<Aviao> todosAvioes() {
            
            OracleDataReader dr = executeQuery(montarSelect());

            List<Aviao> lista = new List<Aviao>();

            while (dr.Read())
            {
                Aviao aviao = new Aviao(
                    new AviaoId(dr["AVIAO_ID"].ToString()),
                        dr["NOM_MODELO"].ToString(),
                        int.Parse(dr["QTD_ASSENTO"].ToString()));
                aviao._id = int.Parse(dr["ID"].ToString());

                lista.Add(aviao);
            }
            dr.Close();
            return lista;
        }

        public void excluir(AviaoId aviaoId) {
            string sql;

            OracleConnection cnn = obterConexao();

            sql = "Delete " + tabela() + " Where " + colunaIdExterno() + " = :" + colunaIdExterno().ToLower();

            OracleCommand cmd = new OracleCommand(sql, cnn);

            cmd.Parameters.Add(":" + colunaIdExterno(), aviaoId.Id);
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
        protected void valuesMap(Dictionary<string, object> d, Aviao dominio)
        {
            Aviao aviao = dominio;
            d.Add("ID", aviao._id);
            d.Add("AVIAO_ID", aviao.aviaoId().Id);
            d.Add("NOM_MODELO", aviao.modelo());
            d.Add("QTD_ASSENTO", aviao.assentos());

        }
        public object[] extrairValores(Aviao dominio)
        {
            Aviao aviao = dominio;
            return new object[] { aviao.aviaoId(), aviao.modelo(), aviao.assentos()};
        }
        public string montarWhereByFiltroString(string filtro)
        {
            return "NOM_MODELO LIKE '%" + filtro + "%'";
        }

    }
}
