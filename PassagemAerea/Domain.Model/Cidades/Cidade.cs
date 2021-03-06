﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Domain.Model.Cidades
{
    public class Cidade
    {
        private CidadeId _cidadeId;
        private string _nome;
        private string _cep;
        public int _id { get; set; }

        public Cidade(CidadeId cidadeId, string nome, string cep) {
            this._cidadeId = cidadeId;
            setNome(nome);
            setCep(cep);
        }
        private void setNome(string nome) {
            if (nome == null || nome == "")
                throw new InvalidOperationException("Nome não pode ser vazio ou nulo.");
            this._nome = nome;
        }
        public void alterarNome(string nome)
        {
            setNome(nome);
        }
        public void setCep(string cep)
        {
            if (cep == null || cep == "")
                throw new InvalidOperationException("CEP não pode ser vazio ou nulo.");
            this._cep = cep;
        }
        public void alterarCep(string cep)
        {
            setCep(cep);
        }
        
        public string nome() {
            return this._nome;
        }
        
        public string cep() {
            return this._cep;
        }

        public CidadeId cidadeId() {
            return this._cidadeId;
        }
    }
}
