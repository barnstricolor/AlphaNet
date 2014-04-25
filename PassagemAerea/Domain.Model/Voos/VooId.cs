using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.Common.Domain.Model;
namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public class VooId : Identity
    {
        public VooId(string id)
        {
            this.Id = id;
        }
    }
}
