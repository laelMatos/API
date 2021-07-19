using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Desafio.Model;
using Desafio.Service;
using Desafio.Common.Exceptions;

namespace Desafio.API.Controllers
{
    /// <summary>
    /// Controle responsavel pelas rotas dos gêneros de filmes
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GenreController : Controller
    {

        /// <summary>
        /// Responsável por buscar todos os gêneros
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <returns>Retorna os generos encontrados</returns>
        /// <response code="200">gêneros encontrados com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Get([FromServices] IGenreService genreService)
        {
            try
            {
                var result = genreService.GetAll();

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao Buscar todos os gêneros",
                        Success = false
                    } });
            }

        }

        /// <summary>
        /// Responsável por buscar um gênero pelo codigo de identificação
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <returns>Retorna o genero encontrado</returns>
        /// <response code="200">O gênero foi encontrado com sucesso</response>
        /// <response code="404">Nenhum item encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult GetById([FromServices] IGenreService genreService, int id)
        {
            try
            {
                var result = genreService.GetById(id);

                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"O gênero com o código {id} não foi encontrado",
                        Success = false
                    } });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao Buscar o gênero de id {id}",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por criar um novo gênero
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequest">Dados do gênero</param>
        /// <returns>Retorna o gênero criado</returns>
        /// <response code="200">O gênero foi criado com sucesso</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Create([FromServices] IGenreService genreService, GenreRequest genreRequest)
        {
            try
            {
                var result = genreService.Insert(genreRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao inserir um gênero",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por criar multiplos gêneros em uma unica resquisição
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequestList">Dados dos gêneros</param>
        /// <returns>Retorna os gêneros criados</returns>
        /// <response code="200">Os gêneros foram criados com sucesso</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [Route("Range")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult CreateRange([FromServices] IGenreService genreService, IEnumerable<GenreRequest> genreRequestList)
        {
            try
            {
                var result = genreService.InsertRange(genreRequestList);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao inserir multiplos gêneros",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por atualizar um gênero
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequest">Dados do gênero</param>
        /// <returns>Retorna o gênero criado</returns>
        /// <response code="200">O gênero foi alterado com sucesso</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Update([FromServices] IGenreService genreService, GenreRequest genreRequest)
        {
            try
            {
                var result = genreService.Update(genreRequest);

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
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao atualizar um gênero",
                        Success = false
                    } });
            }

        }

        /// <summary>
        /// Responsável por atualizar varios gêneros
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequestList">Dados dos gêneros</param>
        /// <returns>Retorna os gêneros alterados</returns>
        /// <response code="200">O gênero foi alterado com sucesso</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPut]
        [Route("Range")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult UpdateRange([FromServices] IGenreService genreService, IEnumerable<GenreRequest> genreRequestList)
        {
            try
            {
                var result = genreService.UpdateRange(genreRequestList);

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
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao atualizar multiplos gêneros",
                        Success = false
                    } });
            }

        }



        /// <summary>
        /// Responsável por remover um genero especifico
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequest">Dados do gênero</param>
        /// <returns>Retorna o status da tarefa</returns>
        /// <response code="200">Confirmação da remoção do gênero</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="405">Restrição de vinculo com filme</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Delete([FromServices] IGenreService genreService, GenreRequest genreRequest)
        {
            try
            {
                var result = genreService.Delete(genreRequest);

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
            catch(BoundContractException ex)
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
                        Message = $"Falha ao remover um gênero",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por remover varios generos
        /// </summary>
        /// <param name="genreService">Serviço responsavel pelas regras de negocio dos gêneros</param>
        /// <param name="genreRequestList">Dados dos gêneros</param>
        /// <returns>Retorna o status da tarefa</returns>
        /// <response code="200">Confirmação da remoção do gênero</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="405">Restrição de vinculo com filme</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [Route("Range")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult DeleteRange([FromServices] IGenreService genreService, IEnumerable<GenreRequest> genreRequestList)
        {
            try
            {
                var result = genreService.DeleteRange(genreRequestList);

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
                        Message = $"Falha ao remover multiplos gêneros",
                        Success = false
                    } });
            }
        }
    }
}
