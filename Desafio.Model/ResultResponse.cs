using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    public class ResultResponse
    {
        /// <summary>
        /// Indica sucesso ou não
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Mensagem do resultado
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Campo com erro
        /// </summary>
        public string ErrorField { get; set; }
    }
}
