using AlphaNet.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public class PrecoPromocionalDefinido : IDomainEvent
    {
        public Voo voo;

        public PrecoPromocionalDefinido(Voo voo) {
            this.voo = voo;
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
        }

        public int EventVersion{get;set;}

        public DateTime OccurredOn{get;set;}
    }
}
