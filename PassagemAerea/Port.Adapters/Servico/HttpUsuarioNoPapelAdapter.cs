using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Servico
{
    public class HttpUsuarioNoPapelAdapter: UsuarioNoPapelAdapter
    {
        public Gestor paraGestor(string identidade)
        {
            RestClient client = new RestClient("http://localhost:51788");
            RestRequest request = new RestRequest("api/usuario/", Method.GET);
            request.AddParameter("nome", identidade);
            request.AddParameter("papel", "Gestor");

            //var response = client.Execute(request);
            var response = client.Execute<PublicoTradutor>(request);
            var content = response.Content;

            if (response.Data == null)
                return null;

            return response.Data.paraGestor();
        }
    }
}
