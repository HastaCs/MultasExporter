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
  internal class Ayt_Valencia_Texto : ExtractorBaseTexto, IExtractorTexto
  {
    public string GetClaseVehiculo()
    {
      return "";
    }

    public EmisorDTO GetEmisor()
    {
      return Emisor.AytoValencia.ToEmisorDto();
    }

    public string GetExpediente()
    {
      var rec_expediente = new PdfRectangle(370, 758.961622, 529, 776);
      return GetDato(rec_expediente);
    }

    public DateTime GetFechaMulta()
    {
      var rec_fecha = new PdfRectangle(105.5, 708.321622, 234, 723.5);
      var fecha = GetDato(rec_fecha);
      return DateTime.Now;
    }

    public decimal GetImporte()
    {
      var rec_importe = new PdfRectangle(339.5, 622.401622, 413.04, 637);
      var importe = GetDato(rec_importe).Replace(".", ",").Replace("€","");
      return Convert.ToDecimal(importe);
    }

    public string GetLugarMulta()
    {
      var rec_lugar = new PdfRectangle(28.08, 545.841622, 303.12, 571.281622);
      return GetDato(rec_lugar);
    }

    public string GetMarcaVehiculo()
    {
      var rec_modelo = new PdfRectangle(75.5, 734.001622, 303.12, 751);
      return GetDato(rec_modelo).Split(" ")[0];
    }

    public string GetMatricula()
    {
      var rec_matricula = new PdfRectangle(28.56, 733.521622, 81.5, 751.5);
      return GetDato(rec_matricula);
    }

    public string GetModeloVehiculo()
    {
      var rec_modelo = new PdfRectangle(75.5, 734.001622, 303.12, 751);
      return string.Join(" ",GetDato(rec_modelo).Split(" ").Skip(1));
    }

    public string GetMotivoMulta()
    {
      var rec_motivo = new PdfRectangle(28.08, 585.5, 303.6, 700.401622);
      return GetDato(rec_motivo);
    }

    public bool Match()
    {
     return base.Match("DENUNCIA SERVICI CENTRAL DEL PROCEDIMENT SANCIONADOR");
     
    }

    public void SetPalabras(IEnumerable<Word> palabras)
    {
      base.Set(palabras);
    }
  }
}
