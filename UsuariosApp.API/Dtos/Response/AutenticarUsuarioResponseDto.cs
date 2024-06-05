namespace UsuariosApp.API.Dtos.Response
{
    /// <summary>
    /// Objeto de dados da resposta de autenticação de usuários
    /// </summary>
    public class AutenticarUsuarioResponseDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
    }
}
