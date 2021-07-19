using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class UserRequest
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; set; }
    }

    public class UserInsertRequest
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome deve ser preenchido.")]
        [MaxLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres.")]
        public string Name { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "O parâmetro email deve ser preenchido.")]
        [MaxLength(140, ErrorMessage = "O parâmetro name deve possuir no máximo 140 caracteres.")]
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "O parâmetro password deve ser preenchido.")]
        public string Password { get; set; }
        /// <summary>
        /// Tipo do usuário <br/>
        /// Utilize o valor 'client' caso seja um novo usuário
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "O parâmetro type deve ser preenchido.")]
        public int Type { get; set; }
    }
}
