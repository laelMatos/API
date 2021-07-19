using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class RentResponse
    {
        /// <summary>
        /// Código de identificação da locação
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// CPF do locatario
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Data de locação
        /// </summary>

        public DateTime DateRent { get; set; }
        /// <summary>
        /// Lista de filmes locados
        /// </summary>
        public IEnumerable<MovieResponse> Movies { get; set; }
    }
}
