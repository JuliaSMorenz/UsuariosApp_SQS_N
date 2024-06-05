using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Messages.Settings
{
    /// <summary>
    /// Classe para configurarmos os parâmetros de conexão do RabbitMQ
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// URL para conexão com o servidor do RabbitMQ
        /// </summary>
        public static string URL = "amqps://wbevggce:ZjClE-ENkdC6o-d1VfDVD_hBKVoBrkCI@moose.rmq.cloudamqp.com/wbevggce";

        /// <summary>
        /// Nome da fila do RabbitMQ
        /// </summary>
        public static string Queue = "emails_usuarios";
    }
}
