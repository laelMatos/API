using Desafio.Model;
using Desafio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;


namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : Controller
    {
        //private readonly IStringLocalizer<RentController> _localizer;

        //public RentController(IStringLocalizer<RentController> localizer)
        //{
        //    _localizer = localizer;
        //}

        /// <summary>
        /// Criar uma noa locação de filmes
        /// </summary>
        /// <param name="model">Dados do usuario</param>
        /// <returns>O novo usuário criado</returns>
        /// <response code="200">Retorna o objeto da locação salva</response>
        /// <response code="400">Informações inconsistentes da locação</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        public IActionResult CreateUser(
        [FromBody] RentRequest model,
        [FromServices] IRentService RentService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = RentService.Insert(model);

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
