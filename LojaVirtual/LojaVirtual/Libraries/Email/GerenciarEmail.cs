using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public  void EnviarContatoPorEmail(Contato contato)
        {
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
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add("francisco.rafaell.pereira@gmail.com");
            mensagem.Subject = "Contato - LojaVirtual - Email: " + contato.Email;//assunto
            mensagem.Body = corpoMsg;//conteudo
            mensagem.IsBodyHtml = true; //para o corpo da mensagem aceitar HTML


            //Enviar mensagem via SMTP
            _smtp.Send(mensagem); 
        }

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Colaborador - LojaVirtual</h2>" + 
                "Sua Senha é:" + 
                "<h3>{0}</h3>", colaborador.Senha);

            //MailMessage > Construir a mensagem

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - LojaVirtual - Senha do Colaborador: " + colaborador.Nome;//assunto
            mensagem.Body = corpoMsg;//conteudo
            mensagem.IsBodyHtml = true; //para o corpo da mensagem aceitar HTML


            //Enviar mensagem via SMTP
            _smtp.Send(mensagem);
        }
    }
}
