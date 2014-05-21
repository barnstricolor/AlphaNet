using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;

namespace AlphaNet.Common.Port.Adapters
{
    public class Email
    {

        public void enviar(string email,string texto)
        {
            MailMessage mensagem = new MailMessage(
                "er4aerea@gmail.com",
                email,
                "Saldão ER4Aerea - Passagens Nacionais até pela metade do preço‏",
                texto);
            
            //Attachment anexo = new Attachment(((ListBoxItem)item).Content.ToString());
            //mail.Attachments.Add(anexo);
            
            SmtpClient envio = new SmtpClient("smtp.gmail.com",587);
            //envio.Port = 25;
            //envio.Timeout = 10;
            
            envio.Credentials = new NetworkCredential("er4aerea","peludinha");
            
            envio.EnableSsl = true;
            //envio.SendAsync(mensagem,null);
            envio.Send(mensagem);
        }

    }
}
