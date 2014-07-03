using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;



namespace IU.Controllers
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            return View(DominioRegistro.clienteService().todosClientes());
        }

        public ActionResult Novo() {
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View("Form", new ClienteData());
        }

        [HttpPost]
        public ActionResult Salvar(ClienteData cliente) {
            ClienteData novo = new ClienteData();

            if (cliente.clienteId == null)
            {
                novo = DominioRegistro.clienteService().novoCliente(cliente.nome, cliente.email);
                cliente.clienteId = novo.clienteId;
            }
            DominioRegistro.clienteService().alterarDados(cliente);
            
            return RedirectToAction("Index", "Cliente");
        }

        public ActionResult Editar(string clienteId = "")
        {
            ClienteData clienteData = DominioRegistro.clienteService().obterCliente(clienteId);
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View("Form", clienteData);
        }

        public ActionResult Excluir(string clienteId = "")
        {
            DominioRegistro.clienteService().excluirCliente(clienteId);
            return RedirectToAction("Index", "Cliente");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

    }
}
