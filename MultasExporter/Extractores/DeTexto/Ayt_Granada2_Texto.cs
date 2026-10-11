using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
//Sin texto
namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Granada2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoGranada.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(82.336587, 775.646366, 128.935591, 790.367174);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(134.541486, 774.945375, 216.527704, 788.639052);
      var fecha = GetDatos(rec_fecha);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(487.012149, 775.295871, 525.903047, 792.470146);
      var importe = GetDatos(rec_importe).Replace(".", ",");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(76.380323, 716.41264, 333.901135, 734.638402);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(154.162119, 693.630438, 262.075603, 709.052236);
      var marca = GetDatos(rec_marcamodelo).Split(" ")[0];
      return marca;
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(87.241745, 696.434401, 139.446644, 708.701741);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marcamodelo = new PdfRectangle(154.162119, 693.630438, 262.075603, 709.052236);
      return string.Join(" ", GetDatos(rec_marcamodelo).Split(" ").Skip(1));

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(76.029955, 743.400787, 374.193507, 761.626549);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(377.697192, 218.008152, 508.734993, 255.511162);
      var match = GetDatos(rec_match);
      return match.Contains("AYUNTAMIENTO DE GRANADA");
    }
  }
}
