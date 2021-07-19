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
        /// Responsável por buscar todos os filmes disponiveis
        /// </summary>
        /// <param name="movieService">Serviço responsavel pelas regras de negocio dos filmes</param>
        /// <param name="Active">Status do filme</param>
        /// <returns>Retorna todos os filmes disponiveis</returns>
        /// <response code="200">Retorna todso os filmes</response>
        /// <response code="404">Nenhum filme encontrado</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<MovieResponse>))]
        public IActionResult GetAll([FromServices] IMovieService movieService, bool Active = true)
        {
            var result =  movieService.GetAll(Active);

            return Ok(result);
        }

        [HttpGet]
        [Route("{ID}")]
        [AllowAnonymous]
        public IActionResult GetByid([FromServices] IMovieService movieService, int ID)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{Name}")]
        [AllowAnonymous]
        public IActionResult GetByname([FromServices] IMovieService movieService, string Name, bool Active = true)
        {
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetBy([FromServices] IMovieService movieService,
           IEnumerable<int> Genre, bool? Active = true)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
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

        [HttpDelete]
        [AllowAnonymous]
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

    }
}
