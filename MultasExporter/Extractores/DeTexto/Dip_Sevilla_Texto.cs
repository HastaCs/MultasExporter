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
  internal class Dip_Sevilla_Texto : ExtractorBaseTexto, IExtractorTexto
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
      var rec_fechamulta = new PdfRectangle(138.412839, 535.754474, 237.929918, 551.87266);
      var rec_horamulta = new PdfRectangle(320.276797, 535.754474, 451.330978, 551.171869);
      if (DateTime.TryParse(GetDatos(rec_fechamulta) + " " + GetDatos(rec_horamulta), out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(138.412839, 426.781524, 238.28033, 441.498128);
      var datos = GetDatos(rec_importe);
      datos = datos.Replace("€", "").Replace(".", "");
      if (decimal.TryParse(datos, out decimal importe))
        return importe;
      throw new Exception("No se pudo recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugarmulta = new PdfRectangle(138.062427, 566.238869, 563.112486, 581.305868);
      return GetDatos(rec_lugarmulta);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marcaclase = new PdfRectangle(392.111308, 521.388265, 546.993522, 535.404079);
      var marca = GetDatos(rec_marcaclase);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(138.763251, 521.03787, 219.358069, 536.455265);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcaclase = new PdfRectangle(392.111308, 521.388265, 546.993522, 535.404079);
      var marca = GetDatos(rec_marcaclase);
      return string.Join(" ", marca.Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(138.763251, 471.632127, 564.514135, 490.903871);

      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(143.951613, 768.182467, 461.844759, 810.192445);
      var rec_match2 = new PdfRectangle(130.756049, 668.558803, 203.931452, 686.56308);
      var rec_match3 = new PdfRectangle(476.23992, 733.974341, 593.200605, 752.57876);

      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      var match3 = GetDatos(rec_match3);

      return match1.Contains("NOTIFICACIÓN DE DENUNCIA") && match2.Contains("SEVILLA") && match3.Contains("Ayuntamiento");
    }
  }
}
