using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaNet.Acesso.Domain.Model.Usuarios
{
    public class Usuario
    {
        private UsuarioId _usuarioId;
        private string _login;
        private string _senha;
        private string _nome;
        private string _email;
        private Papel _papel;

        public Usuario(
            UsuarioId usuarioId, string login, 
            string senha, string nome, string email, 
            Papel papel)
        {
            this._usuarioId = usuarioId;
            this._login = login;
            this._senha = senha;
            this._nome = nome;
            this._email = email;
            this._papel = papel;
        }
        public string login()
        {
            return this._login;
        }
        public string senha()
        {
            return this._senha;
        }
        public string nome()
        {
            return this._nome;
        }

        public void alterarSenha(string novaSenha)
        {
            this._senha = novaSenha;
        }

        public void alterarNome(string novoNome)
        {
            this._nome = novoNome;
        }

        public void atribuirPapel(Papel novoPapel)
        {
            this._papel = novoPapel;
        }

        public bool desempenhaPapel(Papel novoPapel)
        {
            return this._papel.Equals(novoPapel);
        }
        public UsuarioId usuarioId() {
            return this._usuarioId;
        }

        public string email()
        {
            return this._email;
        }
        public Papel papel()
        {
            return this._papel;
        }
    }
}
