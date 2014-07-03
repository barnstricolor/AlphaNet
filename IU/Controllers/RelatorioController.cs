using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using RazorPDF;
using AlphaNet.PassagemAerea.Domain.Model;

namespace IU.Controllers
{
    public class RelatorioController : AbstractController
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Clientes()
        {
            return new PdfResult(DominioRegistro.clienteService().todosClientes(), "Clientes");
        }
        public ActionResult Avioes()
        {
            return new PdfResult(DominioRegistro.aplicacaoAviaoService().todosAvioes(), "Avioes");
        }
        public ActionResult Cidades()
        {
            return new PdfResult(DominioRegistro.cidadeService().todasCidades(), "Cidades");
        }
        public ActionResult Ticket(string clienteId="",string vooId="")
        {
            List<AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData> dados = new List<AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData>();

            foreach (AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData reserva in DominioRegistro.vooService().todasReservas())
                dados.Add(reserva);

            return new PdfResult(dados, "Ticket");
        }

    }
}
