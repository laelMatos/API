using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class UserResponse
    {
        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
    }

    public class UserInsertResponse
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Tipo (role) do usuário
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }

    public class UserGetResponse
    {
        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Tipo (role) do usuário
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}
