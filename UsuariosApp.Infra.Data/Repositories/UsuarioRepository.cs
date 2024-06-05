using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de banco de dados para operações de usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(usuario);
                dataContext.SaveChanges();
            }
        }

        public Usuario? Get(string email)
        {
            using (var dataContext = new DataContext())
            {
                var connection = dataContext.Database.GetDbConnection();

                return connection
                    .Query<Usuario>("SELECT * FROM USUARIO WHERE EMAIL = @email", new { email })
                    .FirstOrDefault();
            }
        }

        public Usuario? Get(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                var connection = dataContext.Database.GetDbConnection();

                return connection
                    .Query<Usuario>("SELECT * FROM USUARIO WHERE EMAIL = @email AND SENHA = @senha", new { email, senha })
                    .FirstOrDefault();
            }
        }

        public Usuario? GeById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA (EntityFramework)
                /*
                return dataContext.Set<Usuario>()
                    .Include(u => u.Perfil) //INNER JOIN
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
                 */

                //capturando a conexão com o banco de dados
                var connection = dataContext.Database.GetDbConnection();

                //variável para escrevermos a consulta SQL
                var query = @"
                    SELECT * FROM USUARIO u
                    INNER JOIN PERFIL p
                    ON p.ID = u.PERFIL_ID
                    WHERE u.ID = @id
                    ";

                //executando a consulta com o DAPPER
                return connection.Query(query, (Usuario u, Perfil p) =>
                {
                    u.Perfil = p; //JOIN (Associação)
                    return u;
                },
                new { id }, //passando o ID como parâmetro
                splitOn: "PERFIL_ID") //Chave estrangeira
                .FirstOrDefault(); //retornar o primeiro registro encontrado
            }
        }
    }
}
