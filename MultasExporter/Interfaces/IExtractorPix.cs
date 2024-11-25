using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultasExporter.Interfaces
{/// <summary>
 /// Extrae los datos desde un Pix de Tesseract
 /// </summary>
  internal interface IExtractorPix : IDatos, IFromPix
  {
    /// <summary>
    /// La pagina del pdf donde se encuentran los datos
    /// </summary>
    public int PaginaDatos() { return 1; }
  }
}
