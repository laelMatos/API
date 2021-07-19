using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public class UserService
    {
        private UserRepository USER_REPOS;
        public UserService(UserRepository userRepos)
        {
            USER_REPOS = userRepos;
        }

        public object Insert(UserInsertRequest model)
        {
            //var userRepos = new UserRepository(this.userRepos);

            var resultado = new List<ResultResponse>();

            var userDb = USER_REPOS.GetByEmailOrName(model.Email, model.Name);

            if(userDb != null)
            {
                if (userDb.Name == model.Name)
                    resultado.Add(new ResultResponse() {
                        Message= "O Nome informado está sendo utilizado por outro usuário." ,
                        ErrorField="Name",
                        Success=false
                    });
                if(userDb.Email == model.Email)
                    resultado.Add(new ResultResponse()
                    {
                        Message = "O email informado está sendo utilizado por outro usuário.",
                        ErrorField = "Email",
                        Success = false
                    });

                return resultado;
            }

            try
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Type = model.Type,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password, workFactor: 10),
                    Created_at = DateTime.Now
                };

                userDb = USER_REPOS.CreateUser(user);

                UserResponse userResp = new UserResponse()
                {
                    ID = userDb.ID,
                    Nome = userDb.Name,
                    Email = userDb.Email
                };

                return userResp;
            }
            catch (Exception)
            {
                throw new Exception("Falha ao inserir o usuario.");
            }  
        }
    }
}
