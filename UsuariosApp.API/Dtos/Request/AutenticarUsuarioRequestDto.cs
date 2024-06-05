using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.Dtos.Request
{
    /// <summary>
    /// Objeto de dados da requisição de autenticação de usuários
    /// </summary>
    public class AutenticarUsuarioRequestDto
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])(?!.*\s).{8,}$",
            ErrorMessage = "Informe a senha com letras maiúsculas, minúsculas, números, símbolos e pelo menos 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string? Senha { get; set; }
    }
}
