using Desafio.API.Service;
using Desafio.Model;
using Desafio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Rota que autentica o usuário
        /// </summary>
        /// <returns>Retorna o login do usuário e o token de autenticação  
        /// que deve ser informado nas outras requisições através do header Authorization Bearer</returns>
        /// <response code="200">Retorna o login do usuário e o token de autenticação</response>
        /// <response code="400">Falha ao autenticar.</response>
        /// <response code="401">Login ou senha incorretos.</response>
        [HttpPost]
        [Route("Autenticar")]
        [AllowAnonymous]
        public IActionResult Autenticar(
            [FromBody] UserRequest model,
            [FromServices] AuthService auth)
        {
            try
            {
                var usuario = auth.ValidAuthentication(model.Email, model.Senha);

                if (usuario == null)
                    return Unauthorized(new { message = "Login ou senha inválidos!" });

                var token = TokenService.GenerateToken(usuario);

                if (token == string.Empty)
                    return Unauthorized(new { message = "Erro ao gerar o token! Verifique os dados do seu usuário." });

                return Ok(new
                {
                    login = usuario.Nome,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }       
        }
    }
}
