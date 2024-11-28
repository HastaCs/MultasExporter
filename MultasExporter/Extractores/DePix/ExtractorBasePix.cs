using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores.DePix
{
  /// <summary>
  /// Necesario en todos los extractores, para usar el GetDato y el SetPix
  /// </summary>
  public abstract class ExtractorBasePix
  {

    private Pix _pix { get; set; }
    // private TesseractEngine _engine { get; set; }
    protected string GetDato(Rect rect)
    {
      try
      {
        var tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
        using (var engine = new TesseractEngine(tessDataPath, "spa", EngineMode.Default))
        {
          engine.SetVariable("tessedit_char_blacklist", "€");
          using (var page = engine.Process(_pix, rect))
          {
            // Obtener el texto extraído
            return page.GetText().Trim().Trim('\n');
          }
        }
      }
      catch (Exception)
      {
        return "";
      }
    }
    protected void SetPix(Pix pixImagen)
    {
      _pix = pixImagen;
    }


  }
}
