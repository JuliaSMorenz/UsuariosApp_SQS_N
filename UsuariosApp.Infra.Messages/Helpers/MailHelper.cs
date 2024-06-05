using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Helpers
{
    /// <summary>
    /// Classe auxiliar para realizar o envio de email
    /// </summary>
    public class MailHelper
    {
        public static void Send(NotificacaoEmailModel model)
        {
            //construindo a mensagem
            MailMessage message = new MailMessage(MailSettings.Email, model.EmailDestinatario);
            message.Subject = model.Assunto;
            message.Body = model.Texto;
            message.IsBodyHtml = true;

            //enviando a mensagem
            var smtpClient = new SmtpClient(MailSettings.Smtp, MailSettings.Porta);
            smtpClient.Credentials = new NetworkCredential(MailSettings.Email, MailSettings.Senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
