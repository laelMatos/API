using Desafio.Common.Exceptions;
using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Service
{
    public class GenreService : IGenreService
    {
        /// <summary>
        /// Repositório Genre
        /// </summary>
        private readonly GenreRepository GENRE_REPOS;
        /// <summary>
        /// Serviço de locação
        /// </summary>
        /// <param name="genreRepos">Repositório locação</param>
        public GenreService(GenreRepository genreRepos)
        {
            GENRE_REPOS = genreRepos;
        }

        public IEnumerable<GenreResponse> GetAll()
        {
            try
            {
                return GENRE_REPOS.GetAll().Select(x => new GenreResponse() { ID = x.ID, Name = x.Name });
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public GenreResponse GetById(int id)
        {
            try
            {
                var genreDb = GENRE_REPOS.GetByParam(x=> x.ID == id).FirstOrDefault();
                if (genreDb != null)
                    return new GenreResponse() { ID = genreDb.ID, Name = genreDb.Name };
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public GenreResponse Insert(GenreRequest entity)
        {
            try
            {
                var result = GENRE_REPOS.Insert(new Genre()
                {
                    Name = entity.Name,
                    Create_at = DateTime.Now,
                    Status = entity.Status
                });

                if (result == null)
                    throw new Exception();

                return new GenreResponse()
                {
                    ID = result.ID,
                    Name = result.Name
                };
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public IEnumerable<GenreResponse> InsertRange(IEnumerable<GenreRequest> entity)
        {
            try
            {
                var result = GENRE_REPOS.InsertRange(entity.Select(x=> new Genre()
                {
                    Name = x.Name,
                    Create_at = DateTime.Now,
                    Status = x.Status
                }));

                if (result == null)
                    throw new Exception();

                return result.Select(x=> new GenreResponse()
                {
                    ID = x.ID,
                    Name = x.Name
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GenreResponse Update(GenreRequest entity)
        {
            try
            {
                var genreDb = GENRE_REPOS.GetByParam(x => x.ID == entity.ID ).FirstOrDefault();

                if (genreDb == null)
                    throw new KeyNotFoundException($"O gênero listado não existe");

                genreDb.Name = entity.Name;
                genreDb.Status = entity.Status;

                var result = GENRE_REPOS.Update(genreDb);

                return new GenreResponse()
                {
                    ID = genreDb.ID,
                    Name = genreDb.Name
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GenreResponse> UpdateRange(IEnumerable<GenreRequest> entity)
        {
            try
            {
                //pega todos os generos que tem o codigo de identificação igual aos da lista recebida
                var genreDb = GENRE_REPOS.GetByParam(x => entity.Any(e=> e.ID == x.ID));
                

                if (genreDb == null || entity.Count() > genreDb.Count())
                    throw new KeyNotFoundException($"Os gêneros listados não existem");

                foreach (var item in genreDb)
                {
                    item.Status = entity.Where(x => x.ID == item.ID).First().Status;
                    item.Name = entity.Where(x => x.ID == item.ID).First().Name;
                }

                var result = GENRE_REPOS.UpdateRange(genreDb);

                if (result == null)
                    throw new Exception();

                return result.Select(x=> new GenreResponse()
                {
                    ID = x.ID,
                    Name = x.Name
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(GenreRequest entity)
        {
            try
            {
                var genreDb = GENRE_REPOS.GetByParam(x => x.ID == entity.ID).FirstOrDefault();

                if (genreDb == null)
                    throw new KeyNotFoundException($"O gênero {entity.ID} listado não existe");

                if (genreDb.GenreMovies.Count() > 0)
                    throw new BoundContractException($"O gênero {entity.Name} está vinculado a filmes, a ação foi cancelada e não foi removido");

                var result = GENRE_REPOS.Delete(genreDb);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRange(IEnumerable<GenreRequest> entity)
        {
            try
            {
                //pega todos os generos que tem o codigo de identificação igual aos da lista recebida
                var genreDb = GENRE_REPOS.GetByParam(x => entity.Any(e => e.ID == x.ID));


                if (genreDb == null || entity.Count() > genreDb.Count())
                    throw new KeyNotFoundException($"Os gêneros listados ja não existem");

                if (genreDb.Any(x=>x.GenreMovies.Count() > 0))
                    throw new BoundContractException($"Existem gênero/s que estão vinculados a filmes, a ação foi cancelada e não foram removidos");

                var result = GENRE_REPOS.DeleteRange(genreDb);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
