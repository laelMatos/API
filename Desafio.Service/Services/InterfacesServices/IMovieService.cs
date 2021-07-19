using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public interface IMovieService
    {
        /// <summary>
        /// Busca todos os objetos no banco de dados
        /// </summary>
        /// <returns>Retorna todas as entidades encontradas</returns>
        IEnumerable<MovieResponse> GetAll(bool? active);
        /// <summary>
        /// Busca o objeto que tenha a chava correspondente à solicitada no banco de dados
        /// </summary>
        /// <param name="id">Chave de identificação</param>
        /// <returns>Retorna a entidade encontrada</returns>
        MovieResponse GetById(int id);
        /// <summary>
        /// Busca o objeto que tenah a propriedade nome igual ou contenha parte do valor
        /// </summary>
        /// <param name="name">nome a se verificado</param>
        /// <returns></returns>
        IEnumerable<MovieResponse> GetByname(string name, bool? active = true);
        /// <summary>
        /// Busca os filmes que tenha algum dos generos listados e tenha o mesmo status
        /// </summary>
        /// <param name="genres">generos</param>
        /// <param name="active">status</param>
        /// <returns>Lista de filmes</returns>
        IEnumerable<MovieResponse> GetByGenre(IEnumerable<int> genres, bool? active = true);
        /// <summary>
        /// Insere um objeto ao banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser inserido</param>
        /// <returns>Retorna o objeto inserido</returns>
        MovieResponse Insert(MovieRequest entity);
        /// <summary>
        /// Insere varios objetos ao banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem inseridos</param>
        /// <returns>Retorna os objetos inseridos</returns>
        IEnumerable<MovieResponse> InsertRange(IEnumerable<MovieRequest> entity);
        /// <summary>
        /// Atualiza um objeto no banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser atualizado</param>
        /// <returns>Retorna o objeto atualizado</returns>
        MovieResponse Update(MovieRequest entity);
        /// <summary>
        /// Atualiza varios objetos no banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem atualizado</param>
        /// <returns>Retorna os objetos atualizados</returns>
        IEnumerable<MovieResponse> UpdateRange(IEnumerable<MovieRequest> entity);
        /// <summary>
        /// Remove o objeto do banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser Removido</param>
        /// <returns>Confirmação de conclusão da tarefa</returns>
        bool Delete(MovieRequest entity);
        /// <summary>
        /// Remove varios objetos do banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem Removidos</param>
        /// <returns>Confirmação de conclusão da tarefa</returns>
        bool DeleteRange(IEnumerable<MovieRequest> entity);
    }
}
