using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{//TODO: Mirar mas ejemplos de multas de cordoba
  internal class Ayt_Cordoba2_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoCordoba.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(36.487741, 709.915698, 107.463894, 724.913917);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fechamulta = new PdfRectangle(35.987909, 476.443423, 117.960368, 491.941582);
      var fecha = GetDatos(rec_fechamulta);
      if (DateTime.TryParse(fecha, out DateTime fechaRes))
        return fechaRes;
      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(213.928124, 446.946926, 263.91133, 465.444729);
      var importe = GetDatos(rec_importe).Replace("€", "");
      if (decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se ha podido recuperar el importe");

    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(400.865316, 459.445441, 552.314431, 480.942889);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(399.365819, 478.443185, 553.314095, 490.441761);
      return GetDatos(rec_marca).Split(" ")[0];

    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(314.894201, 476.943363, 399.365819, 493.441404);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_marca = new PdfRectangle(399.365819, 478.443185, 553.314095, 490.441761);
      var marca = GetDatos(rec_marca);
      return string.Join(" ", GetDatos(rec_marca).Split(" ").Skip(1));

    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(36.487741, 413.450903, 554.313759, 443.947282);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match1 = new PdfRectangle(297.400078, 777.907624, 469.84214, 803.904537);
      var rec_match2 = new PdfRectangle(62.978841, 39.995251, 188.936521, 58.493054);

      var match1 = GetDatos(rec_match1);
      var match2 = GetDatos(rec_match2);
      return match1.Contains("SERVICIO DE GESTIÓN DE MULTAS") && match2.Contains("cordoba");
    }
  }
}
