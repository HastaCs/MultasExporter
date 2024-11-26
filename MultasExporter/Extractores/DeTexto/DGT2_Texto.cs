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
  internal class DGT2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(188.958196, 599.625, 268, 612.75);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Dgt.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(465, 792, 548, 806);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(322.428667, 792, 384.664898, 806);
      var rec_hora = new PdfRectangle(399.66158, 792, 455.524221, 806);
      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(473.145322, 748, 548, 760.5);
      var importe = GetDato(rec_importe).Replace(".", ",").Replace("EUR", "");
      return Convert.ToDecimal(importe);

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(147, 723, 465, 748);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(187.833444, 588, 268, 601.875);
      return GetDato(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(188.208361, 611, 253.818846, 628.5);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(188.208361, 577.125, 268, 590.5);
      return GetDato(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(143.843066, 653, 551.752821, 714.625);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(185.583942, 780.375, 289, 810);
      var match = GetDato(rec_match);
      return match == "Centro de Tratamiento de Denuncias Automatizadas";
    }
  }
}
