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
  internal class Ayt_SantaEularia : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoSantaEularia.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(258.375, 658.125, 327.375, 669.375);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(322.125, 668.25, 379.125, 681.375);
      var rec_hora = new PdfRectangle(424.5, 667.875, 469.875, 681.375);

      var fecha = GetDatos(rec_fecha);
      var hora = GetDatos(rec_hora);
      if (DateTime.TryParse($"{fecha} {hora}", out DateTime fechaRes))
        return fechaRes;
      return DateTime.MinValue;
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(169.125, 564.375, 204, 579.375);
      var importe = GetDatos(rec_importe);
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(84.375, 667.875, 274.5, 682.5);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(153.375, 630.375, 425.625, 647.625);
      var marcaModelo = GetDatos(rec_marcamodelo);
      var marca = marcaModelo.Split(' ')[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(110.25, 631.875, 153.75, 646.875);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(153.375, 630.375, 425.625, 647.625);
      var marcaModelo = GetDatos(rec_marcamodelo);
      var marcaModeloArray = marcaModelo.Split(' ');
      if (marcaModeloArray.Length > 1)
        return marcaModeloArray.Skip(1).First();
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(100.125, 697.875, 575.25, 710.25);
            return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var contains1 = "SANTA EULÀRIA DES RIU";
      var rec_match = new PdfRectangle(491.25, 756, 578.25, 782.25);
      var texto = GetDatos(rec_match);
      return texto.Contains(contains1);
    }
  }
}
