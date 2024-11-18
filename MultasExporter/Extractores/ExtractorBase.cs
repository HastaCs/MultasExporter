using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{/// <summary>
/// Extra los datos desde el pdf transformandolo a imagen
/// </summary>
  public abstract class ExtractorBase
  {
    //TODO Meter aqui el setfichero, y el getdato y el _imagenMulta
    private Pix _pix { get; set; }
    // private TesseractEngine _engine { get; set; }
    protected string GetDato(Rect rect)
    {

      {
        Console.WriteLine("process");
        using (var engine = new TesseractEngine("", "spa", EngineMode.Default))

        using (var page = engine.Process(_pix, rect))
        {
          Console.WriteLine("process fin");
          // Obtener el texto extraído
          return page.GetText().Trim().Trim('\n');
        }
      }
    }
    protected void SetFichero(Pix pixImagen)
    {
      Console.WriteLine("SETFICHERO a " + DateTime.Now);
      _pix = pixImagen;
      // _engine = new TesseractEngine("", "spa", EngineMode.Default);
      Console.WriteLine("SETFICHERO b" + DateTime.Now);
    }

  }
}
