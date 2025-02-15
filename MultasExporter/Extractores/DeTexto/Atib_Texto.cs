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
  internal class Atib_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Atib.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(183, 750.375, 267, 766.125);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(456.75, 752, 506.5, 766);
      var rec_hora = new PdfRectangle(510.25, 728.75, 550.75, 746.75);
      var fecha = GetDatos(rec_fecha) + " " + GetDatos(rec_hora);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(427.125, 681.5625, 487.125, 693.75);
      var importe = GetDatos(rec_importe).Replace(".", ",");

      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(330.5, 726.5, 508.75, 746.5);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_modelo = new PdfRectangle(302.25, 752.625, 399, 769.875);
      var modelo = GetDatos(rec_modelo).Split(" ")[0];
      return modelo;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(405, 750.75, 454.5, 766.5);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(302.25, 752.625, 399, 769.875);
      return string.Join(" ", GetDatos(rec_modelo).Split(" ").Skip(1));

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(120.5, 604, 542.574456, 640.5);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {

      var rec_match = new PdfRectangle(296.87375, 304.233459, 550.285463, 414.991261);
      var match = GetDatos(rec_match);
      return match.Contains("www.atib.es");
    }
  }
}
