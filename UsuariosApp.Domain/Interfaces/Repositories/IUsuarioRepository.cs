using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Contrato de métodos para o repositório de usuário
    /// </summary>
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);

        Usuario? Get(string email);

        Usuario? Get(string email, string senha);

        Usuario? GeById(Guid id);
    }
}
