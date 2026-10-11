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
{/// <summary>
/// Ayto Lebrija
/// </summary>
  internal class Dip_Sevilla2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipSevilla.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(319.926384, 549.770288, 394.564194, 566.238869);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechamulta = new PdfRectangle(203.916769, 732.611013, 251.618728, 742.078153);
      var rec_horamulta = new PdfRectangle(199.797124, 726.25, 226.743269, 734.625);
      if (DateTime.TryParse(GetDatos(rec_fechamulta) + " " + GetDatos(rec_horamulta), out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(216.75, 660.5, 249.75, 672.5);
      var datos = GetDatos(rec_importe);
      datos = datos.Replace("€", "").Replace(".", "");
      if (decimal.TryParse(datos, out decimal importe))
        return importe;
      throw new Exception("No se pudo recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(170.75, 740.25, 449, 749.25);

      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(180.75, 711.75, 347.75, 722.25);
      var marca = GetDatos(rec_marcamodelo);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(184.75, 719, 227.5, 728.25);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(180.75, 711.75, 347.75, 722.25);
      var marca = GetDatos(rec_marcamodelo);
      return string.Join(" ", marca.Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(214, 683.67711, 516.054208, 693.240339);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(61.903305, 611.722913, 140.556916, 636.119006);
      var rec_match2 = new PdfRectangle(144.926561, 554.920072, 261.086292, 576.403198);

      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);


      return match1 == "Agencia Tributaria de Sevilla" && match2.Contains("SEVILLA");
    }
  }
}
