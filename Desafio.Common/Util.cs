using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Common
{
    public static class Util
    {
        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text">Texto a separar apenas os numericos</param>
        /// <returns>Numeros do texto</returns>
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }
    }
}
