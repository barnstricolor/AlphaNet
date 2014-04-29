using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;



namespace IU.Controllers
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            ClienteService cidadeService = new ClienteService();
            return View(cidadeService.todosClientes());
        }

        public ActionResult Novo() {
            return View("Form", new ClienteData());
        }

        [HttpPost]
        public ActionResult Salvar(ClienteData cliente) {
            ClienteService clienteService = new ClienteService();
            if (cliente.clienteId == null)
            {
                clienteService.novaCidade(cliente.nome, cliente.email);
            }
            else {
                clienteService.alterarDados(cliente.clienteId, cliente.nome, cliente.email);
            }
            return RedirectToAction("Index", "Cliente");
        }

        public ActionResult Editar(string clienteId = "")
        {
            ClienteService clienteService = new ClienteService();
            ClienteData clienteData = clienteService.obterCliente(clienteId);
            return View("Form", clienteData);
        }

        public ActionResult Excluir(string clienteId = "")
        {
            ClienteService clienteService = new ClienteService();
            clienteService.excluirCliente(clienteId);
            return RedirectToAction("Index", "Cliente");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
