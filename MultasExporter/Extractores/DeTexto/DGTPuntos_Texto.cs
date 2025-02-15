using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class DGTPuntos_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(80.5, 656, 239.5, 677);
      return GetDatos(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.Dgt.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(118.5, 696, 215.5, 718);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(452, 691, 540, 716);
      if (DateTime.TryParseExact(GetDatos(rec_fecha).Trim(), "dd/MM/yyyy - HH:mm'h'", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
        return fecha;

      throw new Exception("No se pudo recuperar la fecha");
    }

    public decimal GetImporte()
    {
      return 0;
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(34.5, 552, 277, 592.5);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(80.5, 637.5, 240, 658);
      return GetDatos(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(101, 673, 240, 695.5);
      return GetDatos(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(89, 618.5, 240.5, 640.5);
      return GetDatos(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(35, 368, 542.5, 413.5);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_ayto = new PdfRectangle(48, 732, 250, 751.5);
      var rec_matchPuntos = new PdfRectangle(36, 451.5, 182, 470);
      var match1 = GetDatos(rec_ayto);
      var match2 = GetDatos(rec_matchPuntos);

      return match1 == "Centro de Tratamiento de Denuncias Automatizadas" && match2 == "Esta infracción lleva aparejada la";

    }
  }
}
