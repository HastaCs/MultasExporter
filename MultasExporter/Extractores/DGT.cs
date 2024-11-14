using MultasExporter.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MultasExporter.Extractores
{
  public class DGT : IMulta
  {
    private byte[] _multaImagen { get; set; }
    public bool Match(byte[] multaFichero)
    {
      //TODO hacer comprobaciones de que es multa de DGT
      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageBytes"></param>
    public void SetFicheroMulta(byte[] imagenMulta)
    {
      _multaImagen = imagenMulta;
    }

    private string GetDato(Rect rect)
    {
      var image = Pix.LoadFromMemory(_multaImagen);
      using (var engine = new TesseractEngine("", "spa", EngineMode.Default))
      using (var page = engine.Process(image, rect))
      {
        // Obtener el texto extraído
        return page.GetText().Trim().Trim('\n');
      }
    }

    public string GetMatricula()
    {
      var rectMulta = new Rect(794, 762, 194, 42);
      return GetDato(rectMulta);
    }



    public string GetExpediente()
    {
      var recExpediente = new Rect(500, 544, 358, 62);
      return GetDato(recExpediente);
    }
    public DateTime GetFechaMulta()
    {
      var recFechaMulta = new Rect(1894, 534, 346, 88);
      var fecha = DateTime.ParseExact(GetDato(recFechaMulta), "dd/MM/yyyy - HH:mm'h'", CultureInfo.InvariantCulture);
      return fecha;
    }

    public EmisorDTO GetEmisor()
    {
      return new EmisorDTO
      {
        Id = Emisor.Dgt,
        Nombre = "DGT"
      };
    }

    public string GetMotivoMulta()
    {
      return GetDato(new Rect(156, 1790, 2084, 176));
    }

    public string GetLugarMulta()
    {
      var lugar = GetDato(new Rect(146, 1036, 992, 166));
      return lugar;
    }

    public string GetMarcaVehiculo()
    {
      var marca = GetDato(new Rect(338,846,650,48));
      return marca;
    }

    public string GetClaseVehiculo()
    {
      var clase = GetDato(new Rect(320,804,672,48));
      return clase;
    }

    public string GetModeloVehiculo()
    {
      var modelo = GetDato(new Rect(362,886,628,58));
      return modelo;
    }

    public decimal GetImporte()
    {
      var importe = GetDato(new Rect(750, 621, 218, 50));
      decimal result = decimal.Parse(importe, System.Globalization.CultureInfo.InvariantCulture);


      return result;
    }
  }
}
