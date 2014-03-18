using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Aviao
{
    public class Aviao
    {
        private AviaoId _aviaoId;
        private string _modelo;
        private int _assentos;

        public Aviao(AviaoId aviaoId, string modelo, int assentos)
        {
            this._aviaoId = aviaoId;
            this._modelo = modelo;
            this._assentos = assentos;
        }
        public void alterarModelo(string novoModelo){
            setModelo(novoModelo);
        }
        private void setModelo(string modelo){
            if(modelo==null || modelo=="")
                throw new InvalidOperationException("Modelo não pode ser vazio ou nulo.");
            this._modelo=modelo;
        }
        public string modelo(){
            return this._modelo;
        }
        public AviaoId aviaoId() {
            return this._aviaoId;
        }
    }
}
