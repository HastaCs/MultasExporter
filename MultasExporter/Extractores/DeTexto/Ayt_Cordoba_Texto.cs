using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{//TODO: Mirar mas ejemplos de multas de cordoba
  internal class Ayt_Cordoba_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoCordoba.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(37.135917, 682.520288, 94.902897, 696.770711);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechaHora = new PdfRectangle(37.511027, 447.76331, 114.783741, 463.138767);
      var fecha = GetDatos(rec_fechaHora);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(181.553368, 392.26166, 241.94612, 407.262106);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(396.491549, 404.262017, 550.286757, 445.138232);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca  = new PdfRectangle(396.491549, 448.888343, 553.662749, 467.263889);
      return GetDatos(rec_marca).Split(" ")[0];

    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(309.090857, 450.388388, 367.232948, 467.6389);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(396.491549, 448.888343, 553.662749, 467.263889);
      return string.Join(" ", GetDatos(rec_marca).Split(" ").Skip(1));

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(36.385696, 373.886114, 553.662749, 394.886738);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(31.134152, 552.39142, 318.843724, 572.642022);

      var match = GetDatos(rec_match);
      return match.Contains("Por el órgano competente para la incoación se ha dictado");
    }
  }
}
