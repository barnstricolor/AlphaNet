using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class CPF
    {
        private string _cpf;

        public CPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            /*if (valor.Length != 11)
                throw new InvalidOperationException("C.P.F. deve conter 11 digitos.");

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0]) 
                    igual = false;
            
            if (igual || valor == "12345678909")
                throw new InvalidOperationException("C.P.F. Inválido");

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    throw new InvalidOperationException("C.P.F. Inválido");
            }
            else if (numeros[9] != 11 - resultado)
                throw new InvalidOperationException("C.P.F. Inválido");
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    throw new InvalidOperationException("C.P.F. Inválido");
            }
            else if (numeros[10] != 11 - resultado)
                throw new InvalidOperationException("C.P.F. Inválido");
            */
            setCPF(vrCPF);
        }
        private void setCPF(string cpf) {
            this._cpf = cpf;
        }
        public override string ToString()
        {
            //return base.ToString();
            return this._cpf;
        }
    } 
}



