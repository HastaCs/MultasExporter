using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class Suma_Alicante_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(176.571613, 489.970914, 444.615567, 503.47356);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.SumaAlicante.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(425.496348, 522.852455, 521.092444, 536.730174);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var fechaMulta = new PdfRectangle(328.400706, 512.72547, 397.004963, 525.477969);
      var horaMulta = new PdfRectangle(424.371688, 514.225764, 476.480932, 526.228116);

      return Convert.ToDateTime(GetDato(fechaMulta) + " " + GetDato(horaMulta));
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(200.939245, 338.691368, 284.913854, 351.068793);
      return Convert.ToDecimal(GetDato(rec_importe));
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(108.342242, 501.848339, 522.217104, 514.975911);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(124.087482, 483.469738, 520.342671, 494.721943);
      return GetDato(rec_marca).Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(122.962822, 491.721355, 177.321386, 504.09878);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(124.087482, 483.469738, 520.342671, 494.721943);
      return string.Join(" ",GetDato(rec_marca).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(71.228464, 381.074673, 518.468237, 464.340989);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(319.02854, 803.782504, 391.006776, 818.035297);
      var match = GetDato(rec_match);
      return match == "Modelo";

    }

    public void SetPalabras(IEnumerable<Word> palabras)
    {
      base.Set(palabras);
    }
  }
}
