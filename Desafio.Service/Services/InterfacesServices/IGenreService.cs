using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public interface IGenreService
    {
        /// <summary>
        /// Busca todos os objetos no banco de dados
        /// </summary>
        /// <returns>Retorna todas as entidades encontradas</returns>
        IEnumerable<GenreResponse> GetAll();
        /// <summary>
        /// Busca o objeto que tenha a chava correspondente à solicitada no banco de dados
        /// </summary>
        /// <param name="id">Chave de identificação</param>
        /// <returns>Retorna a entidade encontrada</returns>
        GenreResponse GetById(int id);
        /// <summary>
        /// Insere um objeto ao banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser inserido</param>
        /// <returns>Retorna o objeto inserido</returns>
        GenreResponse Insert(GenreRequest entity);
        /// <summary>
        /// Insere varios objetos ao banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem inseridos</param>
        /// <returns>Retorna os objetos inseridos</returns>
        IEnumerable<GenreResponse> InsertRange(IEnumerable<GenreRequest> entity);
        /// <summary>
        /// Atualiza um objeto no banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser atualizado</param>
        /// <returns>Retorna o objeto atualizado</returns>
        GenreResponse Update(GenreRequest entity);
        /// <summary>
        /// Atualiza varios objetos no banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem atualizado</param>
        /// <returns>Retorna os objetos atualizados</returns>
        IEnumerable<GenreResponse> UpdateRange(IEnumerable<GenreRequest> entity);
        /// <summary>
        /// Remove o objeto do banco de dados
        /// </summary>
        /// <param name="entity">objeto a ser Removido</param>
        /// <returns>Confirmação de conclusão da tarefa</returns>
        bool Delete(GenreRequest entity);
        /// <summary>
        /// Remove varios objetos do banco de dados
        /// </summary>
        /// <param name="entity">objetos a serem Removidos</param>
        /// <returns>Confirmação de conclusão da tarefa</returns>
        bool DeleteRange(IEnumerable<GenreRequest> entity);
    }
}
