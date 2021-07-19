using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    /// <summary>
    /// Repositorio gênero
    /// </summary>
    public class GenreRepository
    {
        private DataContextMySql _DbContext;
        public GenreRepository(DataContextMySql dataContextMySql)
        {
            _DbContext = dataContextMySql;
        }

                
        public IEnumerable<Genre> GetAll()
        {
            return _DbContext.Genres.ToList();
        }

        public IEnumerable<Genre> GetByParam(Func<Genre, bool> predicate)
        {
            return _DbContext.Genres.AsNoTracking()
                .Include(x => x.GenreMovies).Where(predicate).ToList();
        }

        public Genre Insert(Genre entity)
        {
            try
            {
                _DbContext.Genres.Add(entity);
                _DbContext.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Genre> InsertRange(IEnumerable<Genre> entity)
        {
            try
            {
                _DbContext.Genres.AddRange(entity);
                _DbContext.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Genre Update(Genre entity)
        {
            try
            {
                _DbContext.Genres.Update(entity);
                _DbContext.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(Genre entity)
        {
            try
            {
                _DbContext.Genres.Remove(entity);
                _DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Genre> UpdateRange(IEnumerable<Genre> entity)
        {
            try
            {
                _DbContext.Genres.UpdateRange(entity);
                _DbContext.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteRange(IEnumerable<Genre> entity)
        {
            try
            {
                _DbContext.Genres.RemoveRange(entity);
                _DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
