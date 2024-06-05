﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface de serviço de domínio para usuário
    /// </summary>
    public interface IUsuarioDomainService
    {
        void Criar(Usuario usuario);

        Usuario Autenticar(string email, string senha);

        Usuario ObterDados(Guid id);
    }
}
