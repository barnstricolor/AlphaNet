using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleAviaoRepositorio : AviaoRepositorio
    {
        private DataTable dt;

        public OracleAviaoRepositorio() { 
            dt = new DataTable("AVIAO");
            dt.Columns.Add(new DataColumn("AVIAO_ID",typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_MODELO", typeof(string)));
            dt.Columns.Add(new DataColumn("QTD_ASSENTO", typeof(int)));
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
        }

        private void insert(Aviao aviao) {

            OracleDataAdapter da = obterAdapter(null);

            DataRow row = dt.NewRow();
            
            aviao._id = obterSequencia();

            preencherEntidade(row, aviao);

            dt.Rows.Add(row);            

            da.Update(dt);

        }
        private void update(Aviao aviao)
        {
            OracleDataAdapter da = obterAdapter(new AviaoId(aviao.aviaoId().Id));

            DataRow row = dt.Rows[0];
            
            preencherEntidade(row, aviao);

            da.Update(dt);

        }

        public void salvar(Aviao aviao)
        {
            if (aviao._id.Equals(0))
                insert(aviao);
            else
                update(aviao);            
        }

        public Aviao obterPeloId(AviaoId aviaoId)
        {
            OracleDataAdapter da = obterAdapter(aviaoId);
            if (dt.Rows.Count == 0)
                return null;

            return modeloPelaEntidade(dt.Rows[0]);
        }

        private OracleDataAdapter obterAdapter(AviaoId aviaoId)
        {            
            string str = "select * from AVIAO";
                        
            if (aviaoId!=null)
                str += " Where AVIAO_ID = " + Bd.aspas(aviaoId.Id);

            dt.Clear();
            
            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);
            
            da.Fill(dt);

            return da;
        }

        public List<Aviao> todosAvioes()
        {
            OracleDataAdapter da = obterAdapter(null);

            List<Aviao> result = new List<Aviao>();

            foreach (DataRow dr in dt.Rows) {
                Aviao aviao = modeloPelaEntidade(dr);
                result.Add(aviao);
            }

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(AviaoId aviaoId)
        {
            OracleDataAdapter da = obterAdapter(aviaoId);

            dt.Rows[0].Delete();

            da.Update(dt);

        }
        public AviaoId proximaIdentidade()
        {
            return new AviaoId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_AVIAO");
        }
        private void preencherEntidade(DataRow entidade, Aviao aviao)
        {
            entidade["AVIAO_ID"] = aviao.aviaoId().Id;
            entidade["NOM_MODELO"] = aviao.modelo();
            entidade["QTD_ASSENTO"] = aviao.assentos();
            entidade["ID"] = aviao._id;
        }
        private Aviao modeloPelaEntidade(DataRow entidade)
        {
            if (entidade == null)
                return null;

            Aviao aviao = new Aviao(new AviaoId(entidade["AVIAO_ID"].ToString()),
                                    entidade["NOM_MODELO"].ToString(),
                                    int.Parse(entidade["QTD_ASSENTO"].ToString()));
            aviao._id = int.Parse(entidade["ID"].ToString());
            return aviao;

        }

    }
}
