using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class RentRepository
    {

        private DataContextMySql _DbContext;
        public RentRepository(DataContextMySql dataContextMySql)
        {
            _DbContext = dataContextMySql;
        }


        public IEnumerable<Rent> GetAll()
        {
            return _DbContext.Rents.AsNoTracking()
                .Include(x=>x.RentMovieList).ToList();
        }

        public Rent GetById(int id)
        {
            return _DbContext.Rents.AsNoTracking().Where(x=>x.ID == id)
                .Include(x => x.RentMovieList).FirstOrDefault();
        }

        public IEnumerable<Rent> GetByParam(Func<Rent, bool> predicate)
        {
            return _DbContext.Rents.AsNoTracking()
                .Include(x=>x.RentMovieList).Where(predicate).ToList();
        }

        public Rent Insert(Rent entity)
        {
            _DbContext.Rents.Add(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<Rent> InsertRange(IEnumerable<Rent> entity)
        {
            _DbContext.Rents.AddRange(entity);
            _DbContext.SaveChanges();

            return entity;
        }

        public Rent Update(Rent entity)
        {
            var RentDb = _DbContext.Rents.Where(x=>x.ID == entity.ID).FirstOrDefault();

            if (RentDb == null)
                return null;

            RentDb.CPF = entity.CPF;

            _DbContext.Rents.Update(RentDb);
            _DbContext.SaveChanges();

            return entity;
        }
        
        public bool Delete(Rent entity)
        {
            try
            {
                _DbContext.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
