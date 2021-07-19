using Desafio.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class MovieRepository
    {
        private DataContextMySql _DbContext;
        public MovieRepository(DataContextMySql dataContextMySql)
        {
            _DbContext = dataContextMySql;
        }


        public IEnumerable<Movie> GetAll()
        {
            return _DbContext.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            var result = _DbContext.Movies.AsNoTracking()
                .Include(x => x.GenreMovies)
                .Include(x => x.RentMovies)
                .Where(x => x.ID == id)
                .FirstOrDefault();

            return result;
        }

        public IEnumerable<Movie> GetByParam(Func<Movie, bool> predicate)
        {
            return _DbContext.Movies.AsNoTracking()
                .Include(x => x.GenreMovies)
                .Include(x => x.RentMovies)
                .Where(predicate).ToList();
        }

        public Movie Insert(Movie entity)
        {
            _DbContext.Movies.Add(entity);
            _DbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Movie> InsertRange(IEnumerable<Movie> entity)
        {
            _DbContext.Movies.AddRange(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public Movie Update(Movie entity)
        {
            _DbContext.Movies.Update(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public bool Delete(Movie entity)
        {
            try
            {
                _DbContext.Movies.Remove(entity);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool DeleteRange(IEnumerable<Movie> entity)
        {
            try
            {
                _DbContext.Movies.RemoveRange(entity);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Movie> GetByGenre(IEnumerable<int> genres, bool? active = true)
        {
            string genreSearch = "";

            foreach (var genre in genres.Select((x, i) => new { Index = i, Value = x }))
            {
                genreSearch += $"[g].[GenreId] = {genre.Value} ";
                if (genre.Index < genres.Count()-1)
                    genreSearch += "OR ";
            }
            var status = (bool)active ? 1 : 0;
            string query = $"SELECT [f].[ID], [f].[Active], [f].[Create_at], [f].[Name], [f].[Update_at] "+
                            $"FROM [Filmes] AS[f] " +
                            $"INNER JOIN[GenreMovies] AS[g] ON[f].[ID] = [g].[MovieId] AND " + 
                            genreSearch +
                            $" AND [f].[Active] = {status} "+
                            $"LEFT JOIN[RentMovies] AS[r] ON[f].[ID] = [r].[MovieId] " +
                            $"ORDER BY[f].[ID], [g].[GenreId], [g].[MovieId], [r].[RentId], [r].[MovieId]";

            return  _DbContext.Movies.FromSqlRaw(query).ToList();
        }
    }
}
