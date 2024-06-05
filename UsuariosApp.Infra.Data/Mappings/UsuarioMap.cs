using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento de entidade
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("ID");
            builder.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(50).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Senha).HasColumnName("SENHA").HasMaxLength(100).IsRequired();
            builder.Property(u => u.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();
            builder.Property(u => u.PerfilId).HasColumnName("PERFIL_ID").IsRequired();

            builder.HasOne(u => u.Perfil) //Usuário TEM 1 Perfil
                .WithMany(p => p.Usuarios) //Perfil TEM MUITOS Usuários
                .HasForeignKey(u => u.PerfilId); //Chave estrangeira do relacionamento
        }
    }
}
