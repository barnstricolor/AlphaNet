using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.PassagemAerea.Domain.Model.Avioes
{
    public class Aviao
    {
        private AviaoId _aviaoId;
        private string _modelo;
        private int _assentos;

        public Aviao(AviaoId aviaoId, string modelo, int assentos)
        {
            this._aviaoId = aviaoId;
            setModelo(modelo);
            setAssentos(assentos);
        }

        private void setModelo(string modelo)
        {
            if (modelo == null || modelo == "")
                throw new InvalidOperationException("Modelo não pode ser vazio ou nulo.");
            this._modelo = modelo;
        }

        public void alterarModelo(string novoModelo)
        {
            setModelo(novoModelo);
        }
        
        private void setAssentos(int assentos) { 
            if (assentos>999)
                throw new InvalidOperationException("Quantidade de assentos não pode ser maior que 999.");

            this._assentos = assentos;
        }
        
        public void alterarAssentos(int novaQuantidade)
        {
            setAssentos(novaQuantidade);
        }

        public string modelo()
        {
            return this._modelo;
        }

        public AviaoId aviaoId()
        {
            return this._aviaoId;
        }

        public int assentos()
        {
            return this._assentos;
        }

        public Assento assento(int p)
        {
            return new Assento(p);
        }
    }
}
