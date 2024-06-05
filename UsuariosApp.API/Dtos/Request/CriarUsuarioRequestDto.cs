using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.Dtos.Request
{
    /// <summary>
    /// Objeto de dados da requisição de criação de usuários
    /// </summary>
    public class CriarUsuarioRequestDto
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])(?!.*\s).{8,}$",
            ErrorMessage = "Informe a senha com letras maiúsculas, minúsculas, números, símbolos e pelo menos 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
