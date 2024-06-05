using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Producers;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    /// <summary>
    /// Classe para conectar no servidor da mensageria e gravar mensagens na fila (incluir itens na fila)
    /// </summary>
    public class MessageProducer : IMessageProducer
    {
        public void EnviarMensagem(NotificacaoEmailModel model)
        {
            #region Conectar no servidor do RabbitMQ

            //conexão com o servidor
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQSettings.URL)
            };

            #endregion

            #region Acessando a fila e gravar a mensagem

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //conexão com a fila do servidor
                    channel.QueueDeclare(
                        queue: RabbitMQSettings.Queue, //nome da fila
                        durable: true, //fila que não é apagada
                        exclusive: false, //fila que não é exclusiva deste projeto
                        autoDelete: false, //fila não exclui mensagens automaticamente
                        arguments: null
                        );

                    //escrevendo a mensagem na fila
                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: RabbitMQSettings.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))
                        );
                }
            }

            #endregion
        }
    }
}
