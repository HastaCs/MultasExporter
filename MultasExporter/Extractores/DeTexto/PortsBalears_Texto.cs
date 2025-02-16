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
  internal class PortsBalears_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
     return Emisor.PortsBalears.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(100.478891, 688.602228, 189.710145, 701.729176);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechaHora = new PdfRectangle(250.447385, 663.473497, 319.807814, 675.850335);
      var fecha = GetDatos(rec_fechaHora);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(381.294897, 528.828508, 445.406428, 552.832072);
            var importe = GetDatos(rec_importe).Replace(".", ",").Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(100.10397, 663.473497, 251.197228, 676.600446);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(152.592943, 625.217818, 320.182735, 640.220045);
      return GetDatos(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(152.218022, 637.2196, 302.561437, 651.09666);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      return "";
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(249.322622, 567.084188, 532.013233, 582.46147);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(165.715186, 403.559911, 269.193447, 421.562584);
      var match = GetDatos(rec_match);
      return match.Contains("Autoritat Portuària de Balears");
    }
  }
}
