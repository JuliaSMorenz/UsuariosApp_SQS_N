namespace UsuariosApp.API.Dtos.Response
{
    /// <summary>
    /// Objeto de dados da resposta de criação de usuários
    /// </summary>
    public class CriarUsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
    }
}
