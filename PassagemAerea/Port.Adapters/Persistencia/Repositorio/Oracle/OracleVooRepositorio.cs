using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Comando;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleVooRepositorio : VooRepositorio
    {
        private DataTable dt;
        private DataTable dtReservas;

        public OracleVooRepositorio()
        { 
            dt = new DataTable("VOO");
            dt.Columns.Add(new DataColumn("VOO_ID",typeof(string)));
            dt.Columns.Add(new DataColumn("DAT_PARTIDA", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DAT_CHEGADA", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ID_AVIAO", typeof(string)));
            dt.Columns.Add(new DataColumn("VAL_PRECO", typeof(double)));
            dt.Columns.Add(new DataColumn("ID_ORIGEM", typeof(string)));
            dt.Columns.Add(new DataColumn("ID_DESTINO", typeof(string)));
            dt.Columns.Add(new DataColumn("FLG_PROMOCAO", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(int)));

            dtReservas = new DataTable("RESERVA");
            dtReservas.Columns.Add(new DataColumn("ID", typeof(int)));
            dtReservas.Columns.Add(new DataColumn("ID_VOO", typeof(int)));
            dtReservas.Columns.Add(new DataColumn("VAL_PRECO", typeof(double)));
            dtReservas.Columns.Add(new DataColumn("QTD_ASSENTO", typeof(string)));
            dtReservas.Columns.Add(new DataColumn("ID_CLIENTE", typeof(string)));
        }

        public void cancelarReservaCliente(Voo voo, Cliente cliente)
        {
            string str = "select * from RESERVA Where ID_VOO = " + voo._id + " And ID_CLIENTE = " + Bd.aspas(cliente.clienteId().Id);

            dtReservas.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dtReservas);

            foreach (DataRow dr in dtReservas.Rows)
            {
                dr.Delete();
            }

            da.Update(dtReservas);
        }

        public void salvarReservas(Voo voo) {

            string str = "select * from RESERVA Where ID_VOO = " + voo._id;

            dtReservas.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dtReservas);

            foreach (Reserva reserva in voo.reservas()) 
            {
                if (reserva._id.Equals(0)){
                    
                    DataRow row = dtReservas.NewRow();

                    string mapa = "";
                    foreach (Assento assento in reserva.assentos())
                        mapa += assento.assento() + " ";

                    row["ID"] = obterSequenciaReserva();
                    reserva._id = (int)row["ID"];
                    row["ID_VOO"] = voo._id;
                    row["VAL_PRECO"] = voo.preco();
                    row["QTD_ASSENTO"] = mapa;
                    row["ID_CLIENTE"] = reserva.clienteId().Id;

                    dtReservas.Rows.Add(row);
                }
            }

            da.Update(dtReservas);            
        }
        public void carregarReservas(Voo voo)
        {
            string str = "select * from RESERVA Where ID_VOO = " + voo._id;

            dtReservas.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dtReservas);            

            foreach (DataRow row in dtReservas.Rows)
            {
                Cliente cliente = DominioRegistro.clienteRepositorio().obterPeloId(new ClienteId((string)row["ID_CLIENTE"]));

                string a = ((string)row["QTD_ASSENTO"]).Trim();
                string[] strAux = a.Split(' ');

                List<Assento> assentos = new List<Assento>();

                foreach (string item in strAux)
                    assentos.Add(new Assento(int.Parse(item)));

                voo.adicionarReserva(cliente, assentos.ToArray());
            }
        }
        private void insert(Voo voo)
        {

            OracleDataAdapter da = obterAdapter(null);

            DataRow row = dt.NewRow();
            
            voo._id = obterSequencia();

            preencherEntidade(row, voo);

            dt.Rows.Add(row);            

            da.Update(dt);

        }
        private void update(Voo voo)
        {
            OracleDataAdapter da = obterAdapter(new VooId(voo.aviaoId().Id));

            DataRow row = dt.Rows[0];
            
            preencherEntidade(row, voo);

            da.Update(dt);

        }

        public void salvar(Voo voo)
        {
            insert(voo);
        }

        public Voo obterPeloId(VooId vooId)
        {
            OracleDataAdapter da = obterAdapter(vooId);

            if (dt.Rows.Count == 0)
                return null;

            return modeloPelaEntidade(dt.Rows[0]);
        }

        private OracleDataAdapter obterAdapter(VooId aviaoId)
        {            
            string str = "select * from VOO";

            if (aviaoId != null)
                str += " Where VOO_ID = " + Bd.aspas(aviaoId.Id);
            else
                str += " Where VOO_ID = '-1'";

            dt.Clear();
            
            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);
            
            da.Fill(dt);

            return da;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(VooId vooId)
        {
            OracleDataAdapter da = obterAdapter(vooId);

            dt.Rows[0].Delete();

            da.Update(dt);

        }
        public VooId proximaIdentidade()
        {
            return new VooId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_VOO");
        }
        private int obterSequenciaReserva()
        {
            return Bd.Instance.obterSequencia("SQ_RESERVA");
        }
        private void preencherEntidade(DataRow entidade, Voo voo)
        {
            entidade["VOO_ID"] = voo.aviaoId().Id;
            entidade["DAT_PARTIDA"] = voo.partida();
            entidade["DAT_CHEGADA"] = voo.chegada();
            entidade["ID_AVIAO"] = voo.aviaoId().Id;
            entidade["VAL_PRECO"] = voo.preco();
            entidade["ID_ORIGEM"] = voo.origemId().Id;
            entidade["ID_DESTINO"] = voo.destinoId().Id;
            entidade["FLG_PROMOCAO"] = voo.promocional();
            entidade["ID"] = voo._id;
        }
        private Voo modeloPelaEntidade(DataRow entidade)
        {
            Aviao aviao = DominioRegistro.aviaoRepositorio().obterPeloId(new AviaoId(entidade["ID_AVIAO"].ToString()));
            Cidade origem = DominioRegistro.cidadeRepositorio().obterPeloId(new CidadeId(entidade["ID_ORIGEM"].ToString()));
            Cidade destino = DominioRegistro.cidadeRepositorio().obterPeloId(new CidadeId(entidade["ID_DESTINO"].ToString()));

            Voo voo = new Voo(new VooId(entidade["VOO_ID"].ToString()),
                                    aviao,
                                    origem,
                                    destino,
                                    DateTime.Parse((entidade["DAT_PARTIDA"]).ToString()),
                                    double.Parse((entidade["VAL_PRECO"].ToString())));

            voo._id = int.Parse(entidade["ID"].ToString());

            carregarReservas(voo);

            return voo;

        }
        public List<Voo> todosVoos()
        {
            DataTable dtTodos = dt;
            
            string str = "select * from VOO";

            dtTodos.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dtTodos);

            List<Voo> result = new List<Voo>();

            foreach (DataRow dr in dtTodos.Rows)
            {
                Voo voo  = modeloPelaEntidade(dr);
                result.Add(voo);
            }

            return result;
        }

        public List<Voo> voosCliente(ClienteId clienteId)
        {
            List<Voo> result = new List<Voo>();

            foreach (Voo voo in DominioRegistro.vooRepositorio().todosVoos())
            {
                if (voo.temReservaParaCliente(clienteId))
                    result.Add(voo);
            }

            return result;
        }


    }
}
