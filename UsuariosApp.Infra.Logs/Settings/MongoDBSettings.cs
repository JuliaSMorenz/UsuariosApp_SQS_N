using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Logs.Settings
{
    /// <summary>
    /// Classe para configurar os parâmetros de conexão com o MongoDB
    /// </summary>
    public class MongoDBSettings
    {
        /// <summary>
        /// Caminho do servidor do banco de dados do MongoDB
        /// </summary>
        public static string Host => "mongodb://localhost:27017/";

        /// <summary>
        /// Nome do banco de dados do MongoDB
        /// </summary>
        public static string Database => "DBLog_N";

        /// <summary>
        /// Indicar se será utilizado criptografia de rede
        /// </summary>
        public static bool IsSSL => false;
    }
}



