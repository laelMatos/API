using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Desafio.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Desafio.Model;
using Desafio.Common.Exceptions;
using System;

namespace Desafio.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        /// <summary>
        /// Responsável por buscar todos os filmes disponiveis com o status definido
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="Active">Status do filme</param>
        /// <returns>Retorna todos os filmes disponiveis</returns>
        /// <response code="200">Retorna todso os filmes encontrados</response>
        /// <response code="404">Nenhum filme encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult GetAll([FromServices] IMovieService movieService, bool Active = true)
        {
            try
            {
                var result = movieService.GetAll(Active);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao buscar filmes",
                        Success = false
                    } });
            }
            
        }

        /// <summary>
        /// Responsável por buscar filmes por seu codigo de identificação
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="ID">identificação do filme</param>
        /// <returns>Retorna todos os filmes disponiveis</returns>
        /// <response code="200">Retorna todso os filmes encontrados</response>
        /// <response code="404">Nenhum filme encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("{ID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult GetByid([FromServices] IMovieService movieService, int ID)
        {
            try
            {
                var result = movieService.GetById(ID);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao buscar filme",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por buscar filmes por seu nome e status
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="Name">nome do filme</param>
        /// <param name="Active">status do filme</param>
        /// <returns>Retorna todos os filmes disponiveis</returns>
        /// <response code="200">Retorna todso os filmes encontrados</response>
        /// <response code="404">Nenhum filme encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("{Name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult GetByname([FromServices] IMovieService movieService, string Name, bool? Active = true)
        {
            try
            {
                var result = movieService.GetByname(Name, Active);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao buscar filmes",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por buscar filmes por seu genero e status
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="Genres">lista de generos</param>
        /// <param name="Active">status do filme</param>
        /// <returns>Retorna todos os filmes encontrados</returns>
        /// <response code="200">Retorna todos os filmes encontrados</response>
        /// <response code="404">Nenhum filme encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult GetByGenre([FromServices] IMovieService movieService,
           IEnumerable<int> Genres, bool? Active = true)
        {
            try
            {
                var result = movieService.GetByGenre(Genres, Active);

                return Ok(result);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao buscar filmes",
                        Success = false
                    } });
            }
        }

        /// <summary>
        /// Responsável por salvar filme
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="movie">dados do filmes</param>
        /// <returns>Retorna o filme salvo</returns>
        /// <response code="200">Retorna o filme salvo</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Create([FromServices] IMovieService movieService,
           MovieRequest movie)
        {
            try
            {
                var result = movieService.Insert(movie);

                return Ok(result);
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
                return Json(new List<ResultResponse>() {
                    new ResultResponse()
                    {
                        Message = $"Falha ao inserir um filme",
                        Success = false
                    } });
            }
            
        }

        /// <summary>
        /// Responsável por atualizar filme
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="movieModel">dados do filme</param>
        /// <returns>Retorna o filme atualizado</returns>
        /// <response code="200">atualizado com sucesso</response>
        /// <response code="400">Nenhum filme encontrado</response>
        /// <response code="405">Restrição para a tarefa</response>
        /// <response code="500">Erro interno</response>
        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Update([FromServices] IMovieService movieService,
           MovieRequest movieModel)
        {
            try
            {
                var result = movieService.Update(movieModel);

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
                        Message = $"Falha ao atualizar um filme",
                        Success = false
                    } });
            }

        }

        /// <summary>
        /// Responsável por remover filme
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="movie">dados do filme</param>
        /// <returns>Retorna o status da tarefa</returns>
        /// <response code="200">Removido com sucesso</response>
        /// <response code="400">Nenhum filme encontrado</response>
        /// <response code="405">Restrição para a tarefa</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult Delete([FromServices] IMovieService movieService,
           MovieRequest movie)
        {
            try
            {
                var result = movieService.Delete(movie);

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
                        Message = $"Falha ao inserir um filme",
                        Success = false
                    } });
            }

        }

        /// <summary>
        /// Responsável por remover uma lista de filmes
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="movies">lista de filme</param>
        /// <returns>Retorna o status da tarefa</returns>
        /// <response code="200">removido com sucesso</response>
        /// <response code="400">Nenhum filme encontrado</response>
        /// <response code="405">Restrição para a tarefa</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [Route("Range")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(List<ResultResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ResultResponse>))]
        public IActionResult DeleteRange([FromServices] IMovieService movieService,
           IEnumerable<MovieRequest> movies)
        {
            try
            {
                var result = movieService.DeleteRange(movies);

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
                        Message = $"Falha ao inserir um filme",
                        Success = false
                    } });
            }

        }

    }
}
