using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class GenreRequest
    {
        /// <summary>
        /// Codigo de identificação do gênero
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do gênoro
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do filme")]
        [MaxLength(200, ErrorMessage = "O nome deve possuir no máximo 200 caracteres.")]
        public string Name { get; set; }
        
        /// <summary>
        /// Status do gênero
        /// </summary>
        [Required(ErrorMessage ="Deve ser informado um status para o gênero")]
        public bool Status { get; set; }
    }
}
