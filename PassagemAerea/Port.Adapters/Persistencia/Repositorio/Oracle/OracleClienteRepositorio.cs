using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleClienteRepositorio : ClienteRepositorio
    {
        private DataTable dt;

        public OracleClienteRepositorio() { 
            dt = new DataTable("CLIENTE");
            dt.Columns.Add(new DataColumn("CLIENTE_ID",typeof(string)));
            dt.Columns.Add(new DataColumn("FLG_PROMOCAO", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_CLIENTE", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_EMAIL", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_ENDERECO", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_CPF", typeof(string)));
            dt.Columns.Add(new DataColumn("ID_CIDADE", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_CELULAR", typeof(string)));
            dt.Columns.Add(new DataColumn("VAL_RENDA", typeof(double)));
            dt.Columns.Add(new DataColumn("NOM_OCUPACAO", typeof(string)));
            dt.Columns.Add(new DataColumn("FLG_ESPECIAL", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_RG", typeof(string)));
            dt.Columns.Add(new DataColumn("SEXO", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_END", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_BAIRRO", typeof(string)));
            dt.Columns.Add(new DataColumn("CAD_CEP", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_TELEFONE", typeof(string)));
            dt.Columns.Add(new DataColumn("PER_DESCONTO", typeof(double))); 
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
        }

        private void insert(Cliente cliente) {

            OracleDataAdapter da = obterAdapter(null);

            DataRow row = dt.NewRow();
            
            cliente._id = obterSequencia();

            preencherEntidade(row, cliente);

            dt.Rows.Add(row);            

            da.Update(dt);

        }
        private void update(Cliente aviao)
        {
            OracleDataAdapter da = obterAdapter(new ClienteId(aviao.clienteId().Id));

            DataRow row = dt.Rows[0];
            
            preencherEntidade(row, aviao);

            da.Update(dt);

        }

        public void salvar(Cliente cliente)
        {
            if (cliente._id.Equals(0))
                insert(cliente);
            else
                update(cliente);            
        }

        public Cliente obterPeloId(ClienteId clienteId)
        {
            OracleDataAdapter da = obterAdapter(clienteId);

            return modeloPelaEntidade(dt.Rows[0]);
        }

        private OracleDataAdapter obterAdapter(ClienteId clienteId)
        {            
            string str = "select * from CLIENTE";
                        
            if (clienteId!=null)
                str += " Where CLIENTE_ID = " + Bd.aspas(clienteId.Id);

            dt.Clear();
            
            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);
            
            da.Fill(dt);

            return da;
        }

        public List<Cliente> todosClientes()
        {
            OracleDataAdapter da = obterAdapter(null);

            List<Cliente> result = new List<Cliente>();

            foreach (DataRow dr in dt.Rows) {
                Cliente cliente = modeloPelaEntidade(dr);
                result.Add(cliente);
            }

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(ClienteId clienteId)
        {
            OracleDataAdapter da = obterAdapter(clienteId);

            dt.Rows[0].Delete();

            da.Update(dt);

        }
        public ClienteId proximaIdentidade()
        {
            return new ClienteId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_CLIENTE");
        }
        private void preencherEntidade(DataRow entidade, Cliente cliente)
        {
            entidade["CLIENTE_ID"] = cliente.clienteId().Id;
            entidade["FLG_PROMOCAO"] = cliente.promocao();
            entidade["NOM_CLIENTE"] = cliente.nome();
            entidade["NOM_EMAIL"] = cliente.email();
            entidade["NOM_ENDERECO"] = cliente.endereco();
            entidade["NUM_CPF"] = cliente.cpf();
            entidade["ID_CIDADE"] = cliente.cidade().Id;
            entidade["NUM_CELULAR"] = cliente.celular();
            entidade["VAL_RENDA"] = cliente.renda();
            entidade["NOM_OCUPACAO"] = cliente.ocupacao();
            entidade["FLG_ESPECIAL"] = cliente.especial();
            entidade["NUM_RG"] = cliente.rg();
            entidade["SEXO"] = cliente.sexo();
            entidade["NUM_END"] = cliente.endereco();
            entidade["NOM_BAIRRO"] = cliente.bairro();
            entidade["CAD_CEP"] = cliente.cep();
            entidade["NUM_TELEFONE"] = cliente.telefone();
            entidade["PER_DESCONTO"] = cliente.desconto();
            entidade["ID"] = cliente._id;
        }
        private Cliente modeloPelaEntidade(DataRow entidade)
        {
            Cliente cliente = new Cliente(
                new ClienteId(entidade["CLIENTE_ID"].ToString()), 
                entidade["NOM_CLIENTE"].ToString(), 
                entidade["NOM_EMAIL"].ToString());
            
            cliente.alterarPromocao(bool.Parse(entidade["FLG_PROMOCAO"].ToString()));
            if (entidade["NOM_ENDERECO"] != null)
                cliente.alterarEndereco(entidade["NOM_ENDERECO"].ToString());
            if (entidade["NUM_CPF"] != null)
                cliente.alterarCpf(new CPF(entidade["NUM_CPF"].ToString()));
            if (entidade["ID_CIDADE"] != null)
                cliente.alterarCidade(new CidadeId(entidade["ID_CIDADE"].ToString()));
            if (entidade["NUM_CELULAR"] != null)
                cliente.alterarCelular(entidade["NUM_CELULAR"].ToString());
            if (entidade["VAL_RENDA"] != null)
                cliente.alterarRenda(double.Parse(entidade["VAL_RENDA"].ToString()));
            if (entidade["NOM_OCUPACAO"] != null)
                cliente.alterarOcupacao(entidade["NOM_OCUPACAO"].ToString());
            if (entidade["FLG_ESPECIAL"] != null) 
                if(entidade["FLG_ESPECIAL"].ToString()=="S")
                    cliente.definirComoEspecial();

            if (entidade["NUM_RG"] != null)
                cliente.alterarRg(entidade["NUM_RG"].ToString());
            if (entidade["SEXO"] != null)
                cliente.alterarSexo(entidade["SEXO"].ToString());
            if (entidade["NUM_END"] != null)
                cliente.alterarNumeroEndereco(entidade["NUM_END"].ToString());
            if (entidade["NOM_BAIRRO"] != null)
                cliente.alterarBairro(entidade["NOM_BAIRRO"].ToString());
            if (entidade["CAD_CEP"] != null)
                cliente.alterarCep(entidade["CAD_CEP"].ToString());
            if (entidade["NUM_TELEFONE"] != null)
                cliente.alterarTelefone(entidade["NUM_TELEFONE"].ToString());
            if (entidade["PER_DESCONTO"] != null)
                cliente.alterarDesconto((double)entidade["PER_DESCONTO"]);

            cliente._id = int.Parse(entidade["ID"].ToString());
            return cliente;

        }

        public Cliente clientePeloEmail(string email)
        {
            string str = "select * from CLIENTE EMAIL = " + Bd.aspas(email);

            dt.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dt);

            return modeloPelaEntidade(dt.Rows[0]);
        }


        public List<Cliente> clientesParaPromocao()
        {
            List<Cliente> result = new List<Cliente>();

            string str = "select * from CLIENTE FLG_PROMOCAO = 'S'";

            dt.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dt);            

            foreach (DataRow dr in dt.Rows)
            {
                Cliente cliente = modeloPelaEntidade(dr);
                result.Add(cliente);
            }

            return result;
        }
    }
}
