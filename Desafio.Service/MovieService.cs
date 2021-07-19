using Desafio.Common.Exceptions;
using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Service
{
    public class MovieService : IMovieService
    {
        /// <summary>
        /// Repositório Movie
        /// </summary>
        private MovieRepository MOVIE_REPOS;
        /// <summary>
        /// Repositório Genre
        /// </summary>
        private IGenericRepository<Genre> GENRE_REPOS;
        /// <summary>
        /// Repositorio GenreMovie
        /// </summary>
        private IGenericRepository<GenreMovie> GENRE_MOVIE_REPOS;

        public MovieService(MovieRepository movieRepos, IGenericRepository<Genre> genreRepos, IGenericRepository<GenreMovie> genericRepos)
        {
            MOVIE_REPOS = movieRepos;
            GENRE_REPOS = genreRepos;
            GENRE_MOVIE_REPOS = genericRepos;
        }

        public IEnumerable<MovieResponse> GetAll(bool? active = true)
        {
            try
            {
                var listMovies = MOVIE_REPOS.GetByParam(x => x.Active == active);
                var genreDb = GENRE_REPOS.GetByParam(x => listMovies.Any(m => m.GenreMovies.Any(g => g.GenreId == x.ID)));

                foreach (var item in listMovies.Select(x => x.GenreMovies))
                {
                    foreach (var gmov in item)
                    {
                        gmov.Genre = genreDb.Where(g => g.ID == gmov.GenreId).First();
                    }

                }

                return SetListMovieResponse(listMovies);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public IEnumerable<MovieResponse> GetByGenre(IEnumerable<int> genre, bool? active = true)
        {
            if (genre.Count() == 0)
                return null;

            try
            {
                var listMovies = MOVIE_REPOS.GetByGenre(genre, active);

                if (listMovies.Count() == 0)
                    return null;
                //Atuaza e busca a lista de gêneros
                listMovies = MOVIE_REPOS.GetByParam(x=> listMovies.Any(m=>m.ID == x.ID));

                var genreDb = GENRE_REPOS.GetByParam(x => listMovies.Any(m => m.GenreMovies.Any(g => g.GenreId == x.ID)));

                foreach (var item in listMovies.Select(x => x.GenreMovies))
                {
                    foreach (var gmov in item)
                    {
                        gmov.Genre = genreDb.Where(g => g.ID == gmov.GenreId).First();
                    }

                }

                return SetListMovieResponse(listMovies);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MovieResponse GetById(int id)
        {
            try
            {
                var movieDb =  MOVIE_REPOS.GetById(id);

                if (movieDb == null)
                    return null;

                var genreDb = GENRE_REPOS.GetByParam(x => movieDb.GenreMovies.Any(g => g.GenreId == x.ID));

                foreach (var item in movieDb.GenreMovies)
                {
                    item.Genre = genreDb.Where(g => g.ID == item.GenreId).First();
                }

                return SetMovieResponse(movieDb);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public IEnumerable<MovieResponse> GetByname(string name, bool? active = true)
        {
            try
            {
                var listMovies = MOVIE_REPOS.GetByParam(x=>x.Name.ToLower().Contains(name) && x.Active == active);
                
                if (listMovies == null)
                    return null;

                var genreDb = GENRE_REPOS.GetByParam(x => listMovies.Any(m => m.GenreMovies.Any(g => g.GenreId == x.ID)));

                foreach (var item in listMovies.Select(x => x.GenreMovies))
                {
                    foreach (var gmov in item)
                    {
                        gmov.Genre = genreDb.Where(g => g.ID == gmov.GenreId).First();
                    }

                }

                return SetListMovieResponse(listMovies);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MovieResponse Insert(MovieRequest movie)
        {
            try
            {
                var newMovie = new Movie()
                {
                    Name = movie.Name,
                    Create_at = DateTime.Now,
                    Active = movie.Active!=0,
                };

                foreach (var item in movie.Genre)
                {
                    newMovie.GenreMovies.Add(new GenreMovie() { GenreId = item });
                }

                var movieDb = MOVIE_REPOS.Insert(newMovie);

                var genreDb = GENRE_REPOS.GetByParam(x=> movieDb.GenreMovies.Any(g=>g.GenreId == x.ID));

                foreach (var item in movieDb.GenreMovies)
                {
                    item.Genre = genreDb.Where(g => g.ID == item.GenreId).First();
                }

                return SetMovieResponse(movieDb);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<MovieResponse> InsertRange(IEnumerable<MovieRequest> entity)
        {
            try
            {
                var newMovieList = new List<Movie>();
                //criando uma lista de Movie com os dados de MovieRequest
                foreach (var item in entity)
                {
                    Movie newMovie =  new Movie()
                    {
                        Name = item.Name,
                        Create_at = DateTime.Now,
                        Active = item.Active!=0,
                    };

                    foreach (var Genre in item.Genre)
                    {
                        newMovie.GenreMovies.Add(new GenreMovie() { GenreId = Genre });
                    }

                    newMovieList.Add(newMovie);
                }
                var movieDb = MOVIE_REPOS.InsertRange(newMovieList);

                var genreDb = GENRE_REPOS.GetByParam(x => movieDb.Any(m => m.GenreMovies.Any(g => g.GenreId == x.ID)));

                foreach (var item in movieDb.Select(x => x.GenreMovies))
                {
                    foreach (var gmov in item)
                    {
                        gmov.Genre = genreDb.Where(g => g.ID == gmov.GenreId).First();
                    }

                }

                return SetListMovieResponse(newMovieList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MovieResponse Update(MovieRequest entity)
        {
            try
            {
                var movieDb = MOVIE_REPOS.GetById(entity.ID);

                if (movieDb == null)
                    throw new KeyNotFoundException($"O filme com o código {entity.ID} não foi encontrado");

                movieDb.Name = entity.Name;
                movieDb.Update_at = DateTime.Now;
                movieDb.Active = entity.Active!=0;

                var genreMovDb = GENRE_MOVIE_REPOS.GetByParam(x=>x.MovieId == entity.ID);

                //gênero a ser inserido
                //Validar a necessidade
                var genreMovInsert = entity.Genre.Where(x => genreMovDb.Any(g => g.GenreId != x));
                //Gênero a ser removido
                var genreMovRemove = genreMovDb.Where(x => entity.Genre.Any(g => g != x.GenreId));
                GENRE_MOVIE_REPOS.DeleteRange(genreMovRemove);

                //Montando o objeto GenreMovie com a lista de ids recebido
                foreach (var item in entity.Genre)
                {
                    movieDb.GenreMovies.Add(new GenreMovie() { GenreId = item });
                }
                var result = MOVIE_REPOS.Update(movieDb);

                //Busca os gêneros para popular MovieResponse
                var genreDb = GENRE_REPOS.GetByParam(x => movieDb.GenreMovies.Any(g => g.GenreId == x.ID));

                //Insere cada gênero 
                foreach (var item in movieDb.GenreMovies)
                {
                    item.Genre = genreDb.Where(g => g.ID == item.GenreId).First();
                }

                return SetMovieResponse(movieDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MovieResponse> UpdateRange(IEnumerable<MovieRequest> entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(MovieRequest entity)
        {
            try
            {
                var movieDb = MOVIE_REPOS.GetById(entity.ID);

                if (movieDb == null)
                    throw new KeyNotFoundException($"O filme com o código {entity.ID} não foi encontrado");

                if (movieDb.RentMovies.Count() > 0)
                    throw new BoundContractException($"O filme {entity.Name} tem locação vinculada e não pode ser removido");

                return MOVIE_REPOS.Delete(movieDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRange(IEnumerable<MovieRequest> entity)
        {
            try
            {
                var movieDb = MOVIE_REPOS.GetByParam(x => entity.Any(m=>m .ID == x.ID && x.Name == m.Name));
                if (movieDb == null)
                    throw new KeyNotFoundException($"Os filmes listados não foram encontrados");

                if (movieDb.Any(x=>x.RentMovies.Count() > 0))
                    throw new BoundContractException($"Os filmes listados, tem locação vinculada e não podem ser removido");

                return MOVIE_REPOS.DeleteRange(movieDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recebe uma lista de objetos Movie e Retorna uma lista de MovieResponse populado
        /// </summary>
        /// <param name="movie">lista de objetos Movie</param>
        /// <returns>Retorna uma lista de MovieResponse populado</returns>
        private List<MovieResponse> SetListMovieResponse(IEnumerable<Movie> listMovies)
        {
            var resp = new List<MovieResponse>();
            foreach (var movie in listMovies)
            {
                resp.Add(SetMovieResponse(movie));
            }
            return resp;
        }

        /// <summary>
        /// Recebe um objeto Movie e Retorna um MovieResponse populado
        /// </summary>
        /// <param name="movie">objeto Movie</param>
        /// <returns>Retorna um MovieResponse populado</returns>
        private MovieResponse SetMovieResponse(Movie movie)
        {
            var genreResp = new List<GenreResponse>();

            foreach (var GenM in movie.GenreMovies)
            {
                genreResp.Add(new GenreResponse() { 
                    ID = GenM.GenreId,
                    Name = GenM.Genre.Name
                });
            }

            var movieResp = new MovieResponse
            {
                ID = movie.ID,
                Name = movie.Name,
                Create_at = movie.Create_at,
                Active = (int)movie.eActive,
                GenreList = genreResp
            };

            return movieResp;
        }

    }
}
