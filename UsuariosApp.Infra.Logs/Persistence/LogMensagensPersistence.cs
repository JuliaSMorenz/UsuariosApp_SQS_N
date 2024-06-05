using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Logs.Collections;
using UsuariosApp.Infra.Logs.Contexts;

namespace UsuariosApp.Infra.Logs.Persistence
{
    /// <summary>
    /// Classe de persistencia de dados para gravar, alterar, excluir e consultar
    /// registros na collection de log de mensagens do MongoDB
    /// </summary>
    public class LogMensagensPersistence
    {
        //atributos
        private MongoDBContext _mongoDBContext => new MongoDBContext();

        /// <summary>
        /// Método para gravar 1 registro na collection do MongoDB
        /// </summary>
        public void Insert(LogMensagens logMensagens)
        {
            _mongoDBContext.LogMensagens.InsertOne(logMensagens);
        }

        /// <summary>
        /// Método para atualizar 1 registro na collection do MongoDB
        /// </summary>
        public void Update(LogMensagens logMensagens)
        {
            _mongoDBContext.LogMensagens.ReplaceOne(
                Builders<LogMensagens>.Filter.Eq(log => log.Id, logMensagens.Id),
                logMensagens);
        }

        /// <summary>
        /// Método para excluir 1 registro na collection do MongoDB
        /// </summary>
        public void Delete(Guid id)
        {
            _mongoDBContext.LogMensagens.DeleteOne(
                Builders<LogMensagens>.Filter.Eq(log => log.Id, id)
                );
        }

        /// <summary>
        /// Método para retornar todos os logs dentro de um período de datas
        /// </summary>
        public List<LogMensagens> GetAll(DateTime dataInicio, DateTime dataFim)
        {
            return _mongoDBContext.LogMensagens.Find(
                Builders<LogMensagens>.Filter.And(
                    Builders<LogMensagens>.Filter.Gte(log => log.DataHora, dataInicio), //Greather than or equal
                    Builders<LogMensagens>.Filter.Lte(log => log.DataHora, dataFim) //Less than or equal
                    )
                ).ToList();
        }

        /// <summary>
        /// Método para retornar 1 log baseado no ID
        /// </summary>
        public LogMensagens GetById(Guid id)
        {
            return _mongoDBContext.LogMensagens.Find(
                Builders<LogMensagens>.Filter.Eq(log => log.Id, id)
                ).FirstOrDefault();
        }
    }
}