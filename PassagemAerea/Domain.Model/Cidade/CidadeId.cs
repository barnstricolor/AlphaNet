using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Domain.Model.Cidade
{
    public class CidadeId:Identity
    {
        public CidadeId(string id){
            this.Id  = id;
        }

    }
}
