using Desafio.Common.Exceptions;
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

        /// <summary>
        /// Criar uma noa locação de filmes
        /// </summary>
        /// <param name="rentModel">dados da locação</param>
        /// <returns>locação criada</returns>
        /// <response code="200">Retorna o objeto da locação salva</response>
        /// <response code="400">Informações inconsistentes da locação</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        public IActionResult Create(
        [FromBody] RentRequest rentModel,
        [FromServices] IRentService RentService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = RentService.Insert(rentModel);

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

        /// <summary>
        /// Responsável por atualuzar uma locação
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio das locações</param>
        /// <param name="movie">dados da locação</param>
        /// <returns>Retorna a locação atualizada</returns>
        /// <response code="200">atualizado com sucesso</response>
        /// <response code="400">Nenhuma locação encontrada</response>
        /// <response code="405">Restrição para a tarefa</response>
        /// <response code="500">Erro interno</response>
        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Update(
            [FromBody] RentRequest rentModel,
            [FromServices] IRentService RentService)
        {
            try
            {
                var result = RentService.Update(rentModel);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = ex.Message,
                        Success = false
                    } });
            }
            catch (BoundContractException ex)
            {
                Response.StatusCode = 405;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = ex.Message,
                        Success = false
                    } });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao atualizar uma locação",
                        Success = false
                    } });
            }

        }

        /// <summary>
        /// Responsável por remover uma locação
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio das locações</param>
        /// <param name="movie">dados da locação</param>
        /// <returns>Retorna o status da tarefa</returns>
        /// <response code="200">Removido com sucesso</response>
        /// <response code="400">Nenhuma locação encontrada</response>
        /// <response code="405">Restrição para a tarefa</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Delete(
            [FromBody] RentRequest rentModel,
            [FromServices] IRentService RentService)
        {
            try
            {
                var result = RentService.Delete(rentModel);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = ex.Message,
                        Success = false
                    } });
            }
            catch (BoundContractException ex)
            {
                Response.StatusCode = 405;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = ex.Message,
                        Success = false
                    } });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao remover uma locação",
                        Success = false
                    } });
            }

        }
    }
}
