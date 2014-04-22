﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio
{
    public class EfAviaoRepositio:AviaoRepositorio
    {
        public AviaoId proximaIdentidade()
        {
            return new AviaoId( Guid.NewGuid().ToString().ToUpper());
        }
        public void salvar(Aviao aviao)
        {

        }
        public Aviao obterPeloId(AviaoId aviaoId)
        {
            return new Aviao(proximaIdentidade(),"747",747);
        }
        public void limpar()
        {
            throw new NotImplementedException();
        }
        public List<Aviao> todosAvioes()
        {

            throw new NotImplementedException();
        }
        public void excluir(AviaoId aviaoId)
        {
            throw new NotImplementedException();
        }

    }
}
