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
  internal class Dip_Malaga2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {

      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipMalaga.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(37.492124, 690.647348, 112.476371, 703.770397);
      return GetDatos(rec_expediente).Replace(" ", "");

    }

    public DateTime GetFechaMulta()
    {
      var rec_fechaHora = new PdfRectangle(39.36673, 462.306287, 122.224323, 474.304503);
      var fechaHoraTexto = GetDatos(rec_fechaHora);
      if (DateTime.TryParse(fechaHoraTexto, out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(214.829868, 436.060188, 287.564588, 447.308516);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (Decimal.TryParse(importe.Split(" ")[0], out decimal importeRes))
        return importeRes;
      return 0; throw new Exception("No se pudo recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(400.040958, 454.432457, 553.383743, 466.055729);
      return GetDatos(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(400.040958, 462.681231, 548.884689, 475.429336);
      var marca = GetDatos(rec_marca);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(315.68368, 463.056175, 386.543794, 475.054392);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(400.040958, 462.681231, 548.884689, 475.429336);
      var marca = GetDatos(rec_marca);
      return string.Join(" ", marca.Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(37.867045, 409.814089, 548.134846, 428.936247);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(296.937619, 776.134642, 538.011973, 806.880072);
      var match = GetDatos(rec_match);
      var rec_importe = new PdfRectangle(214.829868, 436.060188, 287.564588, 447.308516);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (Decimal.TryParse(importe.Split(" ")[0], out decimal importeRes))
        return match == "PATRONATO DE RECAUDACIÓN PROVINCIAL DE MÁLAGA";
      return false;
    }
  }
}
