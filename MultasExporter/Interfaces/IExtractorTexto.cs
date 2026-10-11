using MultasExporter.Extractores.DeWords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultasExporter.Interfaces
{
  internal interface IExtractorTexto:IDatos,IFromTexto
  {
    /// <summary>
    /// La pagina del pdf donde se encuentran los datos
    /// </summary>
    public int PaginaDatos() { return 1; }
  }
}
