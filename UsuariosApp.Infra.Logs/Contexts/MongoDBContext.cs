using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Logs.Collections;
using UsuariosApp.Infra.Logs.Settings;

namespace UsuariosApp.Infra.Logs.Contexts
{
    /// <summary>
    /// Classe para conexão com o banco de dados do MongoDB
    /// </summary>
    public class MongoDBContext
    {
        /// <summary>
        /// Atributo para guardar a conexão do banco de dados
        /// </summary>
        private IMongoDatabase _mongoDatabase;

        //método construtor
        public MongoDBContext()
        {
            //caminho do servidor do banco de dados
            var mongoClient = MongoClientSettings.FromUrl(new MongoUrl(MongoDBSettings.Host));

            //verificar se a conexão usa SSL -> Security Socket Layer
            if (MongoDBSettings.IsSSL)
            {
                mongoClient.SslSettings = new SslSettings()
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };
            }

            //abrindo a conexão com o banco de dados do MongoDB
            var client = new MongoClient(mongoClient);
            _mongoDatabase = client.GetDatabase(MongoDBSettings.Database);
        }

        public IMongoCollection<LogMensagens> LogMensagens
            => _mongoDatabase.GetCollection<LogMensagens>("LogMensagens");
    }
}
