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
  internal class Ayt_Cadiz_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_epediente = new PdfRectangle(120.75, 438.75, 202.5, 451.875);
      var expediente = GetDato(rec_epediente);
      return expediente;
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(460.5, 426, 535.5, 437.25);
      var fecha = GetDato(rec_fecha);
      if(DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
     throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(514.875, 537.75, 556.125, 551.625);
      var importe = GetDato(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(60, 412.5, 291, 426.375);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(314.625, 425.25, 421.5, 437.625);
      var marca = GetDato(rec_marca).Split(" ")[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(237.75, 426, 291, 438.75);
      return GetDato(rec_matricula).Replace(" ", "");
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(314.625, 425.25, 421.5, 437.625);
      var marca = string.Join(" ", GetDato(rec_marca).Split(" ").Skip(1));
      return marca;

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(38.625, 359.25, 339, 400.5);
      return GetDato(rec_motivo).Replace("Hecho denunciado:", "");
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(94.875, 793.5, 259.875, 811.125);
      var match = GetDato(rec_match);
      return match == "EXCMO. AYUNTAMIENTO DE";
    }
  }
}
