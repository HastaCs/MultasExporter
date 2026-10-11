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
  public class Ayt_Almeria_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoAlmeria.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(40.05508, 719.322093, 129.268666, 733.883269);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechamulta = new PdfRectangle(38.234395, 556.600952, 136.187271, 573.346304);
      var fecha = GetDatos(rec_fechamulta);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(142.377601, 539.49157, 242.879437, 553.324687);
      var importe = GetDatos(rec_importe).Replace("€", "").Trim();
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(325.174419, 548.956334, 559.678703, 562.425422);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(325.174419, 560.605275, 555.673195, 573.710333);
      var marcaModelo = GetDatos(rec_marcamodelo).Split(" ");
      return marcaModelo.Length > 0 ? marcaModelo[0] : "";
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(241.058752, 559.513187, 318.619952, 573.346304);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(325.174419, 560.605275, 555.673195, 573.710333);
      var marcaModelo = GetDatos(rec_marcamodelo).Split(" ");
      return marcaModelo.Length > 1 ? string.Join(" ", marcaModelo.Skip(1)) : "";

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(41.147491, 522.018159, 561.499388, 542.767834);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(222.123624, 770.650238, 428.589352, 789.943796);
      var texto = GetDatos(rec_match);
      return texto.Contains("AYUNTAMIENTO DE ALMERIA");
    }
  }
}
