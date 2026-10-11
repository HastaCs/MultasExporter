using MultasExporter.Extractores.DeWords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;

namespace MultasExporter.Interfaces
{
  public interface IFromTexto
  {

    void SetPalabras(IEnumerable<Word> palabras)
    {
      // Implementación por defecto
      ((ExtractorBaseTexto)this).Set(palabras);
    }

  }
}
