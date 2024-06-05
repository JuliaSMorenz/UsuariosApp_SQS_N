using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Messages.Settings
{
    /// <summary>
    /// Classe para configurarmos os parametros de envio de email
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Conta de email utilizada para disparar as mensagens
        /// </summary>
        public static string Email => "sergiojavaarq@outlook.com";

        /// <summary>
        /// Senha da conta de email
        /// </summary>
        public static string Senha => "@Admin12345";

        /// <summary>
        /// Caminho do servidor SMTP da conta de email
        /// </summary>
        public static string Smtp => "smtp-mail.outlook.com";

        /// <summary>
        /// Porta para conexão com o servidor de email
        /// </summary>
        public static int Porta => 587;
    }
}
