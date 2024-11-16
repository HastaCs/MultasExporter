using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{
  public abstract class ExtractorBase
  {
    //TODO Meter aqui el setfichero, y el getdato y el _imagenMulta
    private byte[] _multaImagen { get; set; }
    protected string GetDato(Rect rect)
    {
      var image = Pix.LoadFromMemory(_multaImagen);
     
      using (var engine = new TesseractEngine("", "spa", EngineMode.TesseractAndLstm))
      using (var page = engine.Process(image, rect))
      {
       
        // Obtener el texto extraído
        return page.GetText().Trim().Trim('\n');
      }
    }
    protected void SetFichero(byte[] imagen)
    {
      _multaImagen = imagen;
    }

  }
}
