using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Common.Exceptions
{
    /// <summary>
    /// Erro de contrato vinculado
    /// </summary>
    [Serializable]
    public class BoundContractException : Exception
    {
        /// <summary>
        /// Erro de contrato vinculado
        /// </summary>
        public BoundContractException()
        {

        }
        /// <summary>
        /// Erro de contrato vinculado
        /// </summary>
        public BoundContractException(string message)
            : base(message)
        {

        }
    }
}
