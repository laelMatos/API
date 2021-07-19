using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    /// <summary>
    /// Entidade de gênero
    /// </summary>
    public class GenreResponse
    {
        /// <summary>
        /// Codigo de identificação do gênero
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Nome do gênero
        /// </summary>
        public string Name { get; set; }
    }
}
