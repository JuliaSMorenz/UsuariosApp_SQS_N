using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.API.Dtos.Request;
using UsuariosApp.API.Dtos.Response;
using UsuariosApp.API.Services;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //método construtor para inicializar injeção de dependência
        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [HttpPost]
        [Route("criar")] //ENDPOINT: api/usuarios/criar
        [ProducesResponseType(typeof(CriarUsuarioResponseDto), 201)]
        public IActionResult Criar(CriarUsuarioRequestDto request)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = request.Senha,
                    DataHoraCadastro = DateTime.Now
                };

                _usuarioDomainService.Criar(usuario);

                var response = new CriarUsuarioResponseDto
                {
                    Id = usuario.Id.Value,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraCadastro = usuario.DataHoraCadastro
                };

                //HTTP 201 (CREATED)
                return StatusCode(201, new { message = "Usuário cadastrado com sucesso!", response });
            }
            catch(ApplicationException e)
            {
                //HTTP 422 (UNPROCESSABLE ENTITY)
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost]
        [Route("autenticar")] //ENDPOINT: api/usuarios/autenticar
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDto), 200)]        
        public IActionResult Autenticar(AutenticarUsuarioRequestDto request)
        {
            try
            {
                //capturando os dados do usuário "mockado"
                var usuario = _usuarioDomainService.Autenticar(request.Email, request.Senha);

                //criando os dados do usuário autenticado
                var response = new AutenticarUsuarioResponseDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraAcesso = DateTime.Now,
                    AccessToken = JwtBearerService.GenerateToken(usuario.Id.Value),
                    DataHoraExpiracao = DateTime.Now.AddHours(Convert.ToDouble(JwtBearerService.ExpirationInHours))
                };

                //retornando os dados do usuário autenticado
                return StatusCode(200, new { message = "Usuário autenticado com sucesso!", response });
            }
            catch(ApplicationException e)
            {
                //HTTP 401 (UNAUTHORIZED)
                return StatusCode(401, new { e.Message });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("obter-dados")]
        public IActionResult ObterDados()
        {
            try
            {
                //capturando o ID do usuário autenticado contido no TOKEN
                var id = Guid.Parse(User.Identity.Name);

                //buscando os dados do usuário através do ID
                var usuario = _usuarioDomainService.ObterDados(id);

                //retornar os dados do usuário
                var response = new ObterDadosUsuarioResponseDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    PerfilId = usuario.Perfil.Id,
                    NomePerfil = usuario.Perfil.Nome
                };

                return Ok(response); //retornando os dados
            }
            catch(ApplicationException e)
            {
                //HTTP 404 (NOT FOUND)
                return StatusCode(404, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
