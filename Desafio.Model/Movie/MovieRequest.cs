using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class MovieRequest
    {
        public int ID { get; set; }
        /// <summary>
        /// Nome do filme
        /// </summary>
        [Required(ErrorMessage = "Informe o nome do filme")]
        [MaxLength(200, ErrorMessage = "O nome deve possuir no máximo 200 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// Genero do filme
        /// </summary>
        [Required(ErrorMessage ="informe ao menos um gênero")]
        public ICollection<int> Genre { get; set; }

        /// <summary>
        /// status do filme
        /// </summary>
        [Required(ErrorMessage = "É obrigatorio a definição do status do filme")]
        [Range(0,1, ErrorMessage ="O valor pode ser apenas 1 ou 0 (ativo/inativo)")]
        public int Active { get; set; }
    }
}
