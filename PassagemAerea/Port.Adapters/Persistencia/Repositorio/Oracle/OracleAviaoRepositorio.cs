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

            OracleDataAdapter da = new OracleDataAdapter("select * from AVIAO", Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            DataRow row = dt.NewRow();
            
            aviao._id = obterSequencia();

            row["AVIAO_ID"] = aviao.aviaoId().Id;
            row["NOM_MODELO"] = aviao.modelo();
            row["QTD_ASSENTO"] = aviao.assentos();
            row["ID"] = aviao._id;
            
            da.Fill(dt);
            dt.Rows.Add(row);            
            da.Update(dt);

        }
        public void salvar(Aviao aviao)
        {
            if (aviao._id.Equals(0))
                insert(aviao);
            
        }

        public Aviao obterPeloId(AviaoId aviaoId)
        {
            throw new NotImplementedException();
        }

        public List<Aviao> todosAvioes()
        {
            return new List<Aviao>();
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(AviaoId aviaoId)
        {
            throw new NotImplementedException();
        }
        public AviaoId proximaIdentidade()
        {
            return new AviaoId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_AVIAO");
        }

    }
}
