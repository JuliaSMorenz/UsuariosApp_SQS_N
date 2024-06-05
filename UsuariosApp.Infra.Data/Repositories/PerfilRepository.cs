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
    public class PerfilRepository : IPerfilRepository
    {
        public Perfil? GetById(Guid id)
        {
            //abrindo conexão com banco de dados
            using (var dataContext = new DataContext())
            {
                //capturando a conexão aberta com o banco de dados
                var connection = dataContext.Database.GetDbConnection();

                //executando um comando SQL no banco de dados
                return connection
                    .Query<Perfil>("SELECT * FROM PERFIL WHERE ID = @ID", new { id })
                    .FirstOrDefault();
            }
        }

        public Perfil? GetByNome(string nome)
        {
            //abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                //capturando a conexão aberta com o banco de dados
                var connection = dataContext.Database.GetDbConnection();

                //executando um comando SQL no banco de dados
                return connection
                    .Query<Perfil>("SELECT * FROM PERFIL WHERE NOME = @nome", new { nome })
                    .FirstOrDefault();
            }
        }
    }
}
