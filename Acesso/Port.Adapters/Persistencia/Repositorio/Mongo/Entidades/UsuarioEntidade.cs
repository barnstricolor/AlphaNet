using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.Acesso.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades
{
    public class UsuarioEntidade:Entidade
    {
        public UsuarioId _usuarioId;
        public string _login;
        public string _senha;
        public string _nome;
        public string _email;
        public Papel _papel;
        public int _id { get; set; }
        
        public UsuarioEntidade(){
        }
        /*public UsuarioEntidade(
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
        }*/
    }
}
