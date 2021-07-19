using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace Desafio.Common
{
    public class ResouceStringText
    {
        ResourceManager RESOURCE_MANAGER;

        public ResouceStringText(ResourceManager rs)
        {
            RESOURCE_MANAGER = new ResourceManager("Desafio.API.Resources.StringsTex", Assembly.GetExecutingAssembly());
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        }
        
    }
}
