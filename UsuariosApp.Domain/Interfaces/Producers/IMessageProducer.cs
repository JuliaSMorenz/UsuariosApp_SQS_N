using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Domain.Interfaces.Producers
{
    /// <summary>
    /// Interface para implementarmos métodos que irão gravar informações na fila da mensageria
    /// </summary>
    public interface IMessageProducer
    {
        void EnviarMensagem(NotificacaoEmailModel model);
    }
}
