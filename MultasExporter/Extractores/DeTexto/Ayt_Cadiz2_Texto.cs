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
  internal class Ayt_Cadiz2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoCadiz.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(168.206317, 410.384864, 232.648996, 425.314571);
      var expediente = GetDatos(rec_expediente);
      return expediente;
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(442.360767, 399.460688, 511.900608, 412.20556);

      var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(482.045807, 494.501016, 512.264691, 507.245888);

      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(119.783286, 388.900652, 305.101501, 402.737941);

      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(325.490145, 399.460688, 413.962298, 410.384864);
      var marca = GetDatos(rec_marca).Split(" ")[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(262.503797, 400.917245, 307.650081, 412.933838);
      return GetDatos(rec_matricula).Replace(" ", "");
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(325.490145, 399.460688, 413.962298, 410.384864);
      var marca = string.Join(" ", GetDatos(rec_marca).Split(" ").Skip(1));
      return marca;

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(100.850974, 348.845341, 341.873877, 379.797172);
      return GetDatos(rec_motivo).Replace("Hecho denunciado:", "");
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(146.725424, 702.788631, 291.266349, 724.272843);
      var match = GetDatos(rec_match);
      return match.Contains("EXCMO. AYUNTAMIENTO DE CÁDIZ");
    }
  }
}
