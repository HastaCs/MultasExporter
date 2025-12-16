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
  internal class Ayt_Fuengirola3_Texto : ExtractorBaseTexto, IExtractorTexto
  {

    public int PaginaDatos() { return 2; }
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoFuengirola.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(227.915769, 762.400386, 315.383443, 783.897578);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(227.415953, 742.902934, 308.885844, 762.400386);
      var fecha = GetDatos(rec_fecha);
      if(DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(471.325811, 716.406396, 546.797918, 732.90424);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if(decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(109.459547, 704.407964, 280.396373, 720.905808);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(228.415584, 728.404828, 382.858506, 744.902673);
      return GetDatos(rec_marca).Split("-")[0].Trim();
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(84.968598, 728.904763, 166.938304, 744.902673);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(228.415584, 728.404828, 382.858506, 744.902673);
      return string.Join(" ",GetDatos(rec_marca).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(127.452897, 693.409401, 429.341327, 706.907637);
      return GetDatos(rec_motivo);
    }
    //Algunos de Fuengirola el n. expediente ocupa dos lineas y esta un peli desplazado
    public bool Match()
    {
      var rec_match1 = new PdfRectangle(468.326919, 758.400909, 565.291084, 785.897316);
      var rec_match2 = new PdfRectangle(85.468414, 803.894965, 195.927591, 820.892744);
   
      var match = GetDatos(rec_match1);
           var match2 = GetDatos(rec_match2);
      return match == "Ayuntamiento de Fuengirola" && match2=="NOTIFICACIÓN";
    }
  }
}
