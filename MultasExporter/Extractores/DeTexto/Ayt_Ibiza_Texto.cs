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
  internal class Ayt_Ibiza_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoIbiza.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(417.825323, 523.575695, 545.474795, 536.830776);
      return GetDatos(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(147.529309, 496.716716, 205.77374, 512.413522);
      var rec_hora = new PdfRectangle(205.27374, 497.216716, 262.623095, 512.76234);
      var fecha = GetDatos(rec_fecha).Replace(".", "/");
      var hora = GetDatos(rec_hora);
      if (DateTime.TryParse(fecha + " " + hora, out DateTime fechaHora))
        return fechaHora;

      throw new Exception("No se ha podido recuperar la fecha");
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(491.764362, 359.631274, 541.987105, 377.420988);
      if (decimal.TryParse(GetDatos(rec_importe), out decimal importe))
        return importe;
      throw new Exception("No se ha podido recuperar el importe");  
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(276.922627, 494.972626, 465.955452, 515.204065);
      return GetDatos(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_modelo = new PdfRectangle(39.759672, 424.162589, 146.483002, 440.208213);
      return GetDatos(rec_modelo).Split("-")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(59.290739, 449.975115, 142.297773, 466.718375);
      return GetDatos(rec_matricula).Replace("-","");
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(39.759672, 424.162589, 146.483002, 440.208213);
      return string.Join(" ", GetDatos(rec_modelo).Split("-").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(148.575616, 424.162589, 547.218641, 465.323103);
      return GetDatos(rec_motivo);
    }

    public bool Match()
    {
      var rec_match = new PdfRectangle(469.791911, 496.716716, 549.311255, 511.715886);
      var match = GetDatos(rec_match);
      return match == "IBIZA";
    }
    public int PaginaDatos() { return 2; }
  }
}
