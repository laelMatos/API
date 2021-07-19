using Desafio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class UserRepository
    {
        private DataContextMySql _DbContext;
        public UserRepository(DataContextMySql dataContextMySql)
        {
            _DbContext = dataContextMySql;
        }

        /// <summary>
        /// Pega o usuario que tenha o email igual ao informado
        /// </summary>
        /// <param name="email">Email do usuario desejado</param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return _DbContext.Users.Where(x=>x.Email == email).FirstOrDefault();
        }

        public User GetByEmailOrName(string email, string name)
        {
            return _DbContext.Users.Where(x => x.Email == email || x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Cria um usuario novo no banco de dados
        /// </summary>
        /// <param name="model">Dados do usuario a ser criado</param>
        /// <returns></returns>
        public User CreateUser(User model)
        {
            try
            {
                _DbContext.Users.Add(model);
                _DbContext.SaveChanges();

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
    }
}
