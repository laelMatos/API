using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public class RentService : IRentService
    {
        /// <summary>
        /// Rpesitório Rent
        /// </summary>
        private readonly RentRepository RENT_REPOS;

        /// <summary>
        /// Repositório Movie
        /// </summary>
        private readonly IGenericRepository<Movie> MOVIE_REPOS;

        public RentService(RentRepository rentRepos, IGenericRepository<Movie> movieRepos)
        {
            RENT_REPOS = rentRepos;
            MOVIE_REPOS = movieRepos;
        }

        public IEnumerable<RentResponse> GetAll()
        {
            try
            {
                return RENT_REPOS.GetAll().Select(x => new RentResponse() { ID = x.ID, CPF = x.CPF });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RentResponse GetById(int id)
        {
            try
            {
                var rentDb =  RENT_REPOS.GetByParam(x => x.ID == id).FirstOrDefault();
                if (rentDb == null)
                    return null;

                var rentResp =  new RentResponse() { ID = rentDb.ID, CPF = rentDb.CPF, DateRent = rentDb.DateRent };
                rentResp.Movies = MOVIE_REPOS.GetByParam(x => rentDb.RentMovieList.Any(r => r.MovieId == x.ID)).Select(x=> 
                    new MovieResponse() { 
                        ID = x.ID, 
                        Name = x.Name, 
                        Active = (int)x.eActive, 
                        Create_at = x.Create_at });

                return rentResp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RentResponse Insert(RentRequest entity)
        {
            try
            {
                var rentEnsert = new Rent()
                {
                    ID = entity.ID,
                    CPF = entity.CPF,
                    DateRent = DateTime.Now
                };

                foreach (var mov in entity.ListMovie)
                {
                    rentEnsert.RentMovieList.Add(new RentMovie() { 
                     MovieId = mov
                    });
                }

                var rentDb = RENT_REPOS.Insert(rentEnsert);

                var moviesDb = MOVIE_REPOS.GetByParam(x => rentDb.RentMovieList.Any(g => g.MovieId == x.ID));

                foreach (var item in rentDb.RentMovieList)
                {
                    item.Movie = moviesDb.Where(g => g.ID == item.MovieId).First();
                }

                return SetRentResponse(rentDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RentResponse> InsertRange(IEnumerable<RentRequest> entity)
        {
            try
            {
                var newRentList = new List<Rent>();
                //criando uma lista de Movie com os dados de MovieRequest
                foreach (var rentRequest in entity)
                {
                    var rentEnsert = new Rent()
                    {
                        ID = rentRequest.ID,
                        CPF = rentRequest.CPF,
                        DateRent = DateTime.Now
                    };

                    foreach (var mov in rentRequest.ListMovie)
                    {
                        rentEnsert.RentMovieList.Add(new RentMovie() { MovieId = mov });
                    }

                    newRentList.Add(rentEnsert);
                }
                var rentDb = RENT_REPOS.InsertRange(newRentList);

                var movieDb = MOVIE_REPOS.GetByParam(x => rentDb.Any(m => m.RentMovieList.Any(g => g.MovieId == x.ID)));

                foreach (var mov in rentDb.Select(x => x.RentMovieList))
                {
                    foreach (var renMov in mov)
                    {
                        renMov.Movie = movieDb.Where(g => g.ID == renMov.MovieId).First();
                    }

                }

                return SetListRentResponse(rentDb);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RentResponse Update(RentRequest rentRequest)
        {
            try
            {
                var rentDb = RENT_REPOS.GetById(rentRequest.ID);

                if (rentDb == null)
                    throw new KeyNotFoundException();

                //Alterar o CPF deve ser uma ação restrita para usuarios especificos.
                rentDb.CPF = rentRequest.CPF;

                rentDb.DateRent = DateTime.Parse(rentRequest.DateRent);

                //Busca todos os videos relacionas à locação
                var genreMovDb = MOVIE_REPOS.GetByParam(x => x.ID == rentRequest.ID);

                //Filme a ser inserido
                //Validar a necessidade
                var rentMovInsert = rentRequest.ListMovie.Where(x => genreMovDb.Any(g => g.ID != x));
                //Filme a ser removido
                var rentMovRemove = genreMovDb.Where(x => rentRequest.ListMovie.Any(g => g != x.ID));
                MOVIE_REPOS.DeleteRange(rentMovRemove);

                //Montando o objeto RentMovie com a lista de ids recebido
                foreach (var item in rentRequest.ListMovie)
                {
                    rentDb.RentMovieList.Add(new RentMovie() { MovieId = item });
                }
                var result = RENT_REPOS.Update(rentDb);

                //Busca os filmes para popular RentResponse
                var genreDb = MOVIE_REPOS.GetByParam(x => rentDb.RentMovieList.Any(g => g.MovieId == x.ID));

                //Insere cada filme 
                foreach (var item in rentDb.RentMovieList)
                {
                    item.Movie = genreDb.Where(g => g.ID == item.MovieId).First();
                }

                return SetRentResponse(rentDb);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RentResponse> UpdateRange(IEnumerable<RentRequest> rentRequest)
        {
            try
            {
                throw new NotImplementedException();


                //var newRentList = RENT_REPOS.GetByParam(x=> rentRequest.Any(r=>r.ID == x.ID));

                //if (newRentList == null || newRentList.Count() == 0)
                //    throw new KeyNotFoundException();


                //foreach (var rentDb in newRentList)
                //{
                //    //Alterar o CPF deve ser uma ação restrita para usuarios especificos.
                //    rentDb.CPF = rentRequest.Where(x=>x.ID == rentDb.ID).First().CPF;

                //    rentDb.DateRent = DateTime.Parse(rentRequest.Where(x => x.ID == rentDb.ID).First().DateRent);
                //}

                ////Busca todos os videos relacionas à locação
                //var genreMovDb = MOVIE_REPOS.GetByParam(x => x.ID == rentRequest.ID);

                ////Filme a ser inserido
                ////Validar a necessidade
                //var rentMovInsert = rentRequest.ListMovie.Where(x => genreMovDb.Any(g => g.ID != x));
                ////Filme a ser removido
                //var rentMovRemove = genreMovDb.Where(x => rentRequest.ListMovie.Any(g => g != x.ID));
                //MOVIE_REPOS.DeleteRange(rentMovRemove);

                ////Montando o objeto RentMovie com a lista de ids recebido
                //foreach (var item in rentRequest.ListMovie)
                //{
                //    rentDb.RentMovieList.Add(new RentMovie() { MovieId = item });
                //}
                //var result = RENT_REPOS.Update(rentDb);

                ////Busca os filmes para popular RentResponse
                //var genreDb = MOVIE_REPOS.GetByParam(x => rentDb.RentMovieList.Any(g => g.MovieId == x.ID));

                ////Insere cada filme 
                //foreach (var item in rentDb.RentMovieList)
                //{
                //    item.Movie = genreDb.Where(g => g.ID == item.MovieId).First();
                //}

                //return SetRentResponse(rentDb);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(RentRequest entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(IEnumerable<RentRequest> entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recebe uma lista de objetos Rent e Retorna uma lista de RentResponse populado
        /// </summary>
        /// <param name="movie">lista de objetos Rent</param>
        /// <returns>Retorna uma lista de RentResponse populado</returns>
        private List<RentResponse> SetListRentResponse(IEnumerable<Rent> listRents)
        {
            var resp = new List<RentResponse>();
            foreach (var rent in listRents)
            {
                resp.Add(SetRentResponse(rent));
            }
            return resp;
        }

        /// <summary>
        /// Recebe um objeto Rent e Retorna um RentResponse populado
        /// </summary>
        /// <param name="rent">objeto Rent</param>
        /// <returns>Retorna um RentResponse populado</returns>
        private RentResponse SetRentResponse(Rent rent)
        {
            var RentMovieList = new List<MovieResponse>();

            foreach (var mov in rent.RentMovieList)
            {
                RentMovieList.Add(new MovieResponse()
                {
                    ID = mov.Movie.ID,
                    Name = mov.Movie.Name,
                    Create_at = mov.Movie.Create_at,
                    Active = (int)mov.Movie.eActive
                });
            }

            var rentResp = new RentResponse
            {
                ID = rent.ID,
                CPF = rent.CPF,
                DateRent = rent.DateRent,
                Movies = RentMovieList
            };

            return rentResp;
        }

    }
}
