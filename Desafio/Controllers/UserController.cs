using Desafio.Model;
using Desafio.Repository;
using Desafio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// Responsável por inserir um novo usuário
        /// </summary>
        /// <param name="model">Dados do usuario</param>
        /// <returns>O novo usuário criado</returns>
        /// <response code="200">Retorna o novo usuário criado</response>
        /// <response code="400">Informações inconsistentes do novo usuário</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        public IActionResult CreateUser(
        [FromBody] UserInsertRequest model,
        [FromServices] UserService UserService )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = UserService.Insert(model);

                if (!result.GetType().Equals(typeof(UserResponse)))
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new List<ResultResponse>(){new ResultResponse()
                {
                    Message = ex.Message,
                    ErrorField = "",
                    Success = false
                }});
            }
        }
    }
}
