using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Logs.Collections;
using UsuariosApp.Infra.Logs.Persistence;
using UsuariosApp.Infra.Messages.Helpers;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Consumers
{
    /// <summary>
    /// Classe para ler cada mensagem contida na fila e realizar o envio dessa mensagem por email...
    /// </summary>
    public class MessageConsumer : BackgroundService
    {
        #region Atributos

        private readonly IServiceProvider? _serviceProvider;
        private readonly IConnection? _connection;
        private readonly IModel? _model;

        #endregion

        #region Método construtor

        public MessageConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //conectando no servidor da mensageria
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQSettings.URL)
            };

            //conecctando na fila para fazer a leitura das mensagens
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model?.QueueDeclare(
                queue: RabbitMQSettings.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        #endregion

        //Método para executar a leitura da fila
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //criando um objeto para ler o conteúdo da fila
            var consumer = new EventingBasicConsumer(_model);

            //realizando a leitura
            consumer.Received += (sender, args) =>
            {
                //lendo cada mnsagem contida na fila
                var contentArray = args.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                //deserializando a mensagem (JSON -> OBJETO)
                var emailModel = JsonConvert.DeserializeObject<NotificacaoEmailModel>(contentString);

                #region Enviando o email para o usuário

                using (var scope = _serviceProvider.CreateScope())
                {
                    var logMensagens = new LogMensagens
                    {
                        Id = Guid.NewGuid(),
                        DataHora = DateTime.Now
                    };

                    try
                    {
                        //enviar o email para o usuário
                        MailHelper.Send(emailModel);

                        logMensagens.Status = "Sucesso";
                        logMensagens.Descricao = $"Email enviado com sucesso para: {emailModel.EmailDestinatario}";
                    }
                    catch(Exception e)
                    {
                        logMensagens.Status = "Erro";
                        logMensagens.Descricao = $"Falha ao enviar o email para: {emailModel.EmailDestinatario} - {e.Message}";
                    }
                    finally
                    {
                        var logMensagensPersistence = new LogMensagensPersistence();
                        logMensagensPersistence.Insert(logMensagens);

                        //retirar a mensagem da fila
                        _model?.BasicAck(args.DeliveryTag, false);
                    }

                }

                #endregion
            };

            _model?.BasicConsume(RabbitMQSettings.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}
