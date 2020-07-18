using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            //SMTP --Servidor que vai enviar a mensagem

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);   //smtp Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("francisco.rafaell.pereira@gmail.com","*"); //email e senha 
            smtp.EnableSsl = true; //protocolo de segurança

            //corpo da mensagem
            string corpoMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                "<b>Nome:     </b> {0} <br />" +
                "<br>E-mail: </br> {1} <br />" +
                "<br>Texto: </br>  {2} <br />" +
                "<br /> Email enviado automaticamento do site LojaVirtual.",
                contato.Nome,
                contato.Email,
                contato.Texto
                );

            //MailMessage > Construir a mensagem

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("francisco.rafaell.pereira@gmail.com");
            mensagem.To.Add("francisco.rafaell.pereira@gmail.com");
            mensagem.Subject = "Contato - LojaVirtual - Email: " + contato.Email;//assunto
            mensagem.Body = corpoMsg;//conteudo
            mensagem.IsBodyHtml = true; //para o corpo da mensagem aceitar HTML


            //Enviar mensagem via SMTP
            smtp.Send(mensagem); 
        }
    }
}
