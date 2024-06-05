using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Contrato de métodos para repositório de perfil
    /// </summary>
    public interface IPerfilRepository
    {
        Perfil? GetById(Guid id);
        Perfil? GetByNome(string nome);
    }
}
