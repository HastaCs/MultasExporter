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
  internal class Dip_Malaga_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(260.697999, 639.860021, 363.945984, 653.683996);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.DipMalaga.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(408, 801.5, 562.5, 817.253);
      return GetDato(rec_expediente).Replace(" ","");

    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(416.937012, 699.282996, 491.587983, 718.909002);
      var rec_hora = new PdfRectangle(491.587983, 699.282996, 567.562012, 718.909021);
      var fecha = GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      return Convert.ToDateTime(fecha + " " + hora);
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(22.118001, 612.212011, 138.505997, 626.035985);
      var importe = GetDato(rec_importe);
      return Convert.ToDecimal(importe.Split(" ")[0]);

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(22.118001, 699.282996, 420.5, 718.909002);
      return GetDato(rec_lugar);

    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(363.945008, 639.860021, 502.092011, 653.683996);
      var marca = GetDato(rec_marca);
      return marca.Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(186.998993, 639.860021, 260.697999, 653.683996);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(363.945008, 639.860021, 502.092011, 653.683996);
      var marca = GetDato(rec_marca);
      return string.Join(" ", marca.Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(22.118001, 667.507996, 567.598023, 686.494009);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(397, 814, 572, 834);
      var match = GetDato(rec_match);
      return match == "DIPUTACIÓN PROVINCIAL DE MÁLAGA";
    }
  }
}
