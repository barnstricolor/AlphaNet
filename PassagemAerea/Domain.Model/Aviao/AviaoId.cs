using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Domain.Model.Aviao
{
    public class AviaoId:Identity
    {
        public AviaoId(string id){
            this.Id  = id;
        }
    }
}
