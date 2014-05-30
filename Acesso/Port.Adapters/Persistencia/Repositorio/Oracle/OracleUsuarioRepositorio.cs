using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;

namespace Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Oracle
{
    public class OracleUsuarioRepositorio : UsuarioRepositorio
    {
        private DataTable dt;

        public OracleUsuarioRepositorio()
        { 
            dt = new DataTable("USUARIO");
            dt.Columns.Add(new DataColumn("USUARIO_ID",typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_USUARIO", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_SENHA", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_LOGIN", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_EMAIL", typeof(string)));
            dt.Columns.Add(new DataColumn("NOM_PAPEL", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
        }

        private void insert(Usuario usuario) {

            OracleDataAdapter da = obterAdapter(null);

            DataRow row = dt.NewRow();
            
            usuario._id = obterSequencia();

            preencherEntidade(row, usuario);

            dt.Rows.Add(row);            

            da.Update(dt);

        }
        private void update(Usuario usuario)
        {
            OracleDataAdapter da = obterAdapter(new UsuarioId(usuario.usuarioId().Id));

            DataRow row = dt.Rows[0];
            
            preencherEntidade(row, usuario);

            da.Update(dt);

        }

        public void salvar(Usuario usuario)
        {
            if (usuario._id.Equals(0))
                insert(usuario);
            else
                update(usuario);            
        }

        public Usuario obterPeloId(UsuarioId usuarioId)
        {
            OracleDataAdapter da = obterAdapter(usuarioId);

            return modeloPelaEntidade(dt.Rows[0]);
        }

        private OracleDataAdapter obterAdapter(UsuarioId usuarioId)
        {            
            string str = "select * from USUARIO";

            if (usuarioId != null)
                str += " Where USUARIO_ID = " + Bd.aspas(usuarioId.Id);

            dt.Clear();
            
            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);
            
            da.Fill(dt);

            return da;
        }

        public List<Usuario> todosAvioes()
        {
            OracleDataAdapter da = obterAdapter(null);

            List<Usuario> result = new List<Usuario>();

            foreach (DataRow dr in dt.Rows) {
                Usuario usuario = modeloPelaEntidade(dr);
                result.Add(usuario);
            }

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(UsuarioId usuarioId)
        {
            OracleDataAdapter da = obterAdapter(usuarioId);

            dt.Rows[0].Delete();

            da.Update(dt);

        }
        public UsuarioId proximaIdentidade()
        {
            return new UsuarioId(Guid.NewGuid().ToString().ToUpper());
        }
        private int obterSequencia()
        {
            return Bd.Instance.obterSequencia("SQ_USUARIO");
        }
        private void preencherEntidade(DataRow entidade, Usuario usuario)
        {
            entidade["USUARIO_ID"] = usuario.usuarioId().Id;
            entidade["NOM_LOGIN"] = usuario.login();
            entidade["NOM_SENHA"] = usuario.senha();
            entidade["NOM_USUARIO"] = usuario.nome();
            entidade["NOM_EMAIL"] = usuario.email();
            entidade["NOM_PAPEL"] = usuario.papel();
            entidade["ID"] = usuario._id;
        }
        private Usuario modeloPelaEntidade(DataRow entidade)
        {
            Usuario usuario = new Usuario(
                                new UsuarioId(entidade["USUARIO_ID"].ToString()),
                                    entidade["NOM_LOGIN"].ToString(),
                                    entidade["NOM_SENHA"].ToString(),
                                    entidade["NOM_USUARIO"].ToString(),
                                    entidade["NOM_EMAIL"].ToString(),
                                    new Papel(entidade["NOM_PAPEL"].ToString()));

            usuario._id = int.Parse(entidade["ID"].ToString());
            return usuario;

        }



        public void remover(UsuarioId usuarioId)
        {
            OracleDataAdapter da = obterAdapter(usuarioId);

            dt.Rows[0].Delete();

            da.Update(dt);
        }


        public Usuario usuarioPelaCredencialAutenticacao(string usuario, string senha)
        {
            string str = "select * from USUARIO Where NOM_USUARIO = " +
                Bd.aspas(usuario) + " And NOM_SENHA = " + Bd.aspas(senha);

            dt.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dt);

            if(dt.Rows.Count>0)
                return modeloPelaEntidade(dt.Rows[0]);

            return null;

        }


        public Usuario obterPeloEmail(string email)
        {
            string str = "select * from USUARIO Where NOM_EMAIL = " + Bd.aspas(email);

            dt.Clear();

            OracleDataAdapter da = new OracleDataAdapter(str, Bd.Instance.obterConexao());

            OracleCommandBuilder cb = new OracleCommandBuilder(da);

            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return modeloPelaEntidade(dt.Rows[0]);

            return null;
        }


        public List<Usuario> obterTodos()
        {
            OracleDataAdapter da = obterAdapter(null);

            List<Usuario> result = new List<Usuario>();

            foreach (DataRow dr in dt.Rows)
            {
                Usuario usuario = modeloPelaEntidade(dr);
                result.Add(usuario);
            }

            return result;

        }
    }
}
