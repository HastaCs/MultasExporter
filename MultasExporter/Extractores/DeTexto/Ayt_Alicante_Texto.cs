using MultasExporter.Extractores.DeWords;
using MultasExporter.Interfaces;
using MultasExporter.Modelos;
using MultasExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

namespace MultasExporter.Extractores.DeTexto
{
  internal class Ayt_Alicante_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      var rec_clase = new PdfRectangle(442.439991, 599.810378, 551.039991, 615.439991);
      return GetDato(rec_clase);
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoAlicante.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(452.16001, 779, 551.039698, 796.690583);
      return GetDato(rec_expediente).Replace(" ","");
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(51, 572.72002, 175.856755, 589.160987);
      var rec_hora = new PdfRectangle(177.719996, 572.72002, 300.719996, 589.160987);
      var fecha =GetDato(rec_fecha);
      var hora = GetDato(rec_hora);
      if (DateTime.TryParse(fecha + " " + hora, out DateTime fechaHora))
        return fechaHora;
      throw new Exception("No se pudo recuperar la fecha");
    
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(300, 545.72002, 414.72002, 562.052531);
      var importe = GetDato(rec_importe).Replace("Eur","");
      if (Decimal.TryParse(importe, out decimal importeRes))
        return importeRes;
      throw new Exception("No se pudo recuperar el importe");
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(51, 519.439991, 442.339076, 536.700833);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_marca = new PdfRectangle(177.719996, 599.72002, 301.079944, 615.439991);
      return GetDato(rec_marca);
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(53.160199, 599.72002, 175.856755, 615.439991);
      var matricula= GetDato(rec_matricula).Replace("-","");
      if (string.IsNullOrEmpty(matricula))
        throw new Exception("No se pudo recuperar la matricula");
      return matricula;
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(300, 599.72002, 439.641886, 617.610507);
      return GetDato(rec_modelo);
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(51, 455.12002, 551.039991, 503.797566);
      return GetDato(rec_motivo);
    }
    //TODO cambiar los match por eso. Puede que eldiseño sea distinto pero el testo "ayto de alicante" esté en los dos.
    public bool Match()
    {
      var rec_match = new PdfRectangle(21.874675, 769.15982, 190.170763, 785.37555);
      return GetDato(rec_match) == "EXCMO. AYUNTAMIENTO DE ALICANTE";
    }

   
  }
}
