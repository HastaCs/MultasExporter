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
  internal class Ayt_Murcia_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(104.625, 380.625, 192.75, 392.625);
      return GetDatos(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoMurcia.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(22.875, 381, 102, 392.625);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(349.875, 381, 421.5, 392.625);
      var rec_hora = new PdfRectangle(424.125, 380.625, 462.375, 393);
      var fecha = GetDatos(rec_fecha);
      var hora = GetDatos(rec_hora).Replace(".", ":");
      if (DateTime.TryParse(fecha + " " + hora, out DateTime fechaHora))
        return fechaHora;
     throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(510.75, 379.875, 562.5, 394.5);
      if (decimal.TryParse(GetDatos(rec_importe), out decimal importe))
        return importe;
      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(290, 360.25, 568.75, 374.75);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      return "";
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(199.298157, 381.215393, 261.180664, 394.407806);
      return GetDatos(rec_matricula);

    }

    public string GetModeloVehiculo()
    {
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(101.014099, 295.237213, 606.994812, 334.814484);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(59.625, 730.875, 215.625, 747.375);
      var match = GetDatos(rec_match);
      return match.Contains("AYUNTAMIENTO DE MURCIA");
    }
  }
}
