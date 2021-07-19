using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private DataContextMySql _DbContext;
        public GenericRepository(DataContextMySql dataContextMySql)
        {
            _DbContext = dataContextMySql;
        }

        public IEnumerable<T> GetAll()
        {
            return _DbContext.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetByParam(Func<T, bool> predicate)
        {
            return _DbContext.Set<T>().Where(predicate);
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Set<T>().Add(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Set<T>().AddRange(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Set<T>().UpdateRange(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.SaveChanges();

            return entity;
        }

        public bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _DbContext.Set<T>().Remove(entity);
                _DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool DeleteRange(IEnumerable<T> entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _DbContext.Set<T>().RemoveRange(entity);
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
