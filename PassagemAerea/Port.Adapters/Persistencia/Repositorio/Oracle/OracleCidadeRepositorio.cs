using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AlphaNet.PassagemAerea.Domain.Model.Voos;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleCidadeRepositorio : CidadeRepositorio
    {
        private DataTable dt;

        public OracleCidadeRepositorio() { 
            dt = new DataTable("CIDADE");
            dt.Columns.Add(new DataColumn("CIDADE_ID",typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_CIDADE", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_CEP", typeof(int)));
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
        }

        private void insert(Cidade cidade) {

            OracleDataAdapter da = obterAdapter(null);

            DataRow row = dt.NewRow();
            
            cidade._id = obterSequencia();

            preencherEntidade(row, cidade);

            dt.Rows.Add(row);            

            da.Update(dt);

        }
        private void update(Cidade aviao)
        {
            OracleDataAdapter da = obterAdapter(new CidadeId(aviao.cidadeId().Id));

            DataRow row = dt.Rows[0];
            
            preencherEntidade(row, aviao);

            da.Update(dt);

        }

        public void salvar(Cidade cidade)
        {
            if (cidade._id.Equals(0))
                insert(cidade);
            else
                update(cidade);            
        }

        public Cidade obterPeloId(CidadeId aviaoId)
        {
            OracleDataAdapter da = obterAdapter(aviaoId);

            if (dt.Rows.Count == 0)
                return null;

            return modeloPelaEntidade(dt.Rows[0]);
        }

        private OracleDataAdapter obterAdapter(CidadeId cidadeId)
        {            
            string str = "select * from CIDADE";
                        
            if (cidadeId!=null)
                str += " Where CIDADE_ID = " + Bd.aspas(cidadeId.Id);

            dt.Clear();
            
            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);
            
            da.Fill(dt);

            return da;
        }

        public List<Cidade> todasCidades()
        {
            OracleDataAdapter da = obterAdapter(null);

            List<Cidade> result = new List<Cidade>();

            foreach (DataRow dr in dt.Rows) {
                Cidade cidade = modeloPelaEntidade(dr);
                result.Add(cidade);
            }

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(CidadeId cidadeId)
        {
            OracleDataAdapter da = obterAdapter(cidadeId);

            dt.Rows[0].Delete();

            da.Update(dt);

        }
        public CidadeId proximaIdentidade()
        {
            return new CidadeId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_CIDADE");
        }
        private void preencherEntidade(DataRow entidade, Cidade cidade)
        {
            entidade["CIDADE_ID"] = cidade.cidadeId().Id;
            entidade["NOM_CIDADE"] = cidade.nome();
            entidade["NUM_CEP"] = cidade.cep();
            entidade["ID"] = cidade._id;
        }
        private Cidade modeloPelaEntidade(DataRow entidade)
        {
            Cidade cidade = new Cidade(new CidadeId(entidade["CIDADE_ID"].ToString()),
                                    entidade["NOM_CIDADE"].ToString(),
                                    entidade["NUM_CEP"].ToString());
            cidade._id = int.Parse(entidade["ID"].ToString());
            return cidade;

        }


    }
}
