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
      return GetDatos(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Dgt.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(465, 792, 548, 806);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(322.428667, 792, 384.664898, 806);
      var rec_hora = new PdfRectangle(399.66158, 792, 455.524221, 806);
      var fecha = GetDatos(rec_fecha);
      var hora = GetDatos(rec_hora);
      if (DateTime.TryParse(fecha + " " + hora, out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(473.145322, 748, 548, 760.5);
      var importe = GetDatos(rec_importe).Replace(".", ",").Replace("EUR", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
   throw new Exception("No se pudo recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(147, 723, 465, 748);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(187.833444, 588, 268, 601.875);
      return GetDatos(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(188.208361, 611, 253.818846, 628.5);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(188.208361, 577.125, 268, 590.5);
      return GetDatos(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(143.843066, 653, 551.752821, 714.625);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(185.583942, 780.375, 289, 810);
      var match = GetDatos(rec_match);
      return match == "Centro de Tratamiento de Denuncias Automatizadas";
    }
  }
}
