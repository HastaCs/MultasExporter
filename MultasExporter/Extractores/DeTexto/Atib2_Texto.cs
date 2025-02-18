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
  internal class Atib2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Atib.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(191.25, 719.25, 311.25, 736.875);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(461.625, 719.625, 511.5, 739.125);
      var rec_hora = new PdfRectangle(516, 695.625, 550.125, 714);
      var fecha = GetDatos(rec_fecha) + " " + GetDatos(rec_hora);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(389.625, 778.5, 432.375, 794.625);
      var importe = GetDatos(rec_importe).Replace(".",",").Replace("€","").Trim();
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(340.5, 694.5, 515.625, 714);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_modelo = new PdfRectangle(175.5, 513, 325.5, 526.5);
      var marca = GetDatos(rec_modelo).Split(" ")[0];
      //a veces se repite dos veces la marca en el string, cuando pase quiero eliminar lo repetido, por ejemplo FIATFIAT, MERCEDESMERCEDES, etc
      if (marca.Length > 3 && marca.Substring(0, marca.Length / 2) == marca.Substring(marca.Length / 2))
        return marca.Substring(0, marca.Length / 2);
      return marca;


    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(412.125, 719.625, 461.625, 744);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(175.5, 513, 325.5, 526.5);
      var modelo = GetDatos(rec_modelo).Split(" ").Skip(1);
      return string.Join(" ", modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(121.125, 321, 355.875, 383.25);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(108.375, 587.25, 307.125, 654.75);
      var rec_match2 = new PdfRectangle(353.25, 613.125, 414.75, 625.5);
      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);

      return match1.Contains("www.atib.es") && match2.Contains("www.atib.es");

    }
  }
}
