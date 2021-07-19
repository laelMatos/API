using Desafio.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class RentRequest
    {
        public int ID { get; set; }
        /// <summary>
        /// CPF do locatario
        /// </summary>
        [MinLength(11, ErrorMessage = "O CPF deve possuir ao menos 11 números.")]
        [MaxLength(14, ErrorMessage = "O CPF deve possuir no máximo 14 caracteres.")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        /// <summary>
        /// Data de locação.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não foi preenchido a data de locação.")]
        [DataType(DataType.DateTime, ErrorMessage = "A data de locação deve ser válida.")]
        [CustomValidationStringDate(ErrorMessage ="A data informa é inválida.")]
        public string DateRent { get; set; }

        /// <summary>
        /// Lista de filmes locados
        /// </summary>
        [Required(ErrorMessage ="Informe ao menos um filme para locação")]
        public ICollection<int> ListMovie { get; set; }
    }
}
