using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Madrid_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";

    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoMadrid.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(21.600001, 682.770458, 149.040006, 709.983588);
      var expediente = GetDatos(rec_expediente);
      return expediente;
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(297.6, 779.411958, 435.146219, 816.045018);
      if (DateTime.TryParse(GetDatos(rec_fecha).Replace("DE APERTURA DEL EXPEDIENTE", ""), out DateTime fecha))
        return fecha;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(296.599664, 700.912544, 392.631933, 731.614537);
      if (decimal.TryParse(GetDatos(rec_importe), out decimal importe))
        return importe;
      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(25.920001, 713.123564, 276, 742.581126);
      var lugar = GetDatos(rec_lugar);
      return lugar;
    }

    public string GetMarcaVehiculo()
    {
      return "";
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(26.400002, 742.081126, 276.720002, 775.225323);
      var mat = GetDatos(rec_matricula).Split(" ")[0];
      return mat;
    }

    public string GetModeloVehiculo()
    {
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(26.508908, 625.901993, 276.092774, 682.421571);
      return GetDatos(rec_motivo);

    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(11.503866, 818.400025, 90.030253, 847.310191);
      var match = GetDatos(rec_match);
      var rec_match2 = new PdfRectangle(74.344696, 843.224609, 446.953156, 876.865051);
      var match2 = GetDatos(rec_match2);
            var rec_match3 = new PdfRectangle(18.506219, 493.889362, 522.175463, 546.877492);
      var match3 = GetDatos(rec_match3);

      return match == "MADRID" && match2== "Denuncia por infracción de circulación. Multa" || match3.Contains("madrid.es");
    }
  }
}
