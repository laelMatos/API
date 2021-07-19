using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Model;
using Desafio.Repository;

namespace Desafio.API.Service
{
    public class AuthService
    {
        private UserRepository _userRpos;
        public AuthService(UserRepository dataContextMySql)
        {
            _userRpos = dataContextMySql;
        }

        public UserResponse ValidAuthentication(string email, string senha)
        {
            var usrModel = new UserResponse();

            var usuario = _userRpos.GetByEmail(email);

            if (usuario == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(senha, usuario.Password))
            {
                usrModel.Email = usuario.Email;
                usrModel.Nome = usuario.Name;
                usrModel.ID = usuario.ID;

                usuario.Last_acess = DateTime.Now;


                return usrModel;
            }

            return null;

        }
    }

}
