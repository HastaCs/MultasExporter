using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{
  /// <summary>
  /// Necesario en todos los extractores, para usar el GetDato y el SetPix
  /// </summary>
  public abstract class ExtractorBase
  {
    
    private Pix _pix { get; set; }
    // private TesseractEngine _engine { get; set; }
    protected string GetDato(Rect rect)
    {
      using (var engine = new TesseractEngine("", "spa", EngineMode.Default))
      {
        engine.SetVariable("tessedit_char_blacklist", "€");
        using (var page = engine.Process(_pix, rect))
        {

          // Obtener el texto extraído
          return page.GetText().Trim().Trim('\n');
        }
      }
      
    }
    protected void SetPix(Pix pixImagen)
    {
      Console.WriteLine("SETFICHERO a " + DateTime.Now);
      _pix = pixImagen;
      // _engine = new TesseractEngine("", "spa", EngineMode.Default);
      Console.WriteLine("SETFICHERO b" + DateTime.Now);
    }

  }
}
