using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Servico
{
    public class PublicoTradutor
    {
        public string nome { get; set; }
        public string email { get; set; }

        public Gestor paraGestor() {

            return new Gestor(email, nome, email);
        }
    }
}
