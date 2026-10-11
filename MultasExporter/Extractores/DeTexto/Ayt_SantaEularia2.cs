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
  internal class Ayt_SantaEularia2 : ExtractorBaseTexto, IExtractorTexto
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
      var rec_expediente = new PdfRectangle(53.625, 718.875, 97.875, 728.625);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(84.75, 650.625, 130.5, 663);
      var rec_hora = new PdfRectangle(186.375, 651, 218.25, 663);

      var fecha = GetDatos(rec_fecha);
      var hora = GetDatos(rec_hora);
      if (DateTime.TryParse($"{fecha} {hora}", out DateTime fechaRes))
        return fechaRes;
      return DateTime.MinValue;
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(170.625, 555.375, 216.75, 566.25);
      var importe = GetDatos(rec_importe);
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(82.125, 661.125, 496.875, 675.75);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(369.75, 652.125, 559.5, 667.875);
      var marcaModelo = GetDatos(rec_marcamodelo);
      var marca = marcaModelo.Split(' ')[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(307.875, 651.75, 363.75, 664.125);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(369.75, 652.125, 559.5, 667.875);
      var marcaModelo = GetDatos(rec_marcamodelo);
      var marcaModeloArray = marcaModelo.Split(' ');
      if (marcaModeloArray.Length > 1)
        return marcaModeloArray.Skip(1).First();
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(112.125, 672, 567.375, 692.25);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(512.217407, 763.363037, 579.606812, 773.776428);
      var rec_match2 = new PdfRectangle(35.285446, 662.121216, 57.266544, 675.138);
      var rec_match3 = new PdfRectangle(222.992432, 475.547119, 300.793945, 485.671295);
      var rec_match4 = new PdfRectangle(34.41777, 603.979553, 65.654068, 615.839294);

      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      var match3 = GetDatos(rec_match3);
      var match4 = GetDatos(rec_match4);

      return match1 == "SANTA EULÀRIA DES RIU" && match2 == "LLOC" && match3 == "www.santaeularia.com" &&
             match4 == "NIF/CIF:";
    }
  }
}